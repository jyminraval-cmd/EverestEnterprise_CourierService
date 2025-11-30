namespace CourierServiceConsApp.Domain
{
    public class Offer
    {
        public string Code { get; set; } = "";
        public double DiscountPercent { get; set; }
        public double MinWeight { get; set; }
        public double MaxWeight { get; set; }
        public double MinDistance { get; set; }
        public double MaxDistance { get; set; }

        public bool IsApplicable(Package p)
        {
            return p.Weight >= MinWeight &&
                   p.Weight <= MaxWeight &&
                   p.Distance >= MinDistance &&
                   p.Distance <= MaxDistance;
        }
    }
}
