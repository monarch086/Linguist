using System;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Linguist.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender_, CommandEventArgs e_)
        {
            Exception exception = Server.GetLastError();
            if (exception is CryptographicException)
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}
