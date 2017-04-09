namespace BikeDistributor
{
    public class Bike
    {
        public Bike(string brand, string model, int year, int price)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
        }

        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public int Year { get; set; }
        public int Price { get; set; }
    }
}