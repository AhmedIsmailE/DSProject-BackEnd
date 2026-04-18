using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


// This is the entry point for your Translation Gateway.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container (e.g., your WebSocket manager and TCP wrapper)
// builder.Services.AddSingleton<WebSocketConnectionManager>();
// builder.Services.AddTransient<TcpClientWrapper>();

var app = builder.Build();

// CRITICAL: Enable WebSockets middleware
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
app.UseWebSockets(webSocketOptions);

// Map the endpoint that the React app will connect to (e.g., wss://localhost:xxxx/ws)
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        // TODO: Resolve WebSocketConnectionManager from DI and pass the webSocket to it
        // var connectionManager = context.RequestServices.GetRequiredService<WebSocketConnectionManager>();
        // await connectionManager.HandleConnectionAsync(webSocket);
    }
    else
    {
        context.Response.StatusCode = 400; // Bad Request if it's not a WebSocket
    }
});

app.Run();