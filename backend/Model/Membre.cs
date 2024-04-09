using backend.Model;
using MessagePack;

namespace backend.Models
{
    public class Membre
    {
        public int Id { get; set; }
        public string? Noms { get; set; }
        public string? Prenoms { get; set; }
        public string? Matricule { get; set;}
        public string? Fonction { get; set;}
        public string? StrcutureAffectation {  get; set; }
        public int? MontantAdhesion { get; set; }
        public string? PhoneNumber {  get; set; }
        public DateTime DateAdhesion { get; set;}=DateTime.Now;
        public int CategorieId { get; set; }
        public Categorie? Categorie { get; set; }
        public int StructureId { get; set; }
        public Structure? Structure { get; set; }
        public ICollection<Paiement>? Paiements { get; set; }
        public ICollection<Assistance>? Assistances { get; set; }
        public ICollection<Don>? Dons { get; set; }
    }
}
