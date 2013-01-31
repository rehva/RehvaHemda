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
        public string naslov { get; set; }
        public string opis { get; set; }
        public string tekst { get; set; }
        

        public List<ClanciServis> getAllClanci()
        {
            using (Spajanje s = new Spajanje())
            {

                List<ClanciServis> tst = (from c in s.Context.Clanci
                            join sa in s.Context.Sadrzaji
                            on c.ClanakID equals sa.ClanakID
                            select new ClanciServis
                            {
                                guid = (Guid)c.Guid,
                                slika = c.Slika,
                                popularnost = (int)c.Popularnost,
                                ocjena = (int)c.Ocjenjeno,
                                naslov = sa.Naslov,
                                opis = sa.Opis,
                                tekst = sa.Tekst
                           

                            }).ToList();

                return tst;

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