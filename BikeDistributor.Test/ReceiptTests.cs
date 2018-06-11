using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.Test
{
    [TestClass]
    public class ReceiptTests
    {
        [TestMethod]
        public void Receipt_WhenThereIsNoItems_ShouldGenerateTextReceiptWithEmptyLineItemsSection()
        { 
            // Arrange
            Order order = new Order("Anywhere Bike Shop");

            // Act & Assert
            AssertTextReceiptForOrderIs(
                order,
@"Order Receipt for Anywhere Bike Shop
Sub-Total: $0.00
Tax: $0.00
Total: $0.00");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsOneThousandAndThereIsOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperTextReceipt384971774()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 1);

            // Act & Assert
            AssertTextReceiptForOrderIs(
                order,
@"Order Receipt for Anywhere Bike Shop
	1 x Any brand Any model = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsOneThousandAndThereAreTwentyItems_ShouldApplyTenPercentDiscountsAndGenerateProperTextReceipt949267744()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 20);

            // Act & Assert
            AssertTextReceiptForOrderIs(
                order,
@"Order Receipt for Anywhere Bike Shop
	20 x Any brand Any model = $18,000.00
Sub-Total: $18,000.00
Tax: $1,305.00
Total: $19,305.00");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsTwoThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperTextReceipt317347402()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 1);

            // Act & Arrange
            AssertTextReceiptForOrderIs(
                order,
@"Order Receipt for Anywhere Bike Shop
	1 x Any brand Any model = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsTwoThousandAndThereAreTenItems_ShouldApplyTwentyPercentDiscountAndGenerateProperTextReceipt1425213916()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 10);

            // Act & Arrange
            AssertTextReceiptForOrderIs(
                order,
                @"Order Receipt for Anywhere Bike Shop
	10 x Any brand Any model = $16,000.00
Sub-Total: $16,000.00
Tax: $1,160.00
Total: $17,160.00");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsFiveThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperTextReceipt1648054349()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 1);

            // Act & Arrange
            AssertTextReceiptForOrderIs(
                order, 
@"Order Receipt for Anywhere Bike Shop
	1 x Any brand Any model = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsFiveThousandAndThereAreFiveItems_ShouldApplyTwentyPercentDiscountAndGenerateProperTextReceipt295739202()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 5);

            // Act & Arrange
            AssertTextReceiptForOrderIs(
                order, 
@"Order Receipt for Anywhere Bike Shop
	5 x Any brand Any model = $20,000.00
Sub-Total: $20,000.00
Tax: $1,450.00
Total: $21,450.00");
        }

        [TestMethod]
        public void Receipt_WhenThereAreTwoLineItemsForDiscounts_ShouldGenerateProperTextReceipt1827902231()
        { 
            // Arrange
            Bike firstBike = new Bike("First brand", "First model", Bike.OneThousand);
            Bike secondBike = new Bike("Second brand", "Second model", Bike.FiveThousand);

            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(firstBike, 20));
            order.AddLine(new Line(secondBike, 5));

            // Act & Arrange
            AssertTextReceiptForOrderIs(
                order,
@"Order Receipt for Anywhere Bike Shop
	20 x First brand First model = $18,000.00
	5 x Second brand Second model = $20,000.00
Sub-Total: $38,000.00
Tax: $2,755.00
Total: $40,755.00");
        }

        [TestMethod]
        public void HtmlReceipt_WhenThereIsNoItems_ShouldGenerateHtmlReceiptWithEmptyLineItemsSection317475436()
        { 
            // Arrange
            Order order = new Order("Anywhere Bike Shop");

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><h3>Sub-Total: $0.00</h3><h3>Tax: $0.00</h3><h2>Total: $0.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsOneThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperHtmlReceipt595071742()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 1);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Any brand Any model = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsOneThousandAndThereAreTwentyItems_ShouldApplyTenPercentDiscountsAndGenerateProperHtmlReceipt1222219759()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 20);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>20 x Any brand Any model = $18,000.00</li></ul><h3>Sub-Total: $18,000.00</h3><h3>Tax: $1,305.00</h3><h2>Total: $19,305.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsTwoThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperHtmlReceipt1338653730()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 1);

            // Act
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Any brand Any model = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsTwoThousandAndThereAreTenItems_ShouldApplyTwentyPercentDiscountAndGenerateProperWebReceipt256534842()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 10);

            // Act
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>10 x Any brand Any model = $16,000.00</li></ul><h3>Sub-Total: $16,000.00</h3><h3>Tax: $1,160.00</h3><h2>Total: $17,160.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsFiveThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperHtmlReceipt1382046574()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 1);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Any brand Any model = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsFiveThousandAndThereAreFiveItems_ShouldApplyTwentyPercentDiscountAndGenerateProperHtmlReceipt345206068()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 5);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>5 x Any brand Any model = $20,000.00</li></ul><h3>Sub-Total: $20,000.00</h3><h3>Tax: $1,450.00</h3><h2>Total: $21,450.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenThereAreTwoLineItemsForDiscounts_ShouldGenerateProperHtmlReceipt2074476967()
        { 
            // Arrange
            Bike firstBike = new Bike("First brand", "First model", Bike.OneThousand);
            Bike secondBike = new Bike("Second brand", "Second model", Bike.FiveThousand);

            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(firstBike, 20));
            order.AddLine(new Line(secondBike, 5));

            // Act & Arrange
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>20 x First brand First model = $18,000.00</li><li>5 x Second brand Second model = $20,000.00</li></ul><h3>Sub-Total: $38,000.00</h3><h3>Tax: $2,755.00</h3><h2>Total: $40,755.00</h2></body></html>");
        }

        private static Order CreateOneLineOrderFor(int price, int quantity)
        {
            Bike bike = new Bike("Any brand", "Any model", price);

            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(bike, quantity));

            return order;
        }

        private static void AssertTextReceiptForOrderIs(Order order, string expectedReceipt)
        {
            // Act
            TextReceiptBuilder builder = new TextReceiptBuilder();

            // Act
            order.GenerateReceipt(builder);

            // Assert
            Assert.AreEqual(expectedReceipt, builder.GetReceipt());
        }

        private static void AssertHtmlReceiptForOrderIs(Order order, string expectedReceipt)
        {
            // Arrange
            var builder = new HtmlReceiptBuilder();

            // Act
            order.GenerateReceipt(builder);

            // Assert
            Assert.AreEqual(expectedReceipt, builder.GetReceipt());
        }
    }
}
