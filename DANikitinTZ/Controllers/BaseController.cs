using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using DANikitinTZ.Helpers;

namespace DANikitinTZ.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

       
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            string cultureName = null;

            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                cultureName = requestContext.RouteData.Values["lang"] as string;
                //cultureLang = Repository.Languages.FirstOrDefault(p => p.Code == CurrentLangCode);
                
                cultureName = CultureHelper.GetValidCulture(cultureName); 
                var ci = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            base.Initialize(requestContext);
        }
        

    }
}
