using backend.Model;

namespace backend.Repository.Interfaces
{
    public interface IDon:IDisposable
    {
        ICollection<Don> GetAll();
        Don GetById(int id);
        int Add(Don don);
        int Update(Don don);
        int Delete(int id);
    }
}
