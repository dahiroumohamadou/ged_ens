using System.ComponentModel.DataAnnotations.Schema;

namespace soft.Models
{
    public class Paiement
    {
        public int Id { get; set; }
        public int Montant { get; set; }
        public DateTime datePaiement { get; set; } = DateTime.Now;
        public int MembreId { get; set; }
        public Membre? Membre { get; set; }
        public int PeriodiciteId { get; set; }
        public Periodicite? Periodicite { get; set; }
        [NotMapped]
        public string? MembreInfo
        {
            get
            {
                return Membre == null ? "" : this.Membre.Noms + " " + this.Membre.Prenoms;
            }
        }
        //[NotMapped]
        //public string? MembreGetMontantCotisation
        //{
        //    get
        //    {
        //        return Membre == null ? "" : this.Membre.Noms + " " + this.Membre.Prenoms + "-" + this.Membre.Categorie.MontantCotisation.ToString();
        //    }
        //}
        [NotMapped]
        public string PeriodeInfo
        {
            get
            {
                return Periodicite == null ? "" : this.Periodicite.mois + "-" + this.Periodicite.annee;
            }
        }
    }
}
