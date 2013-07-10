using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DANikitinTZ.Helpers
{
    public static class LangHelper
    {
        public static MvcHtmlString LangSwitcher(this UrlHelper url, string Name, RouteData routeData, string lang, string imageUrl)
        {
            var liTagBuilder = new TagBuilder("li");
            var aTagBuilder = new TagBuilder("a");
            var imgTagBuilder = new TagBuilder("img");
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            if (routeValueDictionary.ContainsKey("lang"))
            {
                if (routeData.Values["lang"] as string == lang)
                {
                    liTagBuilder.AddCssClass("active");
                }
                else
                {
                    routeValueDictionary["lang"] = lang;
                }
            }
            aTagBuilder.MergeAttribute("href", url.RouteUrl(routeValueDictionary));
            liTagBuilder.AddCssClass(lang);
            imgTagBuilder.MergeAttribute("src", imageUrl);
            imgTagBuilder.MergeAttribute("alt", Name);
            imgTagBuilder.MergeAttribute("title", Name);
            //aTagBuilder.SetInnerText(Name);
            aTagBuilder.InnerHtml = imgTagBuilder.ToString();
            liTagBuilder.InnerHtml = aTagBuilder.ToString();
            return new MvcHtmlString(liTagBuilder.ToString());
        }
    }
}