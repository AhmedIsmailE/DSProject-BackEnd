using System;
using System.Threading.Tasks;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Application.Commands
{
    public class DepositCashCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public DepositCashCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // TODO:
            // 1. Deserialize request.Payload (UserId, AmountToDeposit).
            // 2. Fetch the user: var user = await _userRepository.GetByIdAsync(userId).
            // 3. If user doesn't exist, return error.
            // 4. Add cash: user.WalletBalance += amount.
            // 5. Await _userRepository.UpdateAsync(user).
            // 6. Return a "DEPOSIT_SUCCESS" JsonEnvelope reflecting the new balance.

            return new JsonEnvelope
            {
                CorrelationId = request.CorrelationId,
                Command = "DEPOSIT_FAILED",
                Payload = "{\"message\": \"Not implemented yet\"}"
            };
        }
    }
}