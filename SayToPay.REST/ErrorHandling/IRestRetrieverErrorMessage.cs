namespace SayToPay.REST.ErrorHandling
{
    public interface IRestRetrieverErrorMessage
    {
        /// <summary>
        ///     Gets the message.
        /// </summary>
        /// <returns>Error message</returns>
        string GetMessage();
    }
}