using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareAssuranceMaturityModel.Domain.Entities
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public  string Domain { get; set; } = default!;
        public string Statement { get; set; } = default!;
        public List<string> Responds { get; set; } = new();
    }
}