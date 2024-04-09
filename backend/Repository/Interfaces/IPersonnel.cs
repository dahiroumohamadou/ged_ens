using backend.Model;

namespace backend.Repository.Interfaces
{
    public interface IPersonnel:IDisposable
    {
        IEnumerable<Personnel> GetAll();
        Personnel GetById(int id);
        Personnel GetByMatricule(string? matricle);
        int Add(Personnel personnel);
        int Update(Personnel personnel);
        int Delete(int id);

    }
}
