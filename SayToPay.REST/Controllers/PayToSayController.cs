using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestSharp;
using SayToPay.REST.Contract;

namespace SayToPay.REST.Controllers
{
    public class PayToSayController : ApiController
    {
        public readonly RestClient client = new RestClient("https://api.qa.mobilepay.dk/merchant-restapi/");

        public PayToSayController()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 |
                                      SecurityProtocolType.Tls;
        }

        [HttpPost]
        [Route("api/PayToSay")]
        public async Task<HttpResponseMessage> PayToSay(PayToSayRequest request)
        {           
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var restRequest = new RestRequest("api/v1/transactions/28ec33bb-ca8d-4324-975b-b3699637ce97/sales?startDate=2017-03-20&endDate=2017-03-27&paymentPoints=df34b1b8-fb56-4b85-837a-43094eedf857", Method.GET);

            // easily add HTTP Headers
            restRequest.AddHeader("AuthenticatedUser", "138b5f5a-1897-4a54-8c15-e42e9340f621");
            restRequest.AddHeader("X-IBM-Client-Id", "2047f034-506e-4b2b-a40c-d2c62a0cf1cc");
            restRequest.AddHeader("X-IBM-Client-Secret", "X0wR2yM2bM4cN4dO3yY5rF5iN2oC7iN7wA8vX2uF8gV1iA7sE5");

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //IRestResponse<PaymentPointSales> response2 = client.Execute<PaymentPointSales>(restRequest);
            //var result = response2.Data;

            var response = await client.ExecuteTaskAsync<PaymentPointSales>(restRequest);
            var result = response.Data;

            var speech = "Your shop made " + result.TotalSalesAmount.Amount + " " + result.TotalSalesAmount.Currency +
                         ". Refunded " + result.TotalRefundsAmount.Amount + " " + result.TotalRefundsAmount.Currency +
                         ". Have made " + result.TotalTransactions + " transactions.";

            return
                Request.CreateResponse(new PayToSayResponse()
                {
                    Speech = speech,
                    DisplayText = speech,
                    Source = request.Result.Source
                });
        }
    }
}