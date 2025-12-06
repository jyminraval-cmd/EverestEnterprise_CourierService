using CourierServiceConsApp.Presentation;
using CourierServiceConsApp.Services.Interface;
public class ConsoleApp
{
    private readonly Menu _menu;
    private readonly InputParser _parser;
    private readonly ICostCalculator _calculator;
    private readonly IDeliveryScheduler _scheduler;
    private readonly IOfferService _offerService;
    private readonly OutputFormatter _formatter;

    public ConsoleApp(Menu menu, InputParser parser, ICostCalculator calculator,
                      IDeliveryScheduler scheduler, IOfferService offerService, OutputFormatter formatter)
    {
        _menu = menu;
        _parser = parser;
        _calculator = calculator;
        _scheduler = scheduler;
        _offerService = offerService;
        _formatter = formatter;
    }

    public void Run()
    {
        while (true)
        {
            int option = _menu.ShowMainMenu();

            switch (option)
            {
                case 1:
                    RunCostAndTimeCalculation();
                    break;

                case 2:
                    ManageOffers();
                    break;

                case 3:
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }
    }

    private void RunCostAndTimeCalculation()
    {
        try
        {
            var input = _parser.Parse();

            foreach (var pkg in input.Packages)
            {
                _calculator.CalculateCost(input.BaseDeliveryCost, pkg);
            }

            _scheduler.ScheduleDeliveries(
                input.Packages,
                input.NumberOfVehicles,
                input.VehicleSpeed,
                input.MaxCarriableWeight
            );

            Console.WriteLine("\n--- Output ---\n");
            _formatter.PrintResults(input.Packages);   // <-- REQUIRED
        }
        catch (Exception ex)
        {
            Console.WriteLine("Operation cancelled: " + ex.Message);
        }
    }

    private void ManageOffers()
    {
        Console.WriteLine("\n1. Add Offer\n2. Remove Offer\n3. List Offers\nSelect: ");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Enter Offer Code: ");
            var code = Console.ReadLine();

            Console.Write("Enter Discount %: ");
            var discount = Convert.ToInt32(Console.ReadLine());

            _offerService.AddOffer(new CourierServiceConsApp.Domain.Offer
            {
                Code = code!,
                DiscountPercent = discount,
                MinWeight = 0,
                MaxWeight = double.MaxValue,
                MinDistance = 0,
                MaxDistance = double.MaxValue
            });

            Console.WriteLine("Offer added!");
        }
        else if (choice == "2")
        {
            Console.Write("Enter Offer Code: ");
            var code = Console.ReadLine();

            Console.WriteLine(_offerService.RemoveOffer(code!) ? "Removed!" : "Not Found!");
        }
        else if (choice == "3")
        {
            foreach (var offer in _offerService.GetAllOffers())
                Console.WriteLine($"{offer.Code} - {offer.DiscountPercent}%");

        }
    }
}


//namespace CourierServiceConsApp.Presentation
//{
//    public class ConsoleApp
//    {
//        private readonly InputParser _inputParser;
//        private readonly OutputFormatter _outputFormatter;
//        private readonly ICostCalculator _costCalculator;
//        private readonly IOfferService _offerService;
//        private readonly IShipmentSelector _shipmentSelector;
//        private readonly IDeliveryScheduler _deliveryScheduler;

//        public ConsoleApp()
//        {
//            var offerRepo = new OfferRepository();
//            _offerService = new OfferService(offerRepo);

//            _inputParser = new InputParser();
//            _outputFormatter = new OutputFormatter();
//            _costCalculator = new CostCalculator(_offerService);
//            _shipmentSelector = new ShipmentSelector();
//            _deliveryScheduler = new DeliveryScheduler(_shipmentSelector);
//        }

//        public void Run()
//        {
//            bool exit = false;

//            while (!exit)
//            {
//                ShowMenu();

//                Console.Write("\nSelect option: ");
//                string? choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        RunCourierEstimator();
//                        break;

//                    case "2":
//                        ManageOffers();
//                        break;

//                    case "3":
//                        Console.WriteLine("Exiting application...");
//                        exit = true;
//                        break;

