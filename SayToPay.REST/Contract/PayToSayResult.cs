namespace SayToPay.REST.Contract
{
    public class PayToSayResult
    {
        public string Source { get; set; }

        public string ResolvedQuery { get; set; }

        public string Action { get; set; }

        public bool ActionIncomplete { get; set; }

        public Parameters Parameters { get; set; }

        public string[] Contexts { get; set; }

        public Fulfillment Fulfillment { get; set; }

        public decimal Score { get; set; }

        public Metadata Metadata { get; set; }
    }
}