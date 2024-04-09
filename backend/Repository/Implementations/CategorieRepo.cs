using backend.Data;
using backend.Repository.Interfaces;
using backend.Models;

namespace backend.Repository.Implementations
{
    public class CategorieRepo:ICategorie
    {
        private readonly AppDbContext _context;

        public CategorieRepo(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Categorie categorie)
        {
            int res = -1;
            if(categorie == null)
            {
                res = 0;
            }
            else
            {
                _context.Categories.Add(categorie);
                _context.SaveChanges();
                res = categorie.Id;
            }

            return res;
        }

        public int Delete(int id)
        {
            int res = -1;
            var c= _context.Categories.Where(c => c.Id == id).FirstOrDefault() ?? null;
            if(c != null)
            {
                _context.Categories.Remove(c);
                _context.SaveChanges();
                res = c.Id;
            }
            return res;
        }


        public Categorie Get(int id)
        {
            var c = _context.Categories.Where(c => c.Id == id).FirstOrDefault() ?? null;
            return c;
        }

        public ICollection<Categorie> GetAll()
        {
            var categories=_context.Categories.ToList();
            return categories;
        }

        public int Update(Categorie categorie)
        {
           int res = -1;
            var c = _context.Categories.Where(c => c.Id == categorie.Id).FirstOrDefault() ?? null;
            if(c!=null)
            {
                c.libele=categorie.libele;
                c.MontantCotisation = categorie.MontantCotisation;
                c.MontantAdhesion = categorie.MontantAdhesion;
                _context.Categories.Update(c);
                _context.SaveChanges();
                res = c.Id;
            }
            return res;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
