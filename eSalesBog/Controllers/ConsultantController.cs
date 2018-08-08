using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static eSalesBog.Models.SalesViewModels;
using Common.Enums;
using Services.ServiceAbstract;
using Services.DTOs;

namespace eSalesBog.Controllers
{
    public class ConsultantController : Controller
    {

        private ISalesService _serviceClient;

        public ConsultantController(ISalesService serviceClient)
        {
            _serviceClient = serviceClient;
        }

        // GET: Consultant
        public ActionResult Index()
        {
            var dbConsultants = _serviceClient.GetConsultants();
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
            var dbConsultant = _serviceClient.GetConsultantById((int)id);
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
            LoadRecommenderConsultants(null);

            return View();
        }

        // POST: Consultant/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PersonalNumber,Gender,BirthDate,RecommenderConsultantID")] ConsultantViewModel consultant)
        {

            if (ModelState.IsValid)
            {
                if (_serviceClient.CheckIsConsultantRecommender(consultant.RecommenderConsultantID, consultant.PersonalNumber))
                {
                    ModelState.AddModelError(string.Empty, "არჩეული რეკომენდატორი არ შეიძლება იყოს იერარქიულად ამ კონსულტანტის რეკომენდატორი");
                }
                else
                {
                    ConsultantDto c = new ConsultantDto
                    {
                        FirstName = consultant.FirstName,
                        LastName = consultant.LastName,
                        BirthDate = consultant.BirthDate,
                        PersonalNumber = consultant.PersonalNumber,
                        Gender = consultant.Gender,
                        RecommenderConsultantID = consultant.RecommenderConsultantID
                    };
                    _serviceClient.CreateConsultant(c);

                    return RedirectToAction("Index");
                }
            }

            LoadRecommenderConsultants(null);
            return View(consultant);
        }

        // GET: Consultant/Edit/5
        public ActionResult Edit(int? id)
        {
            LoadRecommenderConsultants(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consultant = _serviceClient.GetConsultantById((int)id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PersonalNumber,Gender,BirthDate,RecommenderConsultantID")] ConsultantViewModel consultant)
        {
            if (ModelState.IsValid)
            {
                if (_serviceClient.CheckIsConsultantRecommender(consultant.RecommenderConsultantID, consultant.PersonalNumber))
                {
                    ModelState.AddModelError(string.Empty, "არჩეული რეკომენდატორი არ შეიძლება იყოს იერარქიულად ამ კონსულტანტის რეკომენდატორი");
                }
                else
                {
                    ConsultantDto c = new ConsultantDto
                    {
                        ID = consultant.ID,
                        FirstName = consultant.FirstName,
                        LastName = consultant.LastName,
                        BirthDate = consultant.BirthDate,
                        PersonalNumber = consultant.PersonalNumber,
                        Gender = consultant.Gender,
                        RecommenderConsultantID = consultant.RecommenderConsultantID
                    };
                    _serviceClient.EditConultant(c);
                    return RedirectToAction("Index");
                }
            }
            LoadRecommenderConsultants(null);
            return View(consultant);
        }

        // GET: Consultant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consultant = _serviceClient.GetConsultantById((int)id);
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
            _serviceClient.DeleteConsultant(id);
            return RedirectToAction("Index");
        }


        public void LoadRecommenderConsultants(int? id)
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
            ViewData["RecommenderConsultants"] = consultants;

        }
    }
}
