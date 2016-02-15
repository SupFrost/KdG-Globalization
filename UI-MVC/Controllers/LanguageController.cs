using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC.UI.Web.MVC.Controllers
{
    public class LanguageController : Controller
    {
        public void Set(String lang)
        {
            // Set culture to use next
            CultureAttribute.SavePreferredCulture(HttpContext.Response, lang);

            // Return to the calling URL (or go to the site's home page)
            if (HttpContext.Request.UrlReferrer != null)
                HttpContext.Response.Redirect(HttpContext.Request.UrlReferrer.AbsolutePath);
        }
    }
}