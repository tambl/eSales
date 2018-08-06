using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eSalesBog.Models
{
    public class SalesViewModels
    {
        public class ConsultantViewModel
        {
            public int ID { get; set; }
            [Display(Name = "სახელი")]
            public string FirstName { get; set; }
            [Display(Name = "გვარი")]
            public string LastName { get; set; }
            [Display(Name = "პირადობის ნომერი")]
            [MaxLength(11,ErrorMessage ="მაქს 11 ნიშნა")]
            public string PersonalNumber { get; set; }
            [Display(Name = "სქესი")]
            public int? Gender { get; set; }
            [Display(Name = "დაბადების თარიღი")]
            public DateTime? BirthDate { get; set; }
            [Display(Name = "რეკომენდატორი")]
            public int? RecommenderConsultantID { get; set; }
            public List<ConsultantViewModel> RecommenderConsultant { get; set; }
        }

        public class ProductViewModel
        {
            public int ID { get; set; }

            [Display(Name = "კოდი")]
            public string ProductCode { get; set; }
            [Display(Name = "სახელი")]
            public string ProductName { get; set; }
            [Display(Name = "ფასი")]
            public decimal Price { get; set; }
        }

        public class SalesViewModel
        {
            public int ID { get; set; }

            [Display(Name = "გაყიდვის დრო")]
            public DateTime? SaleDate { get; set; }
            [Display(Name = "კონსულტანტი")]
            public int ConsultantID { get; set; }
            [Display(Name = "პროდუქტი")]
            public int? ProductID { get; set; }
            [Display(Name = "აღწერა")]
            public string SaleDescription { get; set; }

            public List<ConsultantViewModel> Consultants { get; set; }
            public List<ProductViewModel> Products { get; set; }
        }
    }
}