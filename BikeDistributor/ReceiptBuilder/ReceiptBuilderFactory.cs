using BikeDistributor.ReceiptBuilder.Builders;
using BikeDistributor.ReceiptBuilder.Enums;
using BikeDistributor.ReceiptBuilder.Interfaces;

namespace BikeDistributor.ReceiptBuilder
{
    public static class ReceiptBuilderFactory
    {
        public static IReceiptBuilder GetBuilder(ReceiptTypes type)
        {
            switch (type)
            {
                case ReceiptTypes.Html:
                    return new HtmlReceiptBuilder();

                default:
                    return new PaperReceiptBuilder();
            }
        }

        //public static IReceiptBuilder GetBuilderViaReflection(ReceiptTypes type)
        //{
        //    // get the enum custom attribute type and create an instance of it
        //    return (IReceiptBuilder)Activator.CreateInstance(GetBuilderType(type));
        //}

        //// use reflection to get the ReceiptType enum attribute property 'Type'
        //private static Type GetBuilderType(ReceiptTypes type)
        //{            
        //    var attr = (Builder) type.GetType()
        //        .GetMember(type.ToString())[0]
        //        .GetCustomAttributes(typeof(Builder), false)[0];
            
        //    return attr.Type;
        //}
    }
}