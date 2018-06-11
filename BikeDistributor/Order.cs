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
            var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", _company, Environment.NewLine));
            foreach (var line in _lines)
            {
                double lineItemAmmount = CalculateLineItemTotal(line);

                result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Bike.Brand, line.Bike.Model, lineItemAmmount.ToString("C")));

                totalAmount += lineItemAmmount;
            }
            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));
            return result.ToString();
        }

        private static double CalculateLineItemTotal(Line line)
        {
            var thisAmount = 0d;
            switch (line.Bike.Price)
            {
                case Bike.OneThousand:
                    double discountForOneThousand = CalculateDiscountForOneThousand(line);

                    thisAmount = ApplyDiscount(line.Price, discountForOneThousand);

                    break;
                case Bike.TwoThousand:
                    double discountForTwoThousand = CalculateDiscountForTwoThousand(line);

                    thisAmount = ApplyDiscount(line.Price, discountForTwoThousand);

                    break;
                case Bike.FiveThousand:
                    double discountForFiveThousand = CalculateDiscountForFiveThousand(line);

                    thisAmount = ApplyDiscount(line.Price, discountForFiveThousand);

                    break;
            }

            return thisAmount;
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

        public string HtmlReceipt()
        {
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", _company));
            if (_lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in _lines)
                {
                    var thisAmount = 0d;
                    switch (line.Bike.Price)
                    {
                        case Bike.OneThousand:
                            if (line.Quantity >= 20)
                                thisAmount += line.Quantity*line.Bike.Price*.9d;
                            else
                                thisAmount += line.Quantity*line.Bike.Price;
                            break;
                        case Bike.TwoThousand:
                            if (line.Quantity >= 10)
                                thisAmount += line.Quantity*line.Bike.Price*.8d;
                            else
                                thisAmount += line.Quantity*line.Bike.Price;
                            break;
                        case Bike.FiveThousand:
                            if (line.Quantity >= 5)
                                thisAmount += line.Quantity*line.Bike.Price*.8d;
                            else
                                thisAmount += line.Quantity*line.Bike.Price;
                            break;
                    }
                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
                result.Append("</ul>");
            }
            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append("</body></html>");
            return result.ToString();
        }

    }
}
