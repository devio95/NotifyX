using NLog.Extensions.Logging;
using NotificationDispatcher;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Logging.ClearProviders();
builder.Logging.AddNLog();
builder.Services.AddDI(builder.Configuration);

var host = builder.Build();
host.Run();
