using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Services.DTOs;
using Services.ServiceAbstract;
using static eSalesBog.Models.SalesViewModels;

namespace eSalesBog.Controllers
{
    public class ProductController : Controller
    {
        
        private ISalesService _serviceClient;

        public ProductController(ISalesService serviceClient)
        {
            _serviceClient = serviceClient;
        }

        // GET: Product
        public ActionResult Index()
        {
            var dbProducts = _serviceClient.GetProducts();
            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach (var item in dbProducts)
            {
                products.Add(new ProductViewModel
                {
                    ID = item.ID,
                    Price = item.Price,
                    ProductCode = item.ProductCode,
                    ProductName = item.ProductName
                });
            }
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbProduct = _serviceClient.GetProductById((int)id);
            ProductViewModel productDto = new ProductViewModel
            {
                ID = dbProduct.ID,
                Price = dbProduct.Price,
                ProductCode = dbProduct.ProductCode,
                ProductName = dbProduct.ProductName
            };
            if (productDto == null)
            {
                return HttpNotFound();
            }
            return View(productDto);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductCode,ProductName,Price")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                ProductDto c = new ProductDto
                {
                    Price = product.Price,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                };
                _serviceClient.CreateProduct(c);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Product = _serviceClient.GetProductById((int)id);
            ProductViewModel ProductModel = new ProductViewModel
            {
                ID = Product.ID,
                Price = Product.Price,
                ProductCode = Product.ProductCode,
                ProductName = Product.ProductName,
            };
            if (ProductModel == null)
            {
                return HttpNotFound();
            }
            return View(ProductModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductCode,ProductName,Price")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                ProductDto c = new ProductDto
                {
                    ID = product.ID,
                    Price = product.Price,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                };
                _serviceClient.EditProduct(c);
                return RedirectToAction("Index");

            }

            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _serviceClient.GetProductById((int)id);
            ProductViewModel productModel = new ProductViewModel
            {
                ID = product.ID,
                Price = product.Price,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
            };

            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _serviceClient.DeleteProduct(id);
            return RedirectToAction("Index");
        }

      
    }
}
