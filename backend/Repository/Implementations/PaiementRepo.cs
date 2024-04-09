using backend.Data;
using backend.Repository.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Implementations
{
    public class PaiementRepo:IPaiement
    {
        private readonly AppDbContext _context;

        public PaiementRepo(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Paiement paiement)
        {
            int res = -1;
            if (paiement == null)
            {
                res = 0;
            }
            else
            {
                _context.Paiements.Add(paiement);
                _context.SaveChanges();
                res=paiement.Id;
            }
            return res;
        }

        public int Delete(int id)
        {
            int res= -1;
            var p=_context.Paiements.Where(p=>p.Id==id).FirstOrDefault() ?? null;
            if (p!= null){
                _context.Paiements.Remove(p);
                _context.SaveChanges();
                res=p.Id;
            }
            return res;
        }

        public void Dispose()
        {
           _context?.Dispose();
        }

        public ICollection<Paiement> GetAll()
        {
            var paiements = _context.Paiements
                                .Include(p=>p.Membre)
                                .Include(pr=>pr.Periodicite);
            //var paiements= _context.Paiements.ToList();
            return paiements.ToList();
        }

        public Paiement GetById(int id)
        {
            var p= _context.Paiements
                .Include(p => p.Membre)
                .ThenInclude(c=>c.Categorie)
                .Where(p=>p.Id==id).FirstOrDefault() ?? null;
            return p;
        }

        public ICollection<Paiement> GetByIdMembre(int id)
        {
            var paiements = _context.Paiements
                                .Include(p => p.Membre)
                                .ThenInclude(c=>c.Categorie)
                                .Include(pr => pr.Periodicite)
                                .Where(p=>p.MembreId==id);
            //var paiements= _context.Paiements.ToList();
            return paiements.ToList();
        }

        public int Update(Paiement paiement)
        {
            int res = -1;
            var p=_context.Paiements.Where(p=>p.Id==paiement.Id).FirstOrDefault() ?? null;  
            if (p!= null)
            {
                p.Montant=paiement.Montant;
                p.datePaiement=paiement.datePaiement;
                p.PeriodiciteId=paiement.PeriodiciteId;
                p.MembreId=paiement.MembreId;
                _context.Paiements.Update(p);
                _context.SaveChanges();
                res=p.Id;
            }

            return res;
        }
    }
}
