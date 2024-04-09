
using System.ComponentModel.DataAnnotations.Schema;

namespace soft.Models
{
    public class Assistance
    {
        public int Id { get; set; }
        public string? Objet { get; set; }
        public string? Type { get; set; }
        public string Proposition { get; set; }=string.Empty;
        public int Montant { get; set; }
        public DateTime Date { get; set; }=DateTime.Now;
        public int MembreId {  get; set; }
        public Membre? Membre { get; set; }
        public int Statut {  get; set; }

        [NotMapped]
        public string? MembreInfo
        {
            get
            {
                return Membre == null ? "" : this.Membre.Noms + " " + this.Membre.Prenoms;
            }
        }
    }
}
