using CourierServiceConsApp.Infrastructure;
using CourierServiceConsApp.Presentation;
using CourierServiceConsApp.Services.Implementations;
using CourierServiceConsApp.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// DI Setup
services.AddSingleton<IOfferService, OfferService>();
services.AddSingleton<OfferRepository>();
services.AddSingleton<ICostCalculator, CostCalculator>();
services.AddSingleton<IDeliveryScheduler, DeliveryScheduler>();
services.AddSingleton<IShipmentSelector, ShipmentSelector>();

services.AddSingleton<InputParser>();
services.AddSingleton<Menu>();
services.AddSingleton<ConsoleApp>();
services.AddSingleton<OutputFormatter>();

var provider = services.BuildServiceProvider();
var app = provider.GetRequiredService<ConsoleApp>();
app.Run();
