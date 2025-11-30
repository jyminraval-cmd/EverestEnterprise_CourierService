namespace CourierServiceConsApp.Domain
{
    public class Vehicle
    {
        public int Id { get; set; }
        public double MaxCarryWeight { get; set; }
        public double Speed { get; set; }
        public double AvailableAt { get; set; } = 0;
    }
}
