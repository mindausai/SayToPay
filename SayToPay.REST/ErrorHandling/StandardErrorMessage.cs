namespace SayToPay.REST.ErrorHandling
{
    public class StandardErrorMessage : IRestRetrieverErrorMessage
    {
        public string CorrelationId { get; set; }

        public string ErrorType { get; set; }

        public string Message { get; set; }

        public string GetMessage()
        {
            return Message;
        }
    }
}