using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly static Bike Defy = new Bike("Giant", "Defy 1", Bike.OneThousand);
        private readonly static Bike Elite = new Bike("Specialized", "Venge Elite", Bike.TwoThousand);
        private readonly static Bike DuraAce = new Bike("Specialized", "S-Works Venge Dura-Ace", Bike.FiveThousand);

        [TestMethod]
        public void Receipt_WhenOrderContainsOneDefy_ShouldGenerateProperTextReceipt()
        {
            // Arrange
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Defy, 1));
            
            // Act
            string textReceipt = order.Receipt();

            // Assert
            Assert.AreEqual(
@"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50",
                textReceipt);
        }

        [TestMethod]
        public void Receipt_WhenOrderContainsOneElite_ShouldGenerateProperTextReceipt()
        {
            // Arrange
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Elite, 1));
            
            // Act
            string textReceive = order.Receipt();

            // Assert
            Assert.AreEqual(
@"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00",
                textReceive);
        }

        [TestMethod]
        public void Receipt_WhenOrderContainsOneDuraAce_ShouldGenerateProperTextReceipt()
        {
            // Arrange
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(DuraAce, 1));
            
            // Act
            string textReceipt = order.Receipt();

            // Arrange
            Assert.AreEqual(
@"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50",
                textReceipt);
        }

        [TestMethod]
        public void Receipt_WhenOrderContainsOneDefy_ShouldGenerateProperHtmlReceipt()
        {
            // Arrange
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Defy, 1));
            
            // Act
            string htmlReceipt = order.HtmlReceipt();

            // Assert
            Assert.AreEqual(
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>",
                htmlReceipt);
        }

        [TestMethod]
        public void Receipt_WhenOrderContainsOneElite_ShouldGenerateProperHtmlReceipt()
        {
            // Arrange
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Elite, 1));
            
            // Act
            string htmlReceipt = order.HtmlReceipt();

            // Assert
            Assert.AreEqual(
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>",
                htmlReceipt);
        }

        [TestMethod]
        public void Receipt_WhenOrderContainsOneDuraAce_ShouldGenerateProperHtmlReceipt()
        {
            // Arrange
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(DuraAce, 1));

            // Act
            string htmlReceipt = order.HtmlReceipt();

            // Arrange
            Assert.AreEqual(
                @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>",
                htmlReceipt);
        }
    }
}


