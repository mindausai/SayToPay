using System;

namespace SayToPay.REST.Contract
{
    public class Transaction
    {
        public Guid PaymentId { get; set; }

        public DateTime Time { get; set; }

        public string Type { get; set; }

        public TransactionAmount Amount { get; set; }

        public Sender Sender { get; set; }

        public PaymentPoint PaymentPoint { get; set; }

        public string Message { get; set; }

        public TotalAmount Fee { get; set; }
    }
}