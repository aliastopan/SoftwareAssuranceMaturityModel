using SoftwareAssuranceMaturityModel.Domain.Enums;

namespace SoftwareAssuranceMaturityModel.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public SessionFlag Flag { get; set; }

        public Session()
        {
            StartDate = new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day
            );

            EndDate = new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day
            );

            Flag = SessionFlag.OnGoing;
        }

    }
}