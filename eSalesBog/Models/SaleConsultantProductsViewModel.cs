using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSalesBog.Models
{
    public class SaleConsultantProductsViewModel
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int ConsultantID { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime ConsultantBirthDate { get; set; }
        public string FullName { get; set; }
        public string ProductCode { get; set; }
        public int? Quantity { get; set; }
        public decimal? SumAmount { get; set; }
        public int? QuantityOverHierarchy { get; set; }
        public string TopSoldProductCode { get; set; }
        public string TopSoldProductName { get; set; }
        public int TopSoldProductTotalQuantity { get; set; }
        public string TopProfitableProductCode { get; set; }
        public string TopProfitableProductName { get; set; }
        public decimal TopProfitableProductTotalAmount { get; set; }
    }
}