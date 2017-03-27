namespace SayToPay.REST.Contract
{
    public class Metadata
    {
        public string IntentId { get; set; }

        public string WebHookUsed { get; set; }

        public string WebhookForSlotFillingUsed { get; set; }

        public string IntentName { get; set; }
    }
}