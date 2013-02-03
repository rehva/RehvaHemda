using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wikiped.DBBL.BLL;
using Wikiped.DBBL.DAL;

namespace Wikiped.Models
{
    public class ClanciServis
    {



        public Guid guid { get; set; }
        public string slika { get; set; }
        public int popularnost { get; set; }
        public int ocjena { get; set; }
        public int vrsta { get; set; }
        public string naslov { get; set; }
        public string opis { get; set; }
        public string tekst { get; set; }
        public string klasa { get; set; }



    }
    public class ClanciServisObrada
    {
        public string klasa { get; set; }
        public List<ClanciServis> ClanciOrig { get; set; }

        public List<ClanciServisObrada> getAllClanci()
        {
            using (Spajanje s = new Spajanje())
            {

                List<ClanciServis> tst = (from c in s.Context.Clanci
                                          join sa in s.Context.Sadrzaji
                                          on c.ClanakID equals sa.ClanakID
                                          join ts in s.Context.TagVrste
                                          on c.TagVrstaID equals ts.TagVrstaID
                                          select new ClanciServis
                                          {
                                              guid = (Guid)c.Guid,
                                              slika = c.Slika,
                                              popularnost = (int)c.Popularnost,
                                              ocjena = (int)c.Ocjenjeno,
                                              naslov = sa.Naslov,
                                              opis = sa.Opis,
                                              tekst = sa.Tekst,
                                              klasa = ts.Opis,
                                              vrsta = (int)ts.Vrsta



                                          }).ToList();


                List<ClanciServisObrada> finals = new List<ClanciServisObrada>();
                ClanciServisObrada tempF;
                List<TagVrste> SveVrste = GetAllVrste();
                foreach (TagVrste it in SveVrste)
                {


                    try
                    {
                        tempF = null;
                        tempF = new ClanciServisObrada();
                        tempF.ClanciOrig = (from lis in tst where lis.vrsta == it.Vrsta select lis).ToList();
                        tempF.klasa = (from lis in tempF.ClanciOrig select lis.klasa).FirstOrDefault().ToString();
                        finals.Add(tempF);


                    }
                    catch (Exception)
                    {


                    }

                }



                return finals;

            }
        }
        public List<TagVrste> GetAllVrste()
        {
            using (Spajanje s = new Spajanje())
            {
                return s.Context.TagVrste.ToList();

            }

        }
        public string getString(Guid g)
        {
            return g.ToString();

        }
        public ClanciServis getClanakById(Guid id)
        {

            using (Spajanje s = new Spajanje())
            {

                ClanciServis tst = (from c in s.Context.Clanci
                                    join sa in s.Context.Sadrzaji

                                    on c.ClanakID equals sa.ClanakID
                                    where c.Guid == id
                                    select new ClanciServis
                                    {
                                        guid = (Guid)c.Guid,
                                        slika = c.Slika,
                                        popularnost = (int)c.Popularnost,
                                        ocjena = (int)c.Ocjenjeno,
                                        naslov = sa.Naslov,
                                        opis = sa.Opis,
                                        tekst = sa.Tekst
                                    }).FirstOrDefault();

                return tst;

            }
        }
    }
}