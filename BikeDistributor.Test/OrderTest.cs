using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void Receipt_WhenPriceIsOneThousandAndThereIsOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperTextReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 1);

            // Act & Assert
            AssertTextReceiptForOrderIs(order,
@"Order Receipt for Anywhere Bike Shop
	1 x Any brand Any model = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsOneThousandAndThereAreTwentyItems_ShouldApplyTenPercentDiscountsAndGenerateProperTextReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 20);

            // Act & Assert
            AssertTextReceiptForOrderIs(order,
@"Order Receipt for Anywhere Bike Shop
	20 x Any brand Any model = $18,000.00
Sub-Total: $18,000.00
Tax: $1,305.00
Total: $19,305.00");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsTwoThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperTextReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 1);

            // Act & Arrange
            AssertTextReceiptForOrderIs(order,
@"Order Receipt for Anywhere Bike Shop
	1 x Any brand Any model = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsTwoThousandAndThereAreTenItems_ShouldApplyTwentyPercentDiscountAndGenerateProperTextReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 10);

            // Act & Arrange
            AssertTextReceiptForOrderIs(order,
                @"Order Receipt for Anywhere Bike Shop
	10 x Any brand Any model = $16,000.00
Sub-Total: $16,000.00
Tax: $1,160.00
Total: $17,160.00");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsFiveThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperTextReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 1);

            // Act & Arrange
            AssertTextReceiptForOrderIs(order, 
@"Order Receipt for Anywhere Bike Shop
	1 x Any brand Any model = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50");
        }

        [TestMethod]
        public void Receipt_WhenPriceIsFiveThousandAndThereAreFiveItems_ShouldApplyTwentyPercentDiscountAndGenerateProperTextReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 5);

            // Act & Arrange
            AssertTextReceiptForOrderIs(order, 
@"Order Receipt for Anywhere Bike Shop
	5 x Any brand Any model = $20,000.00
Sub-Total: $20,000.00
Tax: $1,450.00
Total: $21,450.00");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsOneThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperHtmlReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 1);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Any brand Any model = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsOneThousandAndThereAreTwentyItems_ShouldApplyTenPercentDiscountsAndGenerateProperHtmlReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.OneThousand, 20);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>20 x Any brand Any model = $18,000.00</li></ul><h3>Sub-Total: $18,000.00</h3><h3>Tax: $1,305.00</h3><h2>Total: $19,305.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsTwoThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperHtmlReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 1);

            // Act
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Any brand Any model = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsTwoThousandAndThereAreTenItems_ShouldApplyTwentyPercentDiscountAndGenerateProperWebReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.TwoThousand, 10);

            // Act
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>10 x Any brand Any model = $16,000.00</li></ul><h3>Sub-Total: $16,000.00</h3><h3>Tax: $1,160.00</h3><h2>Total: $17,160.00</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsFiveThousandAndThereIsOnlyOneItem_ShouldNotApplyAnyDiscountsAndGenerateProperHtmlReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 1);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Any brand Any model = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>");
        }

        [TestMethod]
        public void HtmlReceipt_WhenPriceIsFiveThousandAndThereAreFiveItems_ShouldApplyTwentyPercentDiscountAndGenerateProperHtmlReceipt()
        {
            // Arrange
            Order order = CreateOneLineOrderFor(Bike.FiveThousand, 5);

            // Act & Assert
            AssertHtmlReceiptForOrderIs(
                order,
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>5 x Any brand Any model = $20,000.00</li></ul><h3>Sub-Total: $20,000.00</h3><h3>Tax: $1,450.00</h3><h2>Total: $21,450.00</h2></body></html>");
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
            string textReceipt = order.Receipt();

            // Assert
            Assert.AreEqual(expectedReceipt, textReceipt);
        }

        private static void AssertHtmlReceiptForOrderIs(Order order, string expectedReceipt)
        {
            // Act
            string htmlReceipt = order.HtmlReceipt();

            // Assert
            Assert.AreEqual(expectedReceipt, htmlReceipt);
        }
    }
}