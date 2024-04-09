using backend.Data;
using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace backend.Repository.Implementations
{
    public class PersonnelRepo : IPersonnel
    {
        private readonly AppDbContext _context;
        public PersonnelRepo(AppDbContext ctx) {
            _context = ctx;
        }
        public int Add(Personnel personnel)
        {
            int result = -1;
           if (personnel == null)
            {
                result=0;
            }
            else  
            {
                _context.Personnels.Add(personnel);
                _context.SaveChanges();
                result=personnel.Id;
            }
           return result;
        }

        public int Delete(int id)
        {
            int result = -1;
            var P = _context.Personnels.Where(x => x.Id == id).FirstOrDefault() ?? null;
            if (P != null)
            {
                _context.Personnels.Remove(P);
                _context.SaveChanges();
                result=P.Id;
            }
           
            return result;
        }

        public IEnumerable<Personnel> GetAll()
        {
            var res = _context.Personnels.ToList();
            return res;
        }

        public Personnel GetById(int id)
        {
            var res=_context.Personnels.Where(x => x.Id == id).FirstOrDefault() ?? null;
            return res;
        }

        public int Update(Personnel personnel)
        {
            var p=_context.Personnels.Where(x => x.Id==personnel.Id).FirstOrDefault() ?? null;
            if (p!=null)
            {
                p.Noms = personnel.Noms;
                p.Grade=personnel.Grade;
                p.LieuAffectation=personnel.LieuAffectation;
                p.Sexe=personnel.Sexe;
                p.Matricule=personnel.Matricule;
                _context.SaveChanges();
                return p.Id;
            }
            else
            {
                return -1;
            }
        }
        public Personnel GetByMatricule(string matricule)
        {
            var p=_context.Personnels.Where(x=>x.Matricule==matricule).FirstOrDefault() ?? null;
            if (p!=null)
            {
                return p;
            }
            else
            {
                return null;
            }

        }
        public void Dispose()
        {
            _context?.Dispose();

        }
    }
}
