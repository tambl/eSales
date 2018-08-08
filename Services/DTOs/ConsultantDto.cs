using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.DTOs
{
    public class ConsultantDto
    {
        public int ID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PersonalNumber { get; set; }
        
        public int? Gender { get; set; }
        
        public DateTime? BirthDate { get; set; }
        public int? RecommenderConsultantID { get; set; }
        public List<ConsultantDto> RecommenderConsultant { get; set; }
    }
}