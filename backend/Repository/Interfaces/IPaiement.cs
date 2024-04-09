using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface IPaiement:IDisposable
    {
        ICollection<Paiement> GetAll();
        ICollection<Paiement> GetByIdMembre(int id);
        Paiement GetById(int id);
        int Add(Paiement entity);
        int Update(Paiement entity);
        int Delete(int id);
    }
}
