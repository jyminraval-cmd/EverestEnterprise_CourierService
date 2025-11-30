using CourierServiceConsApp.Domain;

namespace CourierServiceConsApp.Presentation
{
    public class OutputFormatter
    {
        public void PrintResults(List<Package> packages)
        {
            foreach (var p in packages)
            {
                Console.WriteLine($"{p.Id} {Format(p.Discount)} {Format(p.TotalCost)} {Format(p.EstimatedDeliveryTime)}");
            }
        }

        private string Format(double value)
        {
            return value % 1 == 0 ? $"{(int)value}" : $"{value:F2}";
        }
    }
}
