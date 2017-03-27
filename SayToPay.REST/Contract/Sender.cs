namespace SayToPay.REST.Contract
{
    public class Sender
    {
        private string _alias;

        public string Name { get; set; }

        public string Alias
        {
            get { return _alias; }
            set { _alias = $"xxxx {value.Substring(value.Length - 4)}"; }
        }
    }
}