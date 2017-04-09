namespace BikeDistributor
{
    public class Bike
    {
        public const int OneThousand = 1000;
        public const int TwoThousand = 2000;
        public const int FiveThousand = 5000;
    
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

//TODO: Build rules engine for discounts
//DONE: Refactor receipts with builder pattern

    //NOTE: changing to decimal since it involves money
    //NOTE: considered adding additional products, but that was beyond the scope of the requirements
    //NOTE: tag builder from MVC
    //NOTE: Find the "best" discount for an order