namespace backend.Models
{
    public class Periodicite
    {
        public int Id { get; set; }
        public string? mois { get; set; } 
        public string? annee { get; set;}
        public ICollection<Paiement>? Paiements { get; set; }
    }
}
