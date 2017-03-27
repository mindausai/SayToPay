using Newtonsoft.Json;

namespace SayToPay.REST.Contract
{
    public class PayToSayRequest
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string TimeStamp { get; set; }

        public string Lang { get; set; }

        public PayToSayResult Result { get; set; }

        public string Status { get; set; }

        public string SessionId { get; set; }

    }
}