using System.Collections.Generic;
using BikeDistributor.Discount;
using BikeDistributor.ReceiptBuilder;
using BikeDistributor.ReceiptBuilder.Enums;

namespace BikeDistributor
{
    public class Order
    {
        private const decimal TaxRate = .0725m;
        private readonly IList<Line> _lines = new List<Line>();
        private readonly DiscountRulesEngine<Line> _discountRulesEngine;

        public Order(string company, DiscountRulesEngine<Line> discountRulesEngine)
        {
            Company = company;
            _discountRulesEngine = discountRulesEngine;
        }

        public string Company { get; }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public Order Complete()
        {
            foreach (var line in _lines)
            {
                var discount = _discountRulesEngine.GetDiscount(line);
                line.ApplyDiscount(discount);
            }            

            return this;
        }

        public string Receipt(ReceiptTypes type, bool showDiscountCode = false)
        {
            return ReceiptBuilderFactory.GetBuilder(type)
                                        .ForCompany(Company)
                                        .WithHeader()
                                        .WithLineItems(_lines, showDiscountCode)
                                        .WithSubTotal()
                                        .WithTax(TaxRate)
                                        .WithTotal()
                                        .WithFooter()
                                        .Build();
        }
    }
}
