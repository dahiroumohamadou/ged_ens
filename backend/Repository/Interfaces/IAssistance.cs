using backend.Model;

namespace backend.Repository.Interfaces
{
    public interface IAssistance:IDisposable
    {
        ICollection<Assistance> GetAll();
        ICollection<Assistance> GetByIdMembre(int id);
        Assistance Get(int id);
        int Add(Assistance demande);
        int Update(Assistance demande);
        int Delete(int id);

    }
}
