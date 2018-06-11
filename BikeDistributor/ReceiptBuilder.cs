using System.Text;

namespace BikeDistributor
{
    internal abstract class ReceiptBuilder
    {
        public abstract void AddHeader(string company);

        public abstract void AddLineItemSection(Line line, double lineItemTotal);

        public abstract void StartLineItemsSection();

        public abstract void AddSubTotalSection(double subTotal);

        public abstract void EndLineItemsSection();

        public abstract void AddTaxSection(double tax);

        public abstract void AddTotalSection(double total);
    }

    internal class HtmlReceiptBuilder : ReceiptBuilder
    {
        private readonly StringBuilder _receipt = new StringBuilder();
        
        public override void AddHeader(string company)
        {
            _receipt.Append("<html><body>");
            
            string receiptHeader = $"<h1>Order Receipt for {company}</h1>";

            _receipt.Append(receiptHeader);
        }

        public override void AddLineItemSection(Line line, double lineItemTotal)
        {
            string lineItem = $"<li>{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {lineItemTotal:C}</li>";

            _receipt.Append(lineItem);
        }

        public override void StartLineItemsSection()
        {
            _receipt.Append("<ul>");
        }

        public override void AddSubTotalSection(double subTotal)
        {
            string subTotalSection = $"<h3>Sub-Total: {subTotal:C}</h3>";

            _receipt.Append(subTotalSection);
        }

        public override void EndLineItemsSection()
        {
            _receipt.Append("</ul>");
        }

        public override void AddTaxSection(double tax)
        {
            string taxSection = $"<h3>Tax: {tax:C}</h3>";

            _receipt.Append(taxSection);
        }

        public override void AddTotalSection(double total)
        {
            string totalSection = $"<h2>Total: {total:C}</h2>";

            _receipt.Append(totalSection);

            _receipt.Append("</body></html>");
        }

        public string GetReceipt() => _receipt.ToString();
    }
}
