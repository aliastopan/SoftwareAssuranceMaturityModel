#nullable disable

namespace SoftwareAssuranceMaturityModel.Domain.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public Session Session { get; set; }
        public List<Respond> Responds { get; set; }
    }
}