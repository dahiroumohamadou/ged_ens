using backend.Data;
using backend.Repository.Interfaces;
using backend.Models;

namespace backend.Repository.Implementations
{
    public class PeriodiciteRepo:IPeriodicite
    {
        private readonly AppDbContext _context;
     

        public PeriodiciteRepo(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Periodicite periodicite)
        {
            int res = -1;
            if (periodicite == null)
            {
                res= 0;
            }
            else
            {
                _context.Periodicites.Add(periodicite);
                _context.SaveChanges();
                res = periodicite.Id;
            }
            return res;
           
        }

        public int Delete(int id)
        {
            int res= -1;
           var p=_context.Periodicites.Where(x=>x.Id==id).FirstOrDefault() ?? null;
            if (p != null)
            {
                _context.Periodicites.Remove(p);
                _context.SaveChanges();
                res= p.Id;
            }
            return res;
        }

        public ICollection<Periodicite> GetAll()
        {
            var pds=_context.Periodicites.ToList();
            return pds;
            
        }

        public Periodicite GetById(int id)
        {
            var p= _context.Periodicites.Where(p=>p.Id==id).FirstOrDefault() ?? null;
            return p;
        }

        public int Update(Periodicite periodicite)
        {
            int res = -1;
            var p=_context.Periodicites.Where(p=>p.Id==periodicite.Id).FirstOrDefault() ?? null;
            if(p != null)
            {
                p.mois = periodicite.mois;
                p.annee= periodicite.annee;
                _context.Periodicites.Update(p);
                _context.SaveChanges();
                res= p.Id;
            }
            return res;
        }

       

        public void Dispose()
        {
           _context?.Dispose();
        }
    }
}
