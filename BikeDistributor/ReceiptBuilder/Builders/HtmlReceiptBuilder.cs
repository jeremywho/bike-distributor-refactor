using System.Collections.Generic;
using BikeDistributor.ReceiptBuilder.Interfaces;

namespace BikeDistributor.ReceiptBuilder.Builders
{
    public class HtmlReceiptBuilder : AbstractReceiptBuilder
    {
        public HtmlReceiptBuilder()
        {
            Receipt.Append("<html><body>");
        }
        public override IReceiptBuilder WithHeader()
        {
            Receipt.Append("<h1>");

            base.WithHeader();

            Receipt.Append("</h1>");

            return this;
        }

        public override IReceiptBuilder WithLineItems(IList<Line> lines, bool showDiscountCode = false)
        {
            Receipt.Append("<ul>");

            foreach (var line in lines)
            {
                Receipt.Append("<li>");
                AddLineItem(line, showDiscountCode);
                Receipt.Append("</li>");
            }

            Receipt.Append("</ul>");
            
            return this;
        }

        public override IReceiptBuilder WithSubTotal()
        {            
            Receipt.Append("<h3>");

            base.WithSubTotal();

            Receipt.Append("</h3>");

            return this;
        }

        public override IReceiptBuilder WithTax(decimal taxRate)
        {            
            Receipt.Append("<h3>");

            base.WithTax(taxRate);

            Receipt.Append("</h3>");

            return this;
        }

        public override IReceiptBuilder WithTotal()
        {
            Receipt.Append("<h2>");

            base.WithTotal();

            Receipt.Append("</h2>");

            return this;
        }

        public override string Build()
        {
            Receipt.Append("</body></html>");
            return base.Build();
        }        
    }
}