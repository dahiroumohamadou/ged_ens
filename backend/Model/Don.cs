using backend.Models;

namespace backend.Model
{
    public class Don
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int Montant { get; set; }
        public DateTime Date { get; set; }=DateTime.Now;
        public string? Description { get; set; }
        public int MembreId { get; set; }
        public Membre? membre { get; set; }
    }
}
