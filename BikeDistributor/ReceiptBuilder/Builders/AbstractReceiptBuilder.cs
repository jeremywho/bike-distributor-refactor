using System.Collections.Generic;
using System.Text;
using BikeDistributor.ReceiptBuilder.Interfaces;

namespace BikeDistributor.ReceiptBuilder.Builders
{
    public abstract class AbstractReceiptBuilder : IReceiptBuilder
    {
        protected readonly StringBuilder Receipt = new StringBuilder();
        protected string Company = string.Empty;
        protected decimal SubTotal;
        protected decimal Tax;

        public virtual IReceiptBuilder ForCompany(string company)
        {
            Company = company;
            return this;
        }

        public virtual IReceiptBuilder WithHeader()
        {
            Receipt.Append($"Order Receipt for {Company}");
            return this;
        }

        public abstract IReceiptBuilder WithLineItems(IList<Line> lines, bool showDiscountCode = false);

        public virtual IReceiptBuilder AddLineItem(Line line, bool showDiscountCode = false)
        {           
            SubTotal += line.Total;

            if (showDiscountCode && line.Discount != null) Receipt.Append($"Discount applied: {line.Discount.Code} - ");

            Receipt.Append($"{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {line.Total.ToString("C")}");
            
            return this;
        }

        public virtual IReceiptBuilder WithSubTotal()
        {
            Receipt.Append($"Sub-Total: {SubTotal.ToString("C")}");
            return this;
        }

        public virtual IReceiptBuilder WithTax(decimal taxRate)
        {
            Tax = SubTotal * taxRate;
            Receipt.Append($"Tax: {Tax.ToString("C")}");
            return this;
        }

        public virtual IReceiptBuilder WithTotal()
        {
            Receipt.Append($"Total: {(SubTotal + Tax).ToString("C")}");
            return this;
        }

        public virtual IReceiptBuilder WithFooter()
        {
            return this;
        }

        public virtual string Build()
        {
            return Receipt.ToString();
        }
    }
}
