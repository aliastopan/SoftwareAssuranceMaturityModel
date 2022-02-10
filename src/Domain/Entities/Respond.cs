
namespace SoftwareAssuranceMaturityModel.Domain.Entities
{
    public class Respond
    {
        public int Id { get; set; }
        public int QNumber { get; set; } = -1;
        public int QDomain { get; set; } = 0;
        public int Value { get; set; } = 0;
    }
}