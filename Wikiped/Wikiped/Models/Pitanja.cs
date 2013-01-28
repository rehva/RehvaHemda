using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wikiped.DBBL.DAL;

namespace Wikiped.Models
{
    public class Pitanja:IDisposable
    {
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

        public void Dispose()
        {
            context.Dispose();
        }
    }
}