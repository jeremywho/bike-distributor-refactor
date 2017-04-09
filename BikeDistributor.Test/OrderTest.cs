using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using BikeDistributor.Discount;
using BikeDistributor.ReceiptBuilder.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BikeDistributor.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class OrderTest
    {
        private readonly DiscountRulesEngine<Line> _discountRulesEngine;
        private readonly Dictionary<string, Bike> _bikes;

        public OrderTest()
        {
            _bikes = LoadJsonFile<Dictionary<string, Bike>>("testBikes.json");

            var rules = LoadJsonFile<List<Discount<Line>>>("testRules.json");
            _discountRulesEngine = new DiscountRulesEngine<Line>(rules);
        }
         
        [TestMethod]
        public void ReceiptOneDefy()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["Defy"], 1));
            Assert.AreEqual(ResultStatementOneDefy, order.Complete().Receipt(ReceiptTypes.Paper));
        }

        private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void ReceiptOneDefyShowDiscountCode()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["Defy"], 1));
            Assert.AreEqual(ResultStatementOneDefyShowDiscountCode, order.Complete().Receipt(ReceiptTypes.Paper, true));
        }

        private const string ResultStatementOneDefyShowDiscountCode = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void ReceiptOneElite()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["Elite"], 1));
            Assert.AreEqual(ResultStatementOneElite, order.Complete().Receipt(ReceiptTypes.Paper));
        }

        private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";

        [TestMethod]
        public void ReceiptOneDuraAce()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["DuraAce"], 1));
            Assert.AreEqual(ResultStatementOneDuraAce, order.Complete().Receipt(ReceiptTypes.Paper));
        }

        private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";

        [TestMethod]
        public void ReceiptTenDuraAce()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["DuraAce"], 10));
            Assert.AreEqual(ResultStatementTenDuraAce, order.Complete().Receipt(ReceiptTypes.Paper));
        }

        private const string ResultStatementTenDuraAce = @"Order Receipt for Anywhere Bike Shop
	10 x Specialized S-Works Venge Dura-Ace = $40,000.00
Sub-Total: $40,000.00
Tax: $2,900.00
Total: $42,900.00";

        [TestMethod]
        public void ReceiptOneLastYearDuraAce()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["LastYearDuraAce"], 1));
            Assert.AreEqual(ResultStatementLastYearDuraAce, order.Complete().Receipt(ReceiptTypes.Paper));
        }

        private const string ResultStatementLastYearDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $2,500.00
Sub-Total: $2,500.00
Tax: $181.25
Total: $2,681.25";

        [TestMethod]
        public void ReceiptOneLastYearDuraAceWithShowDiscountCode()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["LastYearDuraAce"], 1));
            Assert.AreEqual(ResultStatementLastYearDuraAceShowDiscountCode, order.Complete().Receipt(ReceiptTypes.Paper, true));
        }

        private const string ResultStatementLastYearDuraAceShowDiscountCode = @"Order Receipt for Anywhere Bike Shop
	Discount applied: SAVE2016 - 1 x Specialized S-Works Venge Dura-Ace = $2,500.00
Sub-Total: $2,500.00
Tax: $181.25
Total: $2,681.25";

        [TestMethod]
        public void HtmlReceiptOneDefy()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["Defy"], 1));
            Assert.AreEqual(HtmlResultStatementOneDefy, order.Complete().Receipt(ReceiptTypes.Html));
        }

        private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneElite()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["Elite"], 1));
            Assert.AreEqual(HtmlResultStatementOneElite, order.Complete().Receipt(ReceiptTypes.Html));
        }

        private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneDuraAce()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["DuraAce"], 1));
            Assert.AreEqual(HtmlResultStatementOneDuraAce, order.Complete().Receipt(ReceiptTypes.Html));
        }

        private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptTenDuraAce()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["DuraAce"], 10));
            Assert.AreEqual(HtmlResultStatementTenDuraAce, order.Complete().Receipt(ReceiptTypes.Html));
        }

        private const string HtmlResultStatementTenDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>10 x Specialized S-Works Venge Dura-Ace = $40,000.00</li></ul><h3>Sub-Total: $40,000.00</h3><h3>Tax: $2,900.00</h3><h2>Total: $42,900.00</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneLastYearDuraAce()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["LastYearDuraAce"], 1));
            Assert.AreEqual(HtmlResultStatementLastYearDuraAce, order.Complete().Receipt(ReceiptTypes.Html));
        }

        private const string HtmlResultStatementLastYearDuraAce =
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $2,500.00</li></ul><h3>Sub-Total: $2,500.00</h3><h3>Tax: $181.25</h3><h2>Total: $2,681.25</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneLastYearDuraAceWithShowDiscountCode()
        {
            var order = new Order("Anywhere Bike Shop", _discountRulesEngine);
            order.AddLine(new Line(_bikes["LastYearDuraAce"], 1));
            Assert.AreEqual(HtmlResultStatementLastYearDuraAceShowDiscountCode, order.Complete().Receipt(ReceiptTypes.Html, true));
        }

        private const string HtmlResultStatementLastYearDuraAceShowDiscountCode =
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>Discount applied: SAVE2016 - 1 x Specialized S-Works Venge Dura-Ace = $2,500.00</li></ul><h3>Sub-Total: $2,500.00</h3><h3>Tax: $181.25</h3><h2>Total: $2,681.25</h2></body></html>";

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException), "Rule.CreateComparisonExpression allowed invalid operation")]
        public void InvalidRuleOperationThrowsArithmeticException()
        {
            var rule = new Rule("", "NOT_AN_OPERATION", "");
            Rule.CreateComparisonExpression<Line>(rule, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Rule.CreateComparisonExpression allowed invalid operation")]
        public void InvalidPropertyTypeThrowsArgumentException()
        {
            var rule = new Rule("Bob", "Equal", "40");
            Rule.CreateComparisonExpression<Line>(rule, null);
        }

        public T LoadJsonFile<T>(string file)
        {
            using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), file)))
            {
                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
        }
    }
}