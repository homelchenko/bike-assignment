using System.Collections.Generic;
using System.Linq;

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
            TextReceiptBuilder builder = new TextReceiptBuilder();

            GenerateReport(builder);

            return builder.GetReceipt();
        }

        public string HtmlReceipt()
        {
            HtmlReceiptBuilder builder = new HtmlReceiptBuilder();

            GenerateReport(builder);

            return builder.GetReceipt();
        }

        private void GenerateReport(ReceiptBuilder builder)
        {
            builder.AddHeader(_company);

            var totalAmmount = 0d;

            if (_lines.Any())
            {
                builder.StartLineItemsSection();
                foreach (var line in _lines)
                {
                    double lineItemAmmount = CalculateLineItemTotal(line);

                    builder.AddLineItemSection(line, lineItemAmmount);

                    totalAmmount += lineItemAmmount;
                }

                builder.EndLineItemsSection();
            }

            double tax = totalAmmount * TaxRate;

            builder.AddSubTotalSection(totalAmmount);
            builder.AddTaxSection(tax);
            builder.AddTotalSection(totalAmmount + tax);
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
