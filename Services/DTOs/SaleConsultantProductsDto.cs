using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
   public class SaleConsultantProductsDto
    {

        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int ConsultantID { get; set; }
        public string PersonalNumber { get; set; }
        public string FullName { get; set; }
        public DateTime ConsultantBirthDate { get; set; }
        public string ProductCode { get; set; }
        public int? Quantity { get; set; }
        public decimal? SumAmount { get; set; }
        public int? QuantityOverHierarchy { get; set; }


    }
}
