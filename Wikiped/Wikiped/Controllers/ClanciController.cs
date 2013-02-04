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
            ClanciServisObrada c = new ClanciServisObrada();
            return View(c.getAllClanci());
        }
        public ActionResult Edit(Guid id)
        {
            ClanciServisObrada c = new ClanciServisObrada();
            return View(c.getClanakById(id));
        }
        public bool MogucnostGlasanja(int clanakId)
        {
            if (Session["Korisnik"] == null)
            {
                return false;
            }
            else
            {
                Korisnici kor= Session["Korisnik"] as Korisnici;
                using (Spajanje sp = new Spajanje())
                {
                    OcjenaKorisnici glasanje = (from co in sp.Context.OcjenaKorisnici
                                                where co.ClanakID == clanakId && co.KorisnikID == kor.KorisnikID
                                    select co).FirstOrDefault();

                    if (glasanje != null)
                    {
                        return false;
                    }
                                                                  


                }

            }
            return true;
        }
        public JsonResult SetOcjena(int clanakId, double rating)
        {
            try
            {
                if (MogucnostGlasanja(clanakId) == false)
                {

                    return Json(new JsonServis
                    {
                        Success = false,
                        Message = "Sorry, you already voted for this post"
                    });
                }
                Korisnici kor = Session["Korisnik"] as Korisnici;
                using (Spajanje s = new Spajanje())
                {
                    OcjenaKorisnici oc = new OcjenaKorisnici();
                    oc.KorisnikID = kor.KorisnikID;
                    oc.ClanakID = clanakId;
                    oc.Ocjena = (rating/2);
                    s.Context.OcjenaKorisnici.AddObject(oc);
                 

                    Clanci clanakTemp = (from c in s.Context.Clanci where c.ClanakID == clanakId select c).FirstOrDefault();
                    if (clanakTemp.Ocjenjeno > 0)
                    {
                        int brojOcjena = (int)clanakTemp.Ocjenjeno;
                        double ProsOcjena = (double)clanakTemp.Popularnost;
                        double novaProsjecna = ((ProsOcjena * (double)brojOcjena) + (double)oc.Ocjena) / (brojOcjena + 1);

                        clanakTemp.Popularnost = novaProsjecna;
                        clanakTemp.Ocjenjeno++;
                        
                    }
                    else
                    {
                        clanakTemp.Popularnost = (double)oc.Ocjena;
                        clanakTemp.Ocjenjeno++;
                    }
                    s.Context.SaveChanges();

                return Json(new JsonServis
                {
                    Success = true,
                    Message = "Your Vote was cast successfully",
                    Result = new { Rating = (clanakTemp.Popularnost*2), Raters = clanakTemp.Ocjenjeno }
                });
                }
                //PostModel post = Engine.Posts.SetRating(id, rating);
                
            }
            catch (Exception ex)
            {
                return Json(new JsonServis
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }


    }
}
