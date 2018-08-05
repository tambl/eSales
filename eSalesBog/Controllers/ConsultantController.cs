using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL.Context;
using eSalesBog.DTOs;
using static eSalesBog.Models.SalesViewModels;

namespace eSalesBog.Controllers
{
    public class ConsultantController : Controller
    {
        private SalesBogEntities db = new SalesBogEntities();

        // GET: Consultant
        public ActionResult Index()
        {
            var dbConsultants = db.Consultants.ToList();
            List<ConsultantViewModel> consultants = new List<ConsultantViewModel>();
            foreach (var item in dbConsultants)
            {
                consultants.Add(new ConsultantViewModel
                {
                    FirstName = item.FirstName,
                    LastName=item.LastName
                });
            }
            return View(consultants);
        }

        // GET: Consultant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbConsultant = db.Consultants.Find(id);
            ConsultantDto consultantDto = new ConsultantDto { FirstName = dbConsultant.FirstName, LastName = dbConsultant.LastName };
            if (consultantDto == null)
            {
                return HttpNotFound();
            }
            return View(consultantDto);
        }

        // GET: Consultant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consultant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PersonalNumber,Gender,BirthDate,RecommenderConsultantID")] ConsultantDto consultantDto)
        {
            if (ModelState.IsValid)
            {
                //db.Consultants.Add(consultantDto);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consultantDto);
        }

        // GET: Consultant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantDto consultantDto = null; //db.ConsultantDtoes.Find(id);
            if (consultantDto == null)
            {
                return HttpNotFound();
            }
            return View(consultantDto);
        }

        // POST: Consultant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PersonalNumber,Gender,BirthDate,RecommenderConsultantID")] ConsultantDto consultantDto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultantDto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultantDto);
        }

        // GET: Consultant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantDto consultantDto = null;//db.ConsultantDtoes.Find(id);
            if (consultantDto == null)
            {
                return HttpNotFound();
            }
            return View(consultantDto);
        }

        // POST: Consultant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsultantDto consultantDto = null;// db.ConsultantDtoes.Find(id);
            //db.ConsultantDtoes.Remove(consultantDto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
