using NotifyCalculator;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDI();

var host = builder.Build();
host.Run();
