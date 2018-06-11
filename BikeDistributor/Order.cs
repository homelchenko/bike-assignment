using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
    public class Order
    {
        private const double TaxRate = .0725d;
        private readonly IList<Line> _lines = new List<Line>();
        private readonly string _company;

        public Order(string company)
        {
            this._company = company;
        }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string Receipt()
        {
            var totalAmount = 0d;

            var result = new StringBuilder();

            AddHeaderToTextReceipt(result);

            foreach (var line in _lines)
            {
                double lineItemAmmount = CalculateLineItemTotal(line);

                AddLineItemToTextReceipt(result, line, lineItemAmmount);

                totalAmount += lineItemAmmount;
            }

            double tax = totalAmount * TaxRate;

            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));

            return result.ToString();
        }

        private void AddHeaderToTextReceipt(StringBuilder result)
        {
            string receiptHeader = string.Format("Order Receipt for {0}{1}", _company, Environment.NewLine);

            result.Append(receiptHeader);
        }

        private static void AddLineItemToTextReceipt(StringBuilder result, Line line, double lineItemAmmount)
        {
            string lineItem = string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Bike.Brand, line.Bike.Model, lineItemAmmount.ToString("C"));
            
            result.AppendLine(lineItem);
        }

        public string HtmlReceipt()
        {
            var totalAmount = 0d;

            var result = new StringBuilder();
            result.Append("<html><body>");

            AddHeaderToHtmlReceipt(result);

            if (_lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in _lines)
                {
                    double lineItemAmmount = CalculateLineItemTotal(line);
                    
                    AddLineItemToHtmlReceipt(result, line, lineItemAmmount);
                    
                    totalAmount += lineItemAmmount;
                }
                result.Append("</ul>");
            }

            double tax = totalAmount * TaxRate;

            AddSubTotalToHtmlReceipt(result, totalAmount);
            AddTaxSectionToHtmlReceipt(result, tax);
            AddTotalToHtmlReceipt(result, totalAmount + tax);
            
            result.Append("</body></html>");

            return result.ToString();
        }

        private static void AddTotalToHtmlReceipt(StringBuilder result, double total)
        {
            string totalSection = $"<h2>Total: {total:C}</h2>";

            result.Append(totalSection);
        }

        private static void AddTaxSectionToHtmlReceipt(StringBuilder result, double tax)
        {
            string taxSection = $"<h3>Tax: {tax:C}</h3>";

            result.Append(taxSection);
        }

        private static void AddSubTotalToHtmlReceipt(StringBuilder result, double totalAmount)
        {
            string subTotalSection = $"<h3>Sub-Total: {totalAmount:C}</h3>";

            result.Append(subTotalSection);
        }

        private void AddHeaderToHtmlReceipt(StringBuilder result)
        {
            string receiptHeader = $"<h1>Order Receipt for {_company}</h1>";

            result.Append(receiptHeader);
        }

        private static void AddLineItemToHtmlReceipt(StringBuilder result, Line line, double lineItemAmmount)
        {
            string lineItem =
                $"<li>{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {lineItemAmmount:C}</li>";

            result.Append(lineItem);
        }

        private static double CalculateLineItemTotal(Line line)
        {
            switch (line.Bike.Price)
            {
                case Bike.OneThousand:
                    return ApplyDiscount(line.Price, CalculateDiscountForOneThousand(line));
                case Bike.TwoThousand:
                    return ApplyDiscount(line.Price, CalculateDiscountForTwoThousand(line));
                case Bike.FiveThousand:
                    return ApplyDiscount(line.Price, CalculateDiscountForFiveThousand(line));
                default:
                    return 0d;
            }
        }

        private static double CalculateDiscountForOneThousand(Line line)
        {
            return CalculateAboveQuantityThresholdDiscount(line, 20, .1d);
        }

        private static double CalculateDiscountForTwoThousand(Line line)
        {
            return CalculateAboveQuantityThresholdDiscount(line, 10, .2d);
        }

        private static double CalculateDiscountForFiveThousand(Line line)
        {
            return CalculateAboveQuantityThresholdDiscount(line, 5, .2d);
        }

        private static double CalculateAboveQuantityThresholdDiscount(Line line, int quantityThreshold, double discountAfterThreshold)
        {
            var discount = 0d;
            if (line.Quantity >= quantityThreshold)
            {
                discount = discountAfterThreshold;
            }

            return discount;
        }

        private static double ApplyDiscount(int price, double discount)
        {
            return price * (1 - discount);
        }
    }
}
