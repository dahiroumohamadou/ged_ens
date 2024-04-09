using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface IMembre:IDisposable
    {
        Task<IEnumerable<Membre>> GetAll();
        Membre Get(int id);
        int Add(Membre membre);
        int Update(Membre membre);
        int Delete(int id);

    }
}
