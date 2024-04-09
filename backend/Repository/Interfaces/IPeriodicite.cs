using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface IPeriodicite:IDisposable
    {
        ICollection<Periodicite> GetAll();
        Periodicite GetById(int id);
        int Add(Periodicite periodicite);
        int Update(Periodicite periodicite);
        int Delete(int id);
    }
}
