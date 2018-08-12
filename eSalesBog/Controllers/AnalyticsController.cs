using eSalesBog.Models;
using Services.ServiceAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace eSalesBog.Controllers
{
    public class AnalyticsController : Controller
    {
        private ISalesService _serviceClient;

        public AnalyticsController(ISalesService serviceClient)
        {
            _serviceClient = serviceClient;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSalesByConsultants(DateTime startDate, DateTime endDate)
        {
            var a = Mapper.Map<List<SaleConsultantProductsViewModel>>(_serviceClient.GetSalesByConsultants(startDate, endDate));

            var json = Json(new { data = a },
              JsonRequestBehavior.AllowGet);
            return json;
        }

        public ActionResult GetSalesByProductPrice(DateTime startDate, DateTime endDate, decimal minPrice, decimal maxPrice)
        {
            var a = Mapper.Map<List<SaleConsultantProductsViewModel>>(_serviceClient.GetSalesByProductPrice(startDate, endDate, minPrice, maxPrice));

            var json = Json(new { data = a },
              JsonRequestBehavior.AllowGet);
            return json;
        }
        public ActionResult GetConsultantsByProductQuantity(DateTime startDate, DateTime endDate, string productCode, decimal minQuantityOfProducts)
        {
            var a = Mapper.Map<List<SaleConsultantProductsViewModel>>(_serviceClient.GetConsultantsByProductQuantity(startDate, endDate, productCode, minQuantityOfProducts));

            var json = Json(new { data = a }, JsonRequestBehavior.AllowGet);
            return json;
        }
        public ActionResult GetConsultantsBySummedSales(DateTime? startDate, DateTime? endDate)
        {
            var a = Mapper.Map<List<SaleConsultantProductsViewModel>>(_serviceClient.GetConsultantsBySummedSales(startDate, endDate));

            var json = Json(new { data = a },
              JsonRequestBehavior.AllowGet);
            return json;
        }

        public ActionResult GetConsultantsByTopSoldProducts(DateTime? startDate, DateTime? endDate)
        {
            var a = Mapper.Map<List<SaleConsultantProductsViewModel>>(_serviceClient.GetConsultantsByTopSoldProducts(startDate, endDate));

            var json = Json(new { data = a },
              JsonRequestBehavior.AllowGet);
            return json;
        }
    }
}