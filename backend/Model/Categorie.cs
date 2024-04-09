namespace backend.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string? libele { get; set; }
        public int? MontantAdhesion { get; set; }
        public int? MontantCotisation { get; set; }
        public ICollection<Membre>? Membres { get; set; }
    }
}
