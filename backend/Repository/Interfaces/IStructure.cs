using backend.Model;

namespace backend.Repository.Interfaces
{
    public interface IStructure
    {
        ICollection<Structure> GetAll();
        Structure GetById(int id);
        int Add(Structure s);
        int Update(Structure s);
        int Delete(int id);
    }
}
