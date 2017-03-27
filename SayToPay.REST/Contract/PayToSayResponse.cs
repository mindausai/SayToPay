namespace SayToPay.REST.Contract
{
    public class PayToSayResponse
    {
        public string Speech { get; set; }

        public string DisplayText { get; set; }

        public string Data { get; set; }

        public string ContextOut { get; set; }

        public string Source { get; set; }

        public string FollowupEvent { get; set; }
    }
}