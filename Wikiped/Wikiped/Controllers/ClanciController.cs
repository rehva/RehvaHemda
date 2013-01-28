using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wikiped.Models;
using Wikiped.DBBL.DAL;
using Wikiped.DBBL.BLL;

namespace Wikiped.Controllers
{
    public class ClanciController : Controller
    {
        //
        // GET: /Clanci/

        public ActionResult Index()
        {
            ClanciServis c = new ClanciServis();
            return View(c.getAllClanci());
        }
        public ActionResult Edit(Guid id)
        {
            ClanciServis c = new ClanciServis();
            return View(c.getClanakById(id));
        }


    }
}
