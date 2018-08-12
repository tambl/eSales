using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using eSalesBog.Models;
using Services.ServiceAbstract;
using Services.DTOs;
using System.Text;

namespace eSalesBog.Controllers
{
    public class SalesController : Controller
    {
        private ISalesService _serviceClient;
        public SalesController(ISalesService serviceClient)
        {
            _serviceClient = serviceClient;
        }


        public ActionResult Index()
        {

            var dbSales = _serviceClient.GetSales();
            List<SalesViewModel> sales = new List<SalesViewModel>();
            foreach (var item in dbSales)
            {
                sales.Add(new SalesViewModel
                {
                    ID = item.ID,
                    SaleDate = item.SaleDate,
                    SaleDescription = item.SaleDescription,
                    ConsultantID = (int)item.ConsultantID,
                    ConsultantDescription = item.Consultant.PersonalNumber + " - " + item.Consultant.FirstName + " " + item.Consultant.LastName,
                    ProductID = item.ProductID,
                    ProductsDescription = item.Products.Aggregate(new StringBuilder(), (sb, a) => sb.AppendLine(String.Join(",", a.ProductName)), sb => sb.ToString())
                });

            }
            return View(sales);
        }

        public ActionResult Details(int? id)
        {
            LoadSelectLists();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbSale = _serviceClient.GetSaleById(id);
            SalesViewModel salesViewModel = Mapper.Map<SalesViewModel>(dbSale);


            salesViewModel.ConsultantDescription = salesViewModel.Consultant.FirstName + " " + salesViewModel.Consultant.LastName;


            //LoadSelectLists("Consultant", salesViewModel.Consultant.ID);

            if (salesViewModel == null)
            {
                return HttpNotFound();
            }
            return View(salesViewModel);
        }

        public ActionResult Create()
        {
            LoadSelectLists();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SaleDate,ConsultantID,ProductID,SaleDescription")] SalesViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<ProductDto> products = new List<ProductDto>();
                products.Add(new ProductDto { ID = 1, ProductCount = 10 });
                products.Add(new ProductDto { ID = 3, ProductCount = 2 });
                var testModel = new SalesDto
                {
                    ConsultantID = 9,
                    SaleDescription = "sale4",
                    Products = products
                };

                //var modelProducts = new List<ProductDto>();
                //var servModel = new SalesDto
                //{
                //    ConsultantID = model.ConsultantID,
                //    SaleDescription = model.SaleDescription
                //};

                //servModel.Products = model.Products.Select(a =>
                //new ProductDto
                //{
                //    ProductCount = a.ProductCount,
                //    ID = a.ID
                //}).ToList();

                _serviceClient.CreateSale(testModel);

                return RedirectToAction("Index");
            }

            LoadSelectLists();

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            LoadSelectLists();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbSale = _serviceClient.GetSaleById(id);
            SalesViewModel salesViewModel = Mapper.Map<SalesViewModel>(dbSale);

            if (salesViewModel == null)
            {
                return HttpNotFound();
            }
            return View(salesViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SaleDate,ConsultantID,ProductID,SaleDescription")] SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                //Test
                List<ProductDto> products = new List<ProductDto>();
                products.Add(new ProductDto { ID = 1, ProductCount = 10, IsDeleted = false });
                //products.Add(new ProductDto { ID = 3, ProductCount = 2, IsDeleted = true });
                products.Add(new ProductDto { ID = 2, ProductCount = 15, IsDeleted = false });
                var c1 = new SalesDto
                {
                    ID = 4,
                    ConsultantID = 9,
                    SaleDescription = "sale4",
                    Products = products
                };

                var prods = salesViewModel.Products.Select(s => new ProductDto
                {
                    ID = s.ID,
                    IsDeleted = s.IsDeleted,
                    ProductCount = s.ProductCount
                }).ToList();

                SalesDto c = new SalesDto
                {
                    ID = salesViewModel.ID,
                    SaleDescription = salesViewModel.SaleDescription,
                    Products = prods
                };
                _serviceClient.EditSales(c);
                return RedirectToAction("Index");
            }
            return View(salesViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbSale = _serviceClient.GetSaleById(id);
            SalesViewModel salesViewModel = Mapper.Map<SalesViewModel>(dbSale);

            if (salesViewModel == null)
            {
                return HttpNotFound();
            }
            return View(salesViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _serviceClient.DeleteSale(id);

            return RedirectToAction("Index");
        }
       
        public void LoadSelectLists(string selectorName = null, int? selectedItemID = null)
        {
            var dbConsultants = _serviceClient.GetConsultants();

            List<SelectListItem> consultants = new List<SelectListItem>();
            foreach (var item in dbConsultants)
            {
                consultants.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.PersonalNumber + " " + item.FirstName + " " + item.LastName,
                    Selected = selectedItemID != null && item.ID == selectedItemID && !string.IsNullOrEmpty(selectorName) && selectorName == "Consultant" ? true : false
                });
            }
            var dbProducts = _serviceClient.GetProducts();

            List<SelectListItem> products = new List<SelectListItem>();
            foreach (var item in dbProducts)
            {
                products.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.ProductCode + " " + item.ProductName + " - " + item.Price,
                    Selected = selectedItemID != null && item.ID == selectedItemID && !string.IsNullOrEmpty(selectorName) && selectorName == "Products" ? true : false
                });
            }

            ViewData["Consultants"] = consultants;
            ViewData["Products"] = products;

        }
       
    }
}
