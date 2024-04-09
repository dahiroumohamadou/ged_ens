using System.ComponentModel.DataAnnotations.Schema;

namespace soft.Models
{
    public class Periodicite
    {
        public int Id { get; set; }
        public string? mois { get; set; } 
        public string? annee { get; set;}
        public ICollection<Paiement>? Paiements { get; set; }
        [NotMapped]
        public string PeriodeInfo
        {
            get
            {
                return this.mois + " " + this.annee;
            }
        }
    }
}
