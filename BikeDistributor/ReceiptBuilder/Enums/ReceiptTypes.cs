using BikeDistributor.ReceiptBuilder.Builders;
using BikeDistributor.ReceiptBuilder.CustomAttributes;

namespace BikeDistributor.ReceiptBuilder.Enums
{
    public enum ReceiptTypes
    {
        //[Builder(Type = typeof(PaperReceiptBuilder))]
        Paper = 1,

        //[Builder(Type = typeof(HtmlReceiptBuilder))]
        Html = 2
    }
}