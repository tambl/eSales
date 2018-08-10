using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
   public class ProductSalesDto
    {
        public int ID { get; set; }
        public int? SaleID { get; set; }
        public int? ProductID { get; set; }
        public int? ProductCount { get; set; }

        public virtual ProductDto Products { get; set; }
        public virtual SalesDto Sales { get; set; }
    }
}
