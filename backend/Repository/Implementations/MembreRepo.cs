using backend.Data;
using backend.Repository.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Implementations
{
    public class MembreRepo : IMembre
    {
        private readonly AppDbContext _context;

        public MembreRepo(AppDbContext ctx)
        {
            _context = ctx;
        }


        public int Add(Membre membre)
        {
            int res = -1;
            if(membre==null)
            {
                res= 0;
            }
            else
            {
                _context.Membres.Add(membre);
                _context.SaveChanges();
                res = membre.Id;
                var m2 = _context.Membres
                    .Include(m => m.Categorie)
                    .Where(m => m.Id == res).FirstOrDefault() ?? null;
                if (m2 != null)
                {
                    m2.MontantAdhesion = m2.Categorie.MontantAdhesion;
                    _context.Membres.Update(m2);
                    _context.SaveChanges();
                    res = m2.Id;
                }

            }
            return res;
        }

        public int Delete(int id)
        {
            int res = -1;
            var m = _context.Membres.Where(m => m.Id == id).FirstOrDefault() ?? null;

            if(m != null)
            {
                _context.Membres.Remove(m);
                _context.SaveChanges();
                res= m.Id;
            }
            return res;
        }



        public Membre Get(int id)
        {

            var m=_context.Membres
                .Include(m => m.Structure)
                .Include(m => m.Categorie)
                .Include(m => m.Assistances)
                .Include(m => m.Paiements)
                  .ThenInclude(c => c.Periodicite)
                .Where(m=>m.Id == id).FirstOrDefault() ?? null;
            return m;
            
        }

        public async Task<IEnumerable<Membre>> GetAll()
        {
            return await _context.Membres
                .Include(m=>m.Structure)
                .Include(m=>m.Categorie)
                .OrderBy(m=>m.Structure.Code)
                .ToListAsync();
          
        }

        public int Update(Membre membre)
        {
            var res=-1;
            var m= _context.Membres.Where(m=>m.Id==membre.Id).FirstOrDefault() ?? null;
            if(m!= null)
            {
                m.Matricule=membre.Matricule;
                m.Noms=membre.Noms;
                m.Prenoms=membre.Prenoms;
                m.Fonction=membre.Fonction;
                m.StrcutureAffectation=membre.StrcutureAffectation;
                m.StructureId=membre.StructureId;
                m.DateAdhesion=membre.DateAdhesion;
                m.CategorieId=membre.CategorieId;
                _context.Membres.Update(m);
                _context.SaveChanges();
                res= m.Id;
                //var m2 = _context.Membres
                //    .Include (m=>m.Categorie)
                //    .Where(m => m.Id == res).FirstOrDefault() ?? null;
                //if(m2!= null)
                //{
                //    m.Fonction=membre.;

                //}
            }
            return res;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
