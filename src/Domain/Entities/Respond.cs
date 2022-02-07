#nullable disable

namespace SoftwareAssuranceMaturityModel.Domain.Entities
{
    public class Respond
    {
        public int Id { get; set; }
        public Session Session { get; set; }
        public List<int> Value { get; set; }
    }
}