namespace CourierServiceConsApp.Presentation
{
    public class Menu
    {
        public int ShowMainMenu()
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine(" 🚚 Courier Service Estimator");
            Console.WriteLine("===============================");
            Console.WriteLine("1. Run Calculation");
            Console.WriteLine("2. Manage Offers");
            Console.WriteLine("3. Exit");
            Console.WriteLine("===============================");

            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            return int.TryParse(input, out int result) ? result : -1;
        }
    }
}
