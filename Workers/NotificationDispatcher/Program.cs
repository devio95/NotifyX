using NotificationDispatcher;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDI(builder.Configuration);

var host = builder.Build();
host.Run();
