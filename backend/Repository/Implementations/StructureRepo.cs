using backend.Data;
using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Implementations
{
    public class StructureRepo : IStructure
    {
        private readonly AppDbContext _context;
        public StructureRepo(AppDbContext ctx)
        {
            _context = ctx;
        }
        public int Add(Structure s)
        {
            var res = -1;
            if (s == null)
            {
                res = 0;
            }
            else
            {
                _context.Structures.Add(s);
                _context.SaveChanges();
                res = s.Id;
            }
            return res;
            
        }

        public int Delete(int id)
        {
            var res = 0;
            var s=_context.Structures.Where(s=>s.Id==id).FirstOrDefault() ?? null;
            if (s!=null)
            {
                _context.Structures.Remove(s);
                _context.SaveChanges();
                res = s.Id;
            }
            return res;
        }

        public ICollection<Structure> GetAll()
        {
            var structures=_context.Structures
                .Include(s=>s.Membres)
                .ToList();
            return structures;
        }

        public Structure GetById(int id)
        {
            var s= _context.Structures
                .Include(s=>s.Membres)
                .Where(s=> s.Id==id).FirstOrDefault() ?? null;
            return s;
        }

        public int Update(Structure st)
        {
            int res = -1;
            var s = _context.Structures.Where(s=>s.Id==st.Id).FirstOrDefault() ?? null;
            if(s!=null)
            {
                s.Libele = st.Libele;
                s.Code = st.Code;
                _context.Structures.Update(s);
                _context.SaveChanges();
                res= s.Id;
            }
            return res;
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
