using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Services.Interface;

namespace CourierServiceConsApp.Services.Implementations
{
    public class ShipmentSelector : IShipmentSelector
    {
        public List<Package> PickBestShipment(List<Package> remaining, double capacity)
        {
            int n = remaining.Count;
            if (n == 0) return new List<Package>();

            if (n <= 20)
                return ExhaustiveSearch(remaining, capacity);

            return GreedyHeuristic(remaining, capacity);
        }

        private List<Package> ExhaustiveSearch(List<Package> remaining, double capacity)
        {
            int n = remaining.Count;
            List<Package> best = new();
            int maxCount = 0;
            double bestWeight = 0;
            double bestMaxDistance = double.MaxValue;

            int total = 1 << n;

            for (int mask = 1; mask < total; mask++)
            {
                double weight = 0;
                int count = 0;
                double maxDist = 0;
                var chosen = new List<Package>();

                for (int i = 0; i < n; i++)
                {
                    if (((mask >> i) & 1) == 1)
                    {
                        var p = remaining[i];
                        weight += p.Weight;
                        if (weight > capacity) { count = -1; break; }
                        chosen.Add(p);
                        count++;
                        if (p.Distance > maxDist) maxDist = p.Distance;
                    }
                }

                if (count <= 0) continue;

                if (count > maxCount ||
                    (count == maxCount && weight > bestWeight) ||
                    (count == maxCount && Math.Abs(weight - bestWeight) < 1e-9 && maxDist < bestMaxDistance))
                {
                    maxCount = count;
                    bestWeight = weight;
                    bestMaxDistance = maxDist;
                    best = chosen;
                }
            }

            return best;
        }

        private List<Package> GreedyHeuristic(List<Package> remaining, double capacity)
        {
            var sorted = remaining.OrderByDescending(p => p.Weight).ToList();

            List<Package> result = new();
            double w = 0;

            foreach (var p in sorted)
            {
                if (w + p.Weight <= capacity)
                {
                    result.Add(p);
                    w += p.Weight;
                }
            }

            return result;
        }

        public List<List<Package>> CreateShipments(List<Package> packages, double capacity)
        {
            var remaining = new List<Package>(packages);
            var shipments = new List<List<Package>>();

            while (remaining.Count > 0)
            {
                var shipment = PickBestShipment(remaining, capacity);

                if (shipment.Count == 0)
                    break;

                shipments.Add(shipment);

                // Remove selected packages from remaining list
                foreach (var pkg in shipment)
                {
                    remaining.Remove(pkg);
                }
            }

            return shipments;
        }
    }
}
