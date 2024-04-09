using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace soft.Models
{
    public class PersonnelViewModel
    {
        [Key]
        public int Id { get; set; }

        public string? Noms { get; set; }
        public string? Matricule { get; set; } 
        public string? Grade { get; set; }
        public string? Sexe { get; set; }
        public string? LieuAffectation { get; set; }

        //EXEMPLE @Transient
        [NotMapped]
        public string? initial
        {
            get
            {
                return  "-" + this.Noms;
            }
        }
    }
}
