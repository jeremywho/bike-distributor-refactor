using BikeDistributor.Discount;

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
        public decimal Total { get; private set; }
        public Discount<Line> Discount { get; set; } 

        public void ApplyDiscount(Discount<Line> discount)
        {
            Discount = discount;
            Total = Quantity * Bike.Price * (discount?.Amount ?? 1);
        }
    }
}
