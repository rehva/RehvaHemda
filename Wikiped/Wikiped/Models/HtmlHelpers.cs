using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Wikiped.Models
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString Ratings(this HtmlHelper helper, double rating, int idClanka, int Iduser)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<span class='rating' rating='{0}' post='{1}' title='Click to cast vote'>", rating, idClanka);
            string formatStr = "<img src='/Images/{0}' alt='star' width='15' height='12' class='star' value='{1}' />";

            for (Double i = .5; i <= 5.0; i = i + .5)
            {
                if (i <= rating)
                {
                    sb.AppendFormat(formatStr, (i * 2) % 2 == 1 ? "leftON.gif" : "rightON.gif", i);
                }
                else
                {
                    sb.AppendFormat(formatStr, (i * 2) % 2 == 1 ? "leftOFF.gif" : "rightOFF.gif", i);
                }
            }
            sb.AppendFormat(" <span>Ocjena :  {0} od strane {1} korisnika</span>", rating, Iduser);
            sb.Append("</span>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}