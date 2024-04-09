namespace e_soft.Models
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
    }
}
