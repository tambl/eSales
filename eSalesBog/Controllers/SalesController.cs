using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using eSalesBog.Models;
using Services.ServiceAbstract;
using Services.DTOs;

namespace eSalesBog.Controllers
{
    public class SalesController : Controller
    {
        private ISalesService _serviceClient;
        public SalesController(ISalesService serviceClient)
        {
            _serviceClient = serviceClient;
        }
   

        // GET: Sales
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
                    ProductID = item.ProductID
                });
            }
            return View(sales);
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesViewModel salesViewModel = null;//db.SalesViewModels.Find(id);
            if (salesViewModel == null)
            {
                return HttpNotFound();
            }
            return View(salesViewModel);
        }

        // GET: Sales/Create
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
                var modelProducts = new List<ProductDto>();
                var servModel = new SalesDto
                {
                    ConsultantID = model.ConsultantID,
                    SaleDescription = model.SaleDescription
                };
                
                servModel.Products = model.Products.Select(a =>
                new ProductDto
                {
                    ProductCount = a.ProductCount,
                    ID = a.ID
                }).ToList();

                //var result = _serviceClient.CreateCompany(servModel);

                return RedirectToAction("Index");
            }

            LoadSelectLists();

            return View(model);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesViewModel salesViewModel = null;//db.SalesViewModels.Find(id);
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
                //db.Entry(salesViewModel).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesViewModel);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesViewModel salesViewModel = null;//db.SalesViewModels.Find(id);
            if (salesViewModel == null)
            {
                return HttpNotFound();
            }
            return View(salesViewModel);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // SalesViewModel salesViewModel = db.SalesViewModels.Find(id);
            //db.SalesViewModels.Remove(salesViewModel);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void LoadSelectLists()
        {
            var dbConsultants = _serviceClient.GetConsultants();

            List<SelectListItem> consultants = new List<SelectListItem>();
            foreach (var item in dbConsultants)
            {
                consultants.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.PersonalNumber + " " + item.FirstName + " " + item.LastName
                });
            }
            var dbProducts = _serviceClient.GetProducts();

            List<SelectListItem> products = new List<SelectListItem>();
            foreach (var item in dbProducts)
            {
                products.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.ProductCode + " " + item.ProductName + " - " + item.Price
                });
            }

            ViewData["Consultants"] = consultants;
            ViewData["Products"] = products;

        }
    }
}
