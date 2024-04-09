using backend.Data;
using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace backend.Repository.Implementations
{
    public class AssistanceRepo : IAssistance
    {
        private readonly AppDbContext _context;
        public AssistanceRepo(AppDbContext context)
        {
            _context = context;
        }
        public int Add(Assistance demande)
        {
            int res = -1;
            if (demande == null)
            {
                res = 0;

            }
            else
            {
                _context.Assistances.Add(demande);
                _context.SaveChanges();
                res=demande.Id;

            }
            return res;
        }

        public int Delete(int id)
        {
           int res = -1;
           var d=_context.Assistances.Where(d=>d.Id==id).FirstOrDefault() ?? null;
            if(d!=null)
            {
                _context.Assistances.Remove(d);
                _context.SaveChanges();
                res=d.Id;
            }

            return res;
        }


        public Assistance Get(int id)
        {
            var d=_context.Assistances.Where(d=>d.Id==id).FirstOrDefault() ?? null;
            return d;
        }

        public ICollection<Assistance> GetAll()
        {
            var ds= _context.Assistances
                .Include(a=>a.Membre)
                .ToList();
            return ds;
        }

        public int Update(Assistance demande)
        {
           int res= -1;
            var d = _context.Assistances.Where(d => d.Id == demande.Id).FirstOrDefault() ?? null;
            if(d!=null)
            {
                d.MembreId = demande.MembreId;
                d.Proposition=demande.Proposition;
                d.Objet = demande.Objet;
                d.Type= demande.Type;
                d.Statut = demande.Statut;
                d.Montant=demande.Montant;
                _context.Assistances.Update(d);
                _context.SaveChanges();
                res=d.Id;
            }
            return res;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public ICollection<Assistance> GetByIdMembre(int id)
        {
            var assistances = _context.Assistances
                                .Where(a => a.MembreId == id);
            return assistances.ToList();
                
           
        }
    }
}
