using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static eSalesBog.Models.SalesViewModels;

namespace eSalesBog.Models
{
    public class SalesViewModel
    {
        [Display(Name = "გაყიდვის ნომერი")]
        public int ID { get; set; }

        [Display(Name = "გაყიდვის დრო")]
        public DateTime? SaleDate { get; set; }
        [Display(Name = "კონსულტანტი")]
        public int ConsultantID { get; set; }
        [Display(Name = "პროდუქტი")]
        public int? ProductID { get; set; }
        [Display(Name = "აღწერა")]
        public string SaleDescription { get; set; }
        [Display(Name = "რაოდენობა")]
        public int? ProductCount { get; set; }
        public ConsultantViewModel Consultant { get; set; }
        public List<ProductViewModel> Products { get; set; }
        [Display(Name = "პროდუქტები")]
        public string ProductsDescription { get; set; }
        [Display(Name = "კონსულტანტი")]
        public string ConsultantDescription { get; set; }


    }
}