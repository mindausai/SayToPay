using System.Collections.Generic;

namespace SayToPay.REST.Contract
{
    public class PaymentPointSales
    {
        public TotalAmount TotalSalesAmount { get; set; }

        public TotalAmount TotalRefundsAmount { get; set; }

        public int TotalTransactions { get; set; }

        public object GroupedByDate { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}