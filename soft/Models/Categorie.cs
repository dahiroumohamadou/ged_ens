using System.ComponentModel.DataAnnotations.Schema;

namespace soft.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string? libele { get; set; }
        public int? MontantAdhesion { get; set; }
        public int? MontantCotisation { get; set; }
        public ICollection<Membre>? Membres { get; set; }
        [NotMapped] 
        public string detailCategorie {
            get {
                return this.libele +" [A: ("+this.MontantAdhesion+") & C : ("+this.MontantCotisation+")]";
                }
        }
    }
}
