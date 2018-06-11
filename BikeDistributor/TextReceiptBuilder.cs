using System;
using System.Text;

namespace BikeDistributor
{
    internal class TextReceiptBuilder : ReceiptBuilder
    {
        private readonly StringBuilder _receipt = new StringBuilder();

        public override void AddHeader(string company)
        {
            string receiptHeader = $"Order Receipt for {company}{Environment.NewLine}";

            _receipt.Append(receiptHeader);
        }

        public override void AddLineItemSection(Line line, double lineItemTotal)
        {
            string lineItem = $"\t{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {lineItemTotal:C}";
            
            _receipt.AppendLine(lineItem);
        }

        public override void StartLineItemsSection()
        {
        }

        public override void AddSubTotalSection(double subTotal)
        {
            string subTotalSection = $"Sub-Total: {subTotal:C}";

            _receipt.AppendLine(subTotalSection);
        }

        public override void EndLineItemsSection()
        {
        }

        public override void AddTaxSection(double tax)
        {
            string taxSection = $"Tax: {tax:C}";

            _receipt.AppendLine(taxSection);
        }

        public override void AddTotalSection(double total)
        {
            string totalSection = $"Total: {total:C}";

            _receipt.Append(totalSection);
        }

        public string GetReceipt() => _receipt.ToString();
    }
}