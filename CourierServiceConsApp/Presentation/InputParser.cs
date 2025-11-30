using CourierServiceConsApp.Domain;

namespace CourierServiceConsApp.Presentation
{
    public class InputParser
    {
        public ParsedInput Parse()
        {
            Console.Write("Enter base delivery cost: ");
            double baseCost = ReadPositiveDouble();

            Console.Write("Enter number of packages: ");
            int numberOfPackages = ReadPositiveInt();


            // Read Package Inputs
            var packages = new List<Package>();

            for (int i = 1; i <= numberOfPackages; i++)
            {
                Console.WriteLine($"\nEnter package {i} (Format: pkg_id weight distance offer_code): ");
                Console.WriteLine("Type 'done' to stop early or 'exit' to cancel:");

                string? line = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("Input cannot be empty. Try again:");
                    i--;
                    continue;
                    //line = Console.ReadLine();
                }

                if (line.Trim().ToLower() == "exit")
                    throw new Exception("User cancelled the calculation.");

                if (line.Trim().ToLower() == "done")
                {
                    Console.WriteLine("Stopping early. Remaining packages will not be added.");
                    break;
                }

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 4)
                {
                    Console.WriteLine("Invalid format. Example: PKG1 50 30 OFR001");
                    i--;
                    continue;
                }

                if (!double.TryParse(parts[1], out double weight) || weight <= 0)
                {
                    Console.WriteLine("Weight must be a positive number.");
                    i--;
                    continue;
                }

                if (!double.TryParse(parts[2], out double distance) || distance <= 0)
                {
                    Console.WriteLine("Distance must be a positive number.");
                    i--;
                    continue;
                }

                packages.Add(new Package(
                    parts[0],
                    weight,
                    distance,
                    parts[3]
                ));
            }

            Console.WriteLine($"\nTotal packages entered: {packages.Count}");

            
            // Read Vehicle Inputs
            Console.Write("\nEnter number of vehicles: ");
            int numberOfVehicles = ReadPositiveInt();

            Console.Write("Enter vehicle max speed: ");
            double speed = ReadPositiveDouble();

            Console.Write("Enter max carriable weight: ");
            double maxWeight = ReadPositiveDouble();


            return new ParsedInput
            {
                BaseDeliveryCost = baseCost,
                NumberOfPackages = numberOfPackages,
                Packages = packages,
                NumberOfVehicles = numberOfVehicles,
                VehicleSpeed = speed,
                MaxCarriableWeight = maxWeight
            };
        }

        // Read positive int
        private int ReadPositiveInt()
        {
            string? input = Console.ReadLine();
            int number;

            while (!int.TryParse(input, out number) || number <= 0)
            {
                Console.Write("Invalid entry. Enter a positive number: ");
                input = Console.ReadLine();
            }

            return number;
        }

        //Read positive double
        private double ReadPositiveDouble()
        {
            string? input = Console.ReadLine();
            double number;

            while (!double.TryParse(input, out number) || number <= 0)
            {
                Console.Write("Invalid value. Enter a positive number: ");
                input = Console.ReadLine();
            }

            return number;
        }
    }

    public class ParsedInput
    {
        public double BaseDeliveryCost { get; set; }
        public int NumberOfPackages { get; set; }
        public List<Package> Packages { get; set; }
        public int NumberOfVehicles { get; set; }
        public double VehicleSpeed { get; set; }
        public double MaxCarriableWeight { get; set; }
    }
}
