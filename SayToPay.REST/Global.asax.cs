using System.Web;
using System.Web.Http;
using SayToPay.REST.App_Start;

namespace SayToPay.REST
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}