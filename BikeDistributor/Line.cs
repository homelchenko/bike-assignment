namespace BikeDistributor
{
    public class Line
    {
        public Line(Bike bike, int quantity)
        {
            Bike = bike;
            Quantity = quantity;
        }

        public Bike Bike { get; }
        public int Quantity { get; }

        public double ApplyDiscount(double discount)
        {
            return Quantity * Bike.Price * (1 - discount);
        }
    }
}
