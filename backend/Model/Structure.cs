using backend.Models;

namespace backend.Model
{
    public class Structure
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Libele { get; set; }
        public ICollection<Membre>? Membres { get; set; }
    }
}
