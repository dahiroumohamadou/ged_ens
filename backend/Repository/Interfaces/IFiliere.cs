using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface IFiliere
    {
        ICollection<Filiere> GetAll();
        Filiere GetById(int id);
        int Add(Filiere filiere);
        int Update(Filiere filiere);
        int Delete(int id);
    }
}
