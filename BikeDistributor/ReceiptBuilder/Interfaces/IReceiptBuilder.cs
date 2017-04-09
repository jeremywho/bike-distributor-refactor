using System.Collections.Generic;

namespace BikeDistributor.ReceiptBuilder.Interfaces
{
    public interface IReceiptBuilder
    {
        IReceiptBuilder ForCompany(string company);
        IReceiptBuilder WithHeader();
        IReceiptBuilder AddLineItem(Line line, bool showDiscountCode = false);
        IReceiptBuilder WithLineItems(IList<Line> lines, bool showDiscountCode = false);
        IReceiptBuilder WithSubTotal();
        IReceiptBuilder WithTax(decimal taxRate);
        IReceiptBuilder WithTotal();
        IReceiptBuilder WithFooter();
        string Build();
    }
}