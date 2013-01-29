using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wikiped.Models;
namespace Wikiped.Controllers
{
    public class PitanjaController : Controller
    {
        //
        // GET: /Pitanja/

        public ActionResult Index()
        {
            List<DBBL.DAL.Pitanja> pitanja;
            using (Pitanja pt= new Pitanja())
            {
                pitanja= pt.GetAllPitanja();
            }
            return View(pitanja);
        }
        public ActionResult Details(int id)
        {
            Pitanja pitanje=new Pitanja();
            using (Pitanja pt = new Pitanja())
            {
                pt.SetAllDetaByPitanjeID(id);
                pitanje = pt;
            }
            
            return View(pitanje);
        }
        public ActionResult VoteUp(int id, int voteNumber)
        {
            voteNumber++;
            
            return Json(new { voteNumber = voteNumber });
        }
        public ActionResult VoteDown(int id, int voteNumber)
        {
            voteNumber--;
            return Json(new { voteNumber = voteNumber });
        }

        //public static MvcHtmlString ToJson(this HtmlHelper html, object obj)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return MvcHtmlString.Create(serializer.Serialize(obj));
        //}

        //public static MvcHtmlString ToJson(this HtmlHelper html, object obj, int recursionDepth)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    serializer.RecursionLimit = recursionDepth;
        //    return MvcHtmlString.Create(serializer.Serialize(obj));
        //}
        // <script>
        // var s = @(Html.ToJson(Model.Content));
        //</script>
    }
}
