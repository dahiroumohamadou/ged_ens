using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface ICategorie:IDisposable
    {
        ICollection<Categorie> GetAll();
        Categorie Get(int id);
        int Add(Categorie categorie);
        int Update(Categorie categorie);
        int Delete(int id);
    }
}
