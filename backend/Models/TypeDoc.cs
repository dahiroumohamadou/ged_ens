namespace backend.Models
{
    public class TypeDoc
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Libele { get; set; }
        public ICollection<Doc>? Documents { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = DateTime.Now;
    }
}
