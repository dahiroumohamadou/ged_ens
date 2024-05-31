using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface ICycle
    {
        ICollection<Cycle> GetAll();
        Cycle GetById(int id);
        int Add(Cycle cycle);  
        int Update (Cycle cycle);
        int Delete(int  id);
        int Existe(Cycle cycle);
    }
}
