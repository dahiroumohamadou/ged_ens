using soft.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ged.Models
{
    public class Doc
    {
        public int Id { get; set; }
        public string? TypeDoc { get; set; }
        public string? Objet { get; set; }
        public string? Source { get; set; } = "ENS-YDE";
        public string? Numero { get; set; }
        public string? DateSign { get; set; }
        public string? AnneeAcademique { get; set; }
        public int? CycleId { get; set; }
        public Cycle? Cycle { get; set; }
        public int? Fichier { get; set; }
        public string? Session { get; set; }
        public string? Promotion { get; set; }
        public string? AnneeSortie { get; set; }
        public int? FiliereId { get; set; }
        public Filiere? Filiere { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = DateTime.Now;
        [NotMapped]
        public string? FiliereInfo
        {
            get
            {
                return Filiere == null ? "" : this.Filiere.Code;
            }
        }
        [NotMapped]
        public string? CycleInfo
        {
            get
            {
                return Cycle == null ? "" : this.Cycle.Code;
            }
        }
    }
}
