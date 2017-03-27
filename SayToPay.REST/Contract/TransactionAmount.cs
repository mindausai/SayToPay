namespace SayToPay.REST.Contract
{
    public class TransactionAmount
    {
        public decimal Amount { get; set; }

        public decimal RefundAvailableAmount { get; set; }

        public string Currency { get; set; }
    }
}