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

    }
}
