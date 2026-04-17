using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using WebApplication1.Services;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Register your services here so the WebSocket handler can use them
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<ReportService>();

var app = builder.Build();

// 1. ENABLE WEBSOCKETS (This is the magic line)
app.UseWebSockets();

// 2. CREATE THE WEBSOCKET ENDPOINT
app.Map("/ws", async (HttpContext context,
    UserService userService,
    ProductService productService,
    OrderService orderService) =>
{
    // Check if the incoming request is a WebSocket request from React
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        // Pass the socket and the services to your new handler
        var handler = new WebSocketHandler(userService, productService, orderService);
        await handler.ListenToClientAsync(webSocket);
    }
    else
    {
        context.Response.StatusCode = 400; // Bad Request if not a WebSocket
    }
});

Console.WriteLine("WebSocket Server running at ws://localhost:5000/ws ...");
app.Run();