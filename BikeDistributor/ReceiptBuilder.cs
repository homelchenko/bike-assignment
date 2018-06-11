namespace BikeDistributor
{
    public abstract class ReceiptBuilder
    {
        public abstract void AddHeader(string company);

        public abstract void AddLineItemSection(Line line, double lineItemTotal);

        public abstract void StartLineItemsSection();

        public abstract void AddSubTotalSection(double subTotal);

        public abstract void EndLineItemsSection();

        public abstract void AddTaxSection(double tax);

        public abstract void AddTotalSection(double total);
    }
}
