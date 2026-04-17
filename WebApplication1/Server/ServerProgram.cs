using System.Net;
using System.Net.Sockets;
using System.Text;
using WebApplication1.Server.Networking;

namespace WebApplication1.Server
{
    public class ServerProgram
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Socket Server listening on port 5000...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                // Start a new thread for each client so multiple can connect
                new Thread(() => HandleClient(client)).Start();
            }
        }

        static void HandleClient(TcpClient client)
        {
            // 1. Read the incoming network stream into a string
            // 2. Deserialize the string into your Request object:
            //    Request req = JsonSerializer.Deserialize<Request>(jsonString);

            // 3. Validate SessionToken here (unless req.Type is LOGIN or REGISTER){make sure the session token is unique somehow 
            // bc we would need to test multiple clients on same pc so same ip address so we need to differ by different sessiontokens

            //    int currentUserId = MockDatabase.ActiveSessions[req.SessionToken];

            // 4. Route the request
            /*
            switch (req.Type) 
            {
                case RequestType.LOGIN:
                    // call userService.Login(...)
                    break;
                case RequestType.PURCHASE_ITEM:
                    // call orderService.PurchaseItem(currentUserId, ...)
                    break;
                case RequestType.MANAGE_INVENTORY:
                    // call productService.UpdateInventory(...)
                    break;
                // ... add cases for all RequestTypes
            }
            */

            // 5. Serialize your response and write it back to the client stream
        }
    }
}
