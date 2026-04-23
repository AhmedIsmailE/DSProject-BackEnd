using MarketPlace.Application.Commands;
using MarketPlace.Backend.TCPServer;
using MarketPlace.Backend.TCPServer.Routing;
using MarketPlace.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
// using Marketplace.Infrastructure; // When you implement DI extension

// This is your Core Engine. It uses ASP.NET Core so we can host REST APIs and TCP Sockets side-by-side.
var builder = WebApplication.CreateBuilder(args);

// --- 1. Register Infrastructure and Application Services ---
// builder.Services.AddInfrastructure(); // Extension method to add DB/Repositories
builder.Services.AddSingleton<CommandDispatcher>();
builder.Services.AddTransient<LengthPrefixFramer>();

builder.Services.AddInfrastructure(); // This one line registers all your repositories!
// ---> THE FIX: Register all the command handlers the Dispatcher needs
builder.Services.AddTransient<LoginCommandHandler>();
builder.Services.AddTransient<CreateAccountCommandHandler>();
builder.Services.AddTransient<PurchaseItemCommandHandler>();
builder.Services.AddTransient<DepositCashCommandHandler>();
builder.Services.AddTransient<AddItemCommandHandler>();
// <---

// --- 2. Register REST API Controllers ---
builder.Services.AddControllers();

// --- 3. Register the Raw TCP Listener as a Background Service ---
builder.Services.AddHostedService<ServerSocketService>();

var app = builder.Build();

// Map REST attribute routing
app.MapControllers();

// Start the server (Listens for HTTP on default ports, BackgroundService listens on custom TCP port)
app.Run();