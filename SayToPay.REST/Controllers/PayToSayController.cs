using System.Net.Http;
using System.Web.Http;
using SayToPay.REST.Contract;

namespace SayToPay.REST.Controllers
{
    public class PayToSayController : ApiController
    {
        [HttpPost]
        [Route("api/PayToSay")]
        public HttpResponseMessage PayToSay(PayToSayRequest request)
        {
            return
                Request.CreateResponse(new PayToSayResponse()
                {
                    Speech = "Test Test",
                    DisplayText = "Test Display",
                    Source = request.Result.Source
                });
        }
    }
}