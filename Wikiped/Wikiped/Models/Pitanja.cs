using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wikiped.DBBL.DAL;

namespace Wikiped.Models
{
    public class Pitanja:IDisposable
    {
        #region Property

        private Wikiped.DBBL.DAL.Pitanja pitanjaPost;
        private Wikiped.DBBL.DAL.Korisnici korisnik;
        private List<OdgovoriSaO> odgovori;
        private List<Wikiped.DBBL.DAL.Tagovi> tagovi;

        public Wikiped.DBBL.DAL.Korisnici Korisnik
        {
            get { return korisnik; }
            set { korisnik = value; }
        }
        public List<OdgovoriSaO> Odgovori
        {
            get { return odgovori; }
            set { odgovori = value; }
        }
        public Wikiped.DBBL.DAL.Pitanja PitanjaPost
        {
            get { return pitanjaPost; }
            set { pitanjaPost = value; }
        }
        public List<Wikiped.DBBL.DAL.Tagovi> Tagovi
        {
            get { return tagovi; }
            set { tagovi = value; }
        }

        #endregion
        WikipedEntities context;
        public Pitanja()
        {
            context = new WikipedEntities();
        }
        public List<DBBL.DAL.Pitanja> GetAllPitanja()
        {
            return context.Pitanja.ToList();
        }
        public DBBL.DAL.Pitanja GetPitanjeByID(int id)
        {
            return context.Pitanja.Where(x => x.PitanjeID == id).FirstOrDefault();
        }

        public void SetAllDetaByPitanjeID(int id)
        {
            pitanjaPost = context.Pitanja.Where(x => x.PitanjeID == id).FirstOrDefault();
            korisnik = context.Korisnici.Where(x => x.KorisnikID == pitanjaPost.KorisnikID).FirstOrDefault();
            List<DBBL.DAL.Odgovori> lstOdgovori = context.Odgovori.Where(x => x.PitanjeID == id).ToList();
            odgovori = new List<OdgovoriSaO>();

            foreach (var item in lstOdgovori)
            {
                OdgovoriSaO odgovoriAll = new OdgovoriSaO();
                odgovoriAll.OdgovoriGlavni = item;
                odgovoriAll.Korisnik = context.Korisnici.Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault();

                COdgovorNaOdgovor tempOdgovorO;
                List<DBBL.DAL.OdgovorNaOdgovor> lstOdgovoriOdg = context.OdgovorNaOdgovor.Where(x => x.OdgovorID == item.OdgovorID).ToList();
                foreach (var odg in lstOdgovoriOdg)
                {
                    tempOdgovorO = new COdgovorNaOdgovor();
                    tempOdgovorO.OdgovoriNaPodOgovor = odg;
                    tempOdgovorO.Korisnik = context.Korisnici.Where(x =>x.KorisnikID== odg.KorisnikID).FirstOrDefault();
                    odgovoriAll.OdgovoriNaOdgovor.Add(tempOdgovorO);
                }
                odgovori.Add(odgovoriAll);
            }
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}