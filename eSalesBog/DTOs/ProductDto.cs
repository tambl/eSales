using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSalesBog.DTOs
{
    public class ProductDto
    {
        public int ID { get; set; }
        
        public string ProductCode { get; set; }
   
        public string ProductName { get; set; }

        public decimal Price { get; set; }
    }
}