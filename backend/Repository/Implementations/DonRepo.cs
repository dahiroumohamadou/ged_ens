using backend.Data;
using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Implementations
{
    public class DonRepo : IDon
    {
        private readonly AppDbContext _context;

        public DonRepo(AppDbContext ctx)
        {
            _context = ctx;
        }
        public int Add(Don don)
        {
            int res = -1;
            if (don == null)
            {
                res = 0;
            }
            else
            {
                _context.Dons.Add(don);
                _context.SaveChanges();
                res = don.Id;
            }
            return res;
        }

        public int Delete(int id)
        {
            int res = -1;
           var d=_context.Dons.Where(don => don.Id == id).FirstOrDefault() ?? null;
            if (d != null)
            {
                _context.Dons.Remove(d);
                _context.SaveChanges();
                res = d.Id;
            }
            return res;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public ICollection<Don> GetAll()
        {
            var dons=_context.Dons
                .Include(m=>m.membre)
                .ToList();
            return dons;
        }
        public Don GetById(int id)
        {
            var d = _context.Dons.Where(don => don.Id == id).FirstOrDefault() ?? null;
            return d;
        }

        public int Update(Don dn)
        {
           int res= -1;
            var d=_context.Dons.Where(don=>don.Id==dn.Id).FirstOrDefault() ?? null;
            if(d != null)
            {
                d.Montant=dn.Montant;
                d.Type=dn.Type;
                d.Description=dn.Description;
                d.MembreId = dn.MembreId;
                _context.Dons.Update(d);
                _context.SaveChanges();
                res = d.Id;
            }
            return res;
        }
    }
}
