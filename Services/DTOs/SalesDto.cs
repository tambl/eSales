using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.DTOs
{
    public class SalesDto
    {
        public int ID { get; set; }

        public DateTime? SaleDate { get; set; }
        public int ConsultantID { get; set; }
        public int? ProductID { get; set; }
        public string SaleDescription { get; set; }
        public List<ConsultantDto> Consultants { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}