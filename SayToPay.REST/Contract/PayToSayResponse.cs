using Newtonsoft.Json;

namespace SayToPay.REST.Contract
{
    public class PayToSayResponse
    {
        [JsonProperty(PropertyName = "speech")]
        public string Speech { get; set; }

        [JsonProperty(PropertyName = "displayText")]
        public string DisplayText { get; set; }

        //public string Data { get; set; }

        //public string ContextOut { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        //public string FollowupEvent { get; set; }
    }
}