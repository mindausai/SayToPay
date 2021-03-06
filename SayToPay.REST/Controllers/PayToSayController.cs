﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using RestSharp;
using SayToPay.REST.Contract;
using SayToPay.REST.ErrorHandling;

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
            PaymentPointSales result = null;
            IRestResponse<PaymentPointSales> response = null;
            try
            {
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var restRequest =
                    new RestRequest(
                        "api/v1/transactions/28ec33bb-ca8d-4324-975b-b3699637ce97/sales?startDate=2017-03-20&endDate=2017-03-27&paymentPoints=df34b1b8-fb56-4b85-837a-43094eedf857",
                        Method.GET);

                // easily add HTTP Headers
                restRequest.AddHeader("AuthenticatedUser", "138b5f5a-1897-4a54-8c15-e42e9340f621");
                restRequest.AddHeader("X-IBM-Client-Id", "2047f034-506e-4b2b-a40c-d2c62a0cf1cc");
                restRequest.AddHeader("X-IBM-Client-Secret", "X0wR2yM2bM4cN4dO3yY5rF5iN2oC7iN7wA8vX2uF8gV1iA7sE5");

                // or automatically deserialize result
                // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                //IRestResponse<PaymentPointSales> response2 = client.Execute<PaymentPointSales>(restRequest);
                //var result = response2.Data;

                response = await client.ExecuteTaskAsync<PaymentPointSales>(restRequest);

                var exception = ValidateResponse<StandardErrorMessage>(response);

                if (exception != null)
                {
                    Request.CreateResponse(exception + " " + response.StatusCode);
                }

                result = response.Data;

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
            catch (Exception ex)
            {
                if (result != null)
                {
                    return Request.CreateResponse(result + " not null ");
                }
                return Request.CreateResponse(ex.Message + " null " + response.StatusCode + " Error: " + response.ErrorMessage);
            }
        }

        [HttpPost]
        [Route("api/PayToSayTest")]
        public async Task<HttpResponseMessage> PayToSayTest(PayToSayRequest request)
        {
                var speech = "Your shop made " + 1532 + " " + "DKK" +
             ". Refunded " + 15 + " " + "DKK" +
             ". Have made " + 8 + " transactions.";

                return
                    Request.CreateResponse(new PayToSayResponse()
                    {
                        Speech = speech,
                        DisplayText = speech,
                        Source = request.Result.Source
                    });
            
        }

        private HttpException ValidateResponse<TError>(IRestResponse response) where TError : IRestRetrieverErrorMessage
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                return new HttpException($"RestClient error: {response.ErrorMessage}. " +
                                        $"HttpStatusCode: {response.StatusCode}. " +
                                        $"Request: {response.Request.Resource}");
            }

            if ((int)response.StatusCode >= 200 && (int)response.StatusCode <= 299)
            {
                return null;
            }

            var errorMessage = TryGetErrorMessage<TError>(response) ??
                               "Error message: Unable to deserialize. " +
                               $"HttpStatusCode: {response.StatusCode}. " +
                               $"Request: {response.Request.Resource}";

            return new HttpException((int)response.StatusCode, errorMessage);
        }

        private string TryGetErrorMessage<TError>(IRestResponse response)
    where TError : IRestRetrieverErrorMessage
        {
            try
            {
                var e = JsonConvert.DeserializeObject<TError>(response.Content);
                return e.GetMessage();
            }
            catch
            {
                return null;
            }
        }
    }
}