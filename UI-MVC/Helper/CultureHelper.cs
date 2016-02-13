using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Threading;
using System.Globalization;

namespace SC.UI.Web.MVC.Helper
{


  namespace MvcLocalization.Helper
  {
    public class CultureHelper
    {
      protected HttpSessionState session;

      //constructor   
      public CultureHelper(HttpSessionState httpSessionState)
      {
        session = httpSessionState;
      }
      // Properties  
      public static int CurrentCulture
      {
        get
        {
          switch (Thread.CurrentThread.CurrentUICulture.Name)
          {
            case "en-GB":
            return 0; break;
            case "en-US":
              return 1; break;
            case "nl-BE":
              return 2; break;
            case "nl-NL":
              return 3; break;
            case "fr-FR":
              return 4; break;
            case "de-DE":
              return 5; break;
             default:
              return 0; break;
          }
        }
        set
        {

          switch (value)
          {
            case 0:
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB"); break;
            case 1:
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); break;
            case 2:
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-BE"); break;
            case 3:
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL"); break;
            case 4:
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR"); break;
            case 5:
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE"); break;

            default:
              Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture; break;
          }

          Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

        }
      }
    }
  }
}

//http://www.c-sharpcorner.com/UploadFile/4d9083/globalization-and-localization-in-Asp-Net-mvc-4/