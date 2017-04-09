using System.Collections.Generic;
using BikeDistributor.ReceiptBuilder.Interfaces;

namespace BikeDistributor.ReceiptBuilder.Builders
{
    public class PaperReceiptBuilder : AbstractReceiptBuilder
    {
        public override IReceiptBuilder WithHeader()
        {
            base.WithHeader();
            Receipt.AppendLine();

            return this;
        }

        public override IReceiptBuilder AddLineItem(Line line, bool showDiscountCode = false)
        {
            Receipt.Append("\t");
            base.AddLineItem(line, showDiscountCode);
            Receipt.AppendLine();

            return this;
        }

        public override IReceiptBuilder WithLineItems(IList<Line> lines, bool showDiscountCode = false)
        {
            foreach (var line in lines)
            {
                AddLineItem(line, showDiscountCode);
            }

            return this;
        }

        public override IReceiptBuilder WithSubTotal()
        {
            base.WithSubTotal();
            Receipt.AppendLine();

            return this;
        }

        public override IReceiptBuilder WithTax(decimal taxRate)
        {
            base.WithTax(taxRate);
            Receipt.AppendLine();

            return this;
        }
    }    
}