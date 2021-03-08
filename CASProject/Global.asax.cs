using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CASProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Session_Start(object sender, EventArgs e)
        {
          //  Session["Name"] = "IBM";
            //Session["UserId"] = 111;
            Session["Myuser"] = null;
            Session["Myrole"] = null;
            Session["Myname"] = null;

            /*int app = 0;
            Application.Lock();
            app = (int)Application["HitCount"];
            app++;
            Application["Hit Counter"] = app;
*/
            // Application.UnLock();
        }
      
            protected void Application_Start()
            {
                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                // BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
            protected void Application_AuthenticateRequest(Object sender, EventArgs e)
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket != null && !authTicket.Expired)
                    {
                        var roles = authTicket.UserData.Split(',');
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                    }
                }
            }

        }


    
}
