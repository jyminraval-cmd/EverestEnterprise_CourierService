using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Infrastructure;
using CourierServiceConsApp.Services.Implementations;
using CourierServiceConsApp.Services.Interface;

namespace CourierServiceConsApp.Presentation
{
    public class ConsoleApp
    {
        private readonly InputParser _inputParser;
        private readonly OutputFormatter _outputFormatter;
        private readonly ICostCalculator _costCalculator;
        private readonly IOfferService _offerService;
        private readonly IShipmentSelector _shipmentSelector;
        private readonly IDeliveryScheduler _deliveryScheduler;

        public ConsoleApp()
        {
            var offerRepo = new OfferRepository();
            _offerService = new OfferService(offerRepo);

            _inputParser = new InputParser();
            _outputFormatter = new OutputFormatter();
            _costCalculator = new CostCalculator(_offerService);
            _shipmentSelector = new ShipmentSelector();
            _deliveryScheduler = new DeliveryScheduler(_shipmentSelector);
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                ShowMenu();

                Console.Write("\nSelect option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunCourierEstimator();
                        break;

                    case "2":
                        ManageOffers();
                        break;

                    case "3":
                        Console.WriteLine("Exiting application...");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress ENTER to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
        private void ShowMenu()
        {
            Console.WriteLine("===============================");
            Console.WriteLine(" 🚚 Courier Service Estimator");
            Console.WriteLine("===============================");
            Console.WriteLine("1. Run a new calculation");
            Console.WriteLine("2. Manage Offer Codes");
            Console.WriteLine("3. Exit");
            Console.WriteLine("===============================");
        }

        private void ManageOffers()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("===== Offer Management =====");
                Console.WriteLine("1. List Offers");
                Console.WriteLine("2. Add Offer");
                Console.WriteLine("3. Remove Offer");
                Console.WriteLine("4. Back");
                Console.WriteLine("============================");
                Console.Write("Select: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListOffers();
                        break;

                    case "2":
                        AddOffer();
                        break;

                    case "3":
                        RemoveOffer();
                        break;

                    case "4":
                        back = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }

        private void ListOffers()
        {
            var offers = _offerService.GetAllOffers();

            Console.WriteLine("\nAvailable Offers:");
            foreach (var o in offers)
            {
                Console.WriteLine($"{o.Code} - {o.DiscountPercent}% | Weight[{o.MinWeight}-{o.MaxWeight}] | Distance[{o.MinDistance}-{o.MaxDistance}]");
            }
        }

        private void AddOffer()
        {
            Console.Write("Enter Offer Code: ");
            string code = Console.ReadLine()!;

            Console.Write("Enter Discount %: ");
            double pct = ReadDouble();

            Console.Write("Enter Min Weight: ");
            double minW = ReadDouble();

            Console.Write("Enter Max Weight: ");
            double maxW = ReadDouble();

            Console.Write("Enter Min Distance: ");
            double minD = ReadDouble();

            Console.Write("Enter Max Distance: ");
            double maxD = ReadDouble();

            _offerService.AddOffer(new Offer
            {
                Code = code,
                DiscountPercent = pct,
                MinWeight = minW,
                MaxWeight = maxW,
                MinDistance = minD,
                MaxDistance = maxD
            });

            Console.WriteLine("Offer added successfully!");
        }

        private void RemoveOffer()
        {
            Console.Write("Enter Offer Code to Remove: ");
            string code = Console.ReadLine()!;

            bool removed = _offerService.RemoveOffer(code);

            Console.WriteLine(removed ? "Offer removed!" : "Offer not found.");
        }

        private double ReadDouble()
        {
            string? input = Console.ReadLine();
            double val;
            while (!double.TryParse(input, out val) || val < 0)
            {
                Console.Write("Invalid value. Enter again: ");
                input = Console.ReadLine();
            }
            return val;
        }

        private void RunCourierEstimator()
        {
            try
            {
                var input = _inputParser.Parse();

                foreach (var pkg in input.Packages)
                    _costCalculator.CalculateCost(input.BaseDeliveryCost, pkg);

                _deliveryScheduler.ScheduleDeliveries(
                    input.Packages,
                    input.NumberOfVehicles,
                    input.VehicleSpeed,
                    input.MaxCarriableWeight
                );

                Console.WriteLine("\n--- Output ---");
                _outputFormatter.PrintResults(input.Packages);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
    }
}
