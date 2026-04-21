using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Application.Commands
{
    public class PurchaseItemCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ITransactionRepository _transactionRepository;

        private static readonly SemaphoreSlim _purchaseLock = new(1, 1);

        // Dependency Injection
        public PurchaseItemCommandHandler(IUserRepository userRepository, IWalletRepository walletRepository, IItemRepository itemRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _itemRepository = itemRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Executes the distributed transaction to purchase an item.
        /// </summary>
        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // TODO: Implement the Saga Pattern here:
            // 1. Deserialize the request.Payload to get BuyerId and ItemId.
            // 2. Fetch Buyer from _userRepository.
            // 3. Check if Buyer has enough WalletBalance. If not, return error envelope.
            // 4. Fetch Item from _itemRepository. Check if it's available.
            // 5. Deduct money from Buyer, add to Seller.
            // 6. Transfer Item ownership to Buyer.
            // 7. Save changes via repositories.
            // 8. Handle compensating transactions (reverting) if any step fails.
            /*  Payload:
                {
                    "BuyerId": "11111111-1111-1111-1111-111111111111",
                    "ItemId": "22222222-2222-2222-2222-222222222222"
                }
            */

            Guid buyerId;
            Guid itemId;

            try
            {
                using var jsonDoc = JsonDocument.Parse(request.Payload);
                var root = jsonDoc.RootElement;

                buyerId = root.GetProperty("BuyerId").GetGuid();
                itemId = root.GetProperty("ItemId").GetGuid();
            }
            catch
            {
                return BuildResponse(
                    request.CorrelationId,
                    "PURCHASE_FAILED",
                    new
                    {
                        Success = false,
                        Message = "Invalid payload format."
                    });
            }

            await _purchaseLock.WaitAsync();

            try
            {
                var buyer = await _userRepository.GetByIdAsync(buyerId);
                if (buyer == null)
                {
                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "Buyer not found."
                        });
                }

                var item = await _itemRepository.GetByIdAsync(itemId);
                if (item == null)
                {
                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "Item not found."
                        });
                }

                if (!item.IsAvailable)
                {
                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "Item is not available."
                        });
                }

                if (item.OwnerId == buyerId)
                {
                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "You already own this item."
                        });
                }

                var buyerWallet = await _walletRepository.GetByUserIdAsync(buyerId);
                if (buyerWallet == null)
                {
                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "Buyer wallet not found."
                        });
                }

                var sellerWallet = await _walletRepository.GetByUserIdAsync(item.OwnerId);
                if (sellerWallet == null)
                {
                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "Seller wallet not found."
                        });
                }

                if (buyerWallet.Balance < item.Price)
                {
                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "Insufficient balance."
                        });
                }

                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    BuyerId = buyerId,
                    SellerId = item.OwnerId,
                    ItemId = item.Id,
                    Amount = item.Price,
                    Timestamp = DateTime.UtcNow,
                    Status = "Pending"
                };

                await _transactionRepository.AddAsync(transaction);

                try
                {
                    buyerWallet.Balance -= item.Price;
                    sellerWallet.Balance += item.Price;

                    item.OwnerId = buyerId;
                    item.IsAvailable = false;

                    await _walletRepository.UpdateAsync(buyerWallet);
                    await _walletRepository.UpdateAsync(sellerWallet);
                    await _itemRepository.UpdateAsync(item);

                    transaction.Status = "Completed";
                    await _transactionRepository.UpdateAsync(transaction);

                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_SUCCESS",
                        new
                        {
                            Success = true,
                            Message = "Purchase completed successfully.",
                            TransactionId = transaction.Id,
                            ItemId = item.Id,
                            BuyerId = buyerId,
                            NewBalance = buyerWallet.Balance
                        });
                }
                catch
                {
                    transaction.Status = "Failed";
                    await _transactionRepository.UpdateAsync(transaction);

                    return BuildResponse(
                        request.CorrelationId,
                        "PURCHASE_FAILED",
                        new
                        {
                            Success = false,
                            Message = "Purchase failed during processing."
                        });
                }
            }
            finally
            {
                _purchaseLock.Release();
            }


        }

        private static JsonEnvelope BuildResponse(string correlationId, string command, object payload)
        {
            return new JsonEnvelope
            {
                CorrelationId = correlationId,
                Command = command,
                Payload = JsonSerializer.Serialize(payload)
            };
        }
    }
}
