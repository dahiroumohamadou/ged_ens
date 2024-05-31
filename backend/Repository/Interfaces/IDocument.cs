﻿using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface IDocument
    {
        ICollection<Doc> GetAll();
        ICollection<Doc> GetAllByType(string type);
        ICollection<Doc> GetAllByAnnee(string annee);
        Doc GetById(int id);
        int Add(Doc document);
        int Update(Doc document);
        int Delete(int id);
        Doc ExistePv(string source, string session, string promotion, string anneeSortie, int cycleId, int filiereId);
        Doc ExisteAr(string source, string numero, string dateSign, string anneeAca, int cycleId);
        Doc ExisteCrp(string source, string numero, string dateSign, string session,  string anneeAca, int cycleId);
        Doc ExisteOthers(string source, string numero, string dateSign);
    }
}
