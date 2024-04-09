using MessagePack;

namespace e_soft.Models
{
    public class Membre
    {
        public int Id { get; set; }
        public string? Noms { get; set; }
        public string? Prenoms { get; set; }
        public string? Matricule { get; set;}
        public string? Fonction { get; set;}
        public string? LieuAffectation { get; set; }
        public int MontantAdhesison { get; set; }
        private DateTime DateAdhesion { get; set;}=DateTime.Now;
        public int CategorieId { get; set; }
        public Categorie? Categorie { get; set; }
        public ICollection<Paiement>? Paiements { get; set; }
    }
}