//                    default:
//                        Console.WriteLine("Invalid option. Try again.");
//                        break;
//                }

//                if (!exit)
//                {
//                    Console.WriteLine("\nPress ENTER to return to menu...");
//                    Console.ReadLine();
//                    Console.Clear();
//                }
//            }
//        }
//        private void ShowMenu()
//        {
//            Console.WriteLine("===============================");
//            Console.WriteLine(" 🚚 Courier Service Estimator");
//            Console.WriteLine("===============================");
//            Console.WriteLine("1. Run a new calculation");
//            Console.WriteLine("2. Manage Offer Codes");
//            Console.WriteLine("3. Exit");
//            Console.WriteLine("===============================");
//        }

//        private void ManageOffers()
//        {
//            bool back = false;

//            while (!back)
//            {
//                Console.Clear();
//                Console.WriteLine("===== Offer Management =====");
//                Console.WriteLine("1. List Offers");
//                Console.WriteLine("2. Add Offer");
//                Console.WriteLine("3. Remove Offer");
//                Console.WriteLine("4. Back");
//                Console.WriteLine("============================");
//                Console.Write("Select: ");

//                string? choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        ListOffers();
//                        break;

//                    case "2":
//                        AddOffer();
//                        break;

//                    case "3":
//                        RemoveOffer();
//                        break;

//                    case "4":
//                        back = true;
//                        break;

//                    default:
//                        Console.WriteLine("Invalid option!");
//                        break;
//                }

//                if (!back)
//                {
//                    Console.WriteLine("\nPress ENTER to continue...");
//                    Console.ReadLine();
//                }
//            }
//        }

//        private void ListOffers()
//        {
//            var offers = _offerService.GetAllOffers();

//            Console.WriteLine("\nAvailable Offers:");
//            foreach (var o in offers)
//            {
//                Console.WriteLine($"{o.Code} - {o.DiscountPercent}% | Weight[{o.MinWeight}-{o.MaxWeight}] | Distance[{o.MinDistance}-{o.MaxDistance}]");
//            }
//        }

//        private void AddOffer()
//        {
//            Console.Write("Enter Offer Code: ");
//            string code = Console.ReadLine()!;

//            Console.Write("Enter Discount %: ");
//            double pct = ReadDouble();

//            Console.Write("Enter Min Weight: ");
//            double minW = ReadDouble();

//            Console.Write("Enter Max Weight: ");
//            double maxW = ReadDouble();

//            Console.Write("Enter Min Distance: ");
//            double minD = ReadDouble();

//            Console.Write("Enter Max Distance: ");
//            double maxD = ReadDouble();

//            _offerService.AddOffer(new Offer
//            {
//                Code = code,
//                DiscountPercent = pct,
//                MinWeight = minW,
//                MaxWeight = maxW,
//                MinDistance = minD,
//                MaxDistance = maxD
//            });

//            Console.WriteLine("Offer added successfully!");
//        }

//        private void RemoveOffer()
//        {
//            Console.Write("Enter Offer Code to Remove: ");
//            string code = Console.ReadLine()!;

//            bool removed = _offerService.RemoveOffer(code);

//            Console.WriteLine(removed ? "Offer removed!" : "Offer not found.");
//        }

//        private double ReadDouble()
//        {
//            string? input = Console.ReadLine();
//            double val;
//            while (!double.TryParse(input, out val) || val < 0)
//            {
//                Console.Write("Invalid value. Enter again: ");
//                input = Console.ReadLine();
//            }
//            return val;
//        }

//        private void RunCourierEstimator()
//        {
//            try
//            {
//                var input = _inputParser.Parse();

//                foreach (var pkg in input.Packages)
//                    _costCalculator.CalculateCost(input.BaseDeliveryCost, pkg);

//                _deliveryScheduler.ScheduleDeliveries(
//                    input.Packages,
//                    input.NumberOfVehicles,
//                    input.VehicleSpeed,
//                    input.MaxCarriableWeight
//                );

//                Console.WriteLine("\n--- Output ---");
//                _outputFormatter.PrintResults(input.Packages);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\nError: {ex.Message}");
//            }
//        }
//    }
//}
