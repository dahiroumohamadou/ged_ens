using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace soft.Models
{
    public class Membre
    {
        public int Id { get; set; }
        public string? Noms { get; set; }
        public string? Prenoms { get; set; }
        public string? Matricule { get; set; }
        public string? Fonction { get; set; }
        public string? StrcutureAffectation { get; set; }
        public int? MontantAdhesion { get ; set; } 
        public string? PhoneNumber { get; set; }
        public DateTime DateAdhesion { get; set; } = DateTime.Now;
        public int CategorieId { get; set; }
        public Categorie? Categorie { get; set; }
        public int StructureId { get; set; }
        public Structure? Structure { get; set; }
        public ICollection<Paiement>? Paiements { get; set; }
        public ICollection<Assistance>? Demandes { get; set; }
        [NotMapped]
        public string MembreInfo
        {
            get
            {
                return this.Noms + " " + this.Prenoms;
            }
        }
       
        [NotMapped]
        public string? MembreGetMontantCotisation
        {
            get
            {
                return Categorie == null ? "" : this.Noms + " " + this.Prenoms + "-" + this.Categorie.MontantCotisation.ToString();
            }
        }
        [NotMapped]
        public string? MembreStrAffecation
        {
            get
            {
                return Structure==null ? " " : this.Structure.Code;
            }
        }
        //[NotMapped]
        //public string? MembreCategorie
        //{
        //    get
        //    {
        //        return Id == 0 ? "" : this.Categorie.libele;
        //    }
        //}
        //[NotMapped]
        //public string MembreMontantAdh
        //{
        //    get
        //    {
        //        return this.Id == 0 ? "0" : this.Categorie.MontantAdhesion.ToString();
        //    }
        //}
    }
}
