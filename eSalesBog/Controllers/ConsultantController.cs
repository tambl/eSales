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
using Common.Enums;

namespace eSalesBog.Controllers
{
    public class ConsultantController : Controller
    {
        private SalesBogEntities db;

        public ConsultantController()
        {

            db = new SalesBogEntities();
        }

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
                    LastName = item.LastName,
                    Gender = item.Gender,
                    BirthDate = item.BirthDate.Value.Date,
                    ID = item.ID,
                    PersonalNumber = item.PersonalNumber
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
            ConsultantViewModel consultantDto = new ConsultantViewModel { FirstName = dbConsultant.FirstName, LastName = dbConsultant.LastName, PersonalNumber = dbConsultant.PersonalNumber };
            if (consultantDto == null)
            {
                return HttpNotFound();
            }
            return View(consultantDto);
        }

        // GET: Consultant/Create
        public ActionResult Create()
        {
            GetRecommenderConsultants();
            return View();
        }

        // POST: Consultant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PersonalNumber,Gender,BirthDate,RecommenderConsultantID")] ConsultantViewModel consultant)
        {
            if (ModelState.IsValid)
            {
                db.Consultants.Add(new Consultants
                {
                    FirstName = consultant.FirstName,
                    LastName = consultant.LastName,
                    BirthDate = consultant.BirthDate,
                    PersonalNumber = consultant.PersonalNumber,
                    Gender = consultant.Gender,
                    RecommenderConsultantID = consultant.RecommenderConsultantID
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consultant);
        }

        // GET: Consultant/Edit/5
        public ActionResult Edit(int? id)
        {
            GetRecommenderConsultants();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consultant = db.Consultants.Find(id);
            ConsultantViewModel consultantModel = new ConsultantViewModel
            {
                FirstName = consultant.FirstName,
                LastName = consultant.LastName,
                BirthDate = consultant.BirthDate,
                PersonalNumber = consultant.PersonalNumber,
                Gender = consultant.Gender,
                RecommenderConsultantID = consultant.RecommenderConsultantID
            };
            if (consultantModel == null)
            {
                return HttpNotFound();
            }
            return View(consultantModel);
        }

        // POST: Consultant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PersonalNumber,Gender,BirthDate,RecommenderConsultantID")] ConsultantViewModel consultant)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(consultant).State = EntityState.Modified;
                var dbConsultant = db.Consultants.Where(s => s.ID == consultant.ID).FirstOrDefault();
                dbConsultant.FirstName = consultant.FirstName;
                dbConsultant.LastName = consultant.LastName;
                dbConsultant.Gender = consultant.Gender;
                dbConsultant.BirthDate = consultant.BirthDate;
                dbConsultant.RecommenderConsultantID = consultant.RecommenderConsultantID;
                dbConsultant.PersonalNumber = consultant.PersonalNumber;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultant);
        }

        // GET: Consultant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consultant = db.Consultants.Find(id);
            ConsultantViewModel consultantModel = new ConsultantViewModel
            {
                FirstName = consultant.FirstName,
                LastName = consultant.LastName,
                BirthDate = consultant.BirthDate,
                PersonalNumber = consultant.PersonalNumber,
                Gender = consultant.Gender,
                RecommenderConsultantID = consultant.RecommenderConsultantID
            };
           
            if (consultantModel == null)
            {
                return HttpNotFound();
            }
            return View(consultantModel);
        }

        // POST: Consultant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultants consultant =  db.Consultants.Find(id);
            db.Consultants.Remove(consultant);
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

        public void GetRecommenderConsultants()
        {
            List<ConsultantViewModel> result1 = new List<ConsultantViewModel>();

            var dbConsultants = db.Consultants.ToList();
            List<SelectListItem> consultants = new List<SelectListItem>();
            foreach (var item in dbConsultants)
            {
                consultants.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.PersonalNumber + " " + item.FirstName + " " + item.LastName
                });
            }


            ViewData["RecommenderConsultants"] = consultants;

        }
    }
}
