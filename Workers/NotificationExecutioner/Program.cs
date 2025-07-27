using NLog.Extensions.Logging;
using NotificationExecutioner;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Logging.ClearProviders();
builder.Logging.AddNLog();
builder.Services.AddDI();

var host = builder.Build();
host.Run();
