using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SC.UI.Web.MVC.Controllers
{
    public class BaseController : Controller
    {

        public BaseController()
        {
//            var cultureString =  HttpContext.Request.Params["culture"];
//            var cultureInfo = CultureInfo.CreateSpecificCulture(cultureString);
//            Thread.CurrentThread.CurrentCulture = cultureInfo;
//            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
//            string cultureName = null;
//
//            // Attempt to read the culture cookie from Request
//            HttpCookie cultureCookie = Request.Cookies["_culture"];
//            if (cultureCookie != null)
//                cultureName = cultureCookie.Value;
//            else
//                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
//                    ? Request.UserLanguages[0]
//                    : // obtain it from HTTP header AcceptLanguages
//                    null;
//
//            // Modify current thread's cultures            
//            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
//            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
//
            return base.BeginExecuteCore(callback, state);
        }
    }
}