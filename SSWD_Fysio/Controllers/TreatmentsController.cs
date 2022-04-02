using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SSWD_Fysio.Models;
using System;
using System.Collections.Generic;

namespace SSWD_Fysio.Controllers
{
    [Authorize]
    public class TreatmentsController : Controller
    {
        private AppAccount appUser;
        static List<VektisTreatment> vektisTreatments;

        // Repositories
        private IPatientFileRepository fileRepo;
        private IPatientRepository patientRepo;
        private IPractitionerRepository practitionerRepo;
        private ITreatmentPlanRepository planRepo;
        private ITreatmentRepository treatmentRepo;
        private IAppAccountRepository appAccRepo;

        private readonly ILogger<TreatmentsController> _logger;

        public TreatmentsController(
            ILogger<TreatmentsController> logger,
            IAppAccountRepository app,
            IPatientFileRepository file,
            IPatientRepository patient,
            IPractitionerRepository practitioner,
            ITreatmentPlanRepository plan,
            ITreatmentRepository treatment)
        {
            appAccRepo = app;
            fileRepo = file;
            patientRepo = patient;
            practitionerRepo = practitioner;
            planRepo = plan;
            treatmentRepo = treatment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Create(int id) {

            // Getting the treatment codes from the database
            GetVektisTreatments();

            // Getting all practitioners to fill the selector
            // Might change it to just take the logged on Practitioner instead.
            ViewData["Practitioners"] = new SelectList(practitionerRepo.getAllPractitioners(), "practitionerId", "name");
            ViewData["VektisTreatments"] = new SelectList(vektisTreatments, "treatmentCode", "treatmentCode");

            TreatmentViewModel vm = new TreatmentViewModel();
            vm.patientFile = fileRepo.FindPatientFileById(id);

            // Setting active editing file ID
            TempData["TreatmentPlanId"] = vm.patientFile.treatmentPlan.treatmentPlanId;
            TempData.Keep();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(TreatmentViewModel model) 
        {
            // Getting the user
            GetUser();

            // Getting the treatment codes from the database
            GetVektisTreatments();

            // Getting the active patient file
            PatientFile pf = fileRepo.FindPatientFileById((int)TempData["PatientFileId"]);
            TempData.Keep();
            model.patientFile = pf;

            // Setting the right plan Id
            Treatment t = new Treatment();
            t.treatmentPlanId = (int)TempData["TreatmentPlanId"];

            if (model.treatmentDate < pf.dischargeDate)
            {
                ModelState.AddModelError("INVALID_DATE", "Treatment date can't be after discharge date.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Getting info from the code list
                    VektisTreatment vektisTreatment = vektisTreatments.Find(vt => vt.treatmentCode == model.treatment.type);
                    t.hasMandatoryExplanation = vektisTreatment.hasMandatoryExplanation;
                    t.description = vektisTreatment.description;
                    t.type = model.treatment.type;

                    // Data
                    t.location = model.treatment.location;
                    t.practitionerId = model.chosenPractitioner;
                    t.treatmentDate = model.treatmentDate;

                    treatmentRepo.AddTreatment(t);
                    return RedirectToAction("Details", "PatientFiles", new {id = (int)TempData["PatientFileId"]});
                }
            }

            return RedirectToAction("Create", "Treatments", (int)TempData["PatientFileId"]);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            // Loading in data
            TreatmentViewModel vm = new TreatmentViewModel();
            vm.treatment = treatmentRepo.FindTreatmentById(id);
            vm.currentPractitioner = practitionerRepo.GetPractitionerById(vm.treatment.practitionerId);


            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            GetUser();
            GetVektisTreatments();

            // Getting the active patient file
            PatientFile pf = fileRepo.FindPatientFileById((int)TempData["PatientFileId"]);
            TempData.Keep();

            ViewData["Practitioners"] = new SelectList(practitionerRepo.getAllPractitioners(), "practitionerId", "name");
            ViewData["VektisTreatments"] = new SelectList(vektisTreatments, "treatmentCode", "treatmentCode");

            // Setting the current variables
            TreatmentViewModel vm = new TreatmentViewModel();
            vm.treatment = treatmentRepo.FindTreatmentById(id);
            vm.patientFile = pf;

            // Setting active editing file ID
            TempData["TreatmentId"] = id;
            TempData.Keep();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TreatmentViewModel model) 
        {
            GetUser();
            GetVektisTreatments();

            // Getting the active patient file
            PatientFile pf = fileRepo.FindPatientFileById((int)TempData["PatientFileId"]);
            TempData.Keep();
            model.patientFile = pf;

            // Setting the right plan Id
            Treatment t = new Treatment();

            if (model.treatmentDate < pf.dischargeDate)
            {
                ModelState.AddModelError("INVALID_DATE", "Treatment date can't be after discharge date.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Getting info from the code list
                    VektisTreatment vektisTreatment = vektisTreatments.Find(vt => vt.treatmentCode == model.treatment.type);
                    t.hasMandatoryExplanation = vektisTreatment.hasMandatoryExplanation;
                    t.description = vektisTreatment.description;

                    // Data
                    t.type = vektisTreatment.treatmentCode;
                    t.location = model.treatment.location;
                    t.practitionerId = model.chosenPractitioner;
                    t.treatmentDate = model.treatmentDate;
                    t.treatmentId = (int)TempData["TreatmentId"];

                    treatmentRepo.UpdateTreatment(t);
                    
                    return RedirectToAction("Details", "PatientFiles", new { id = (int)TempData["PatientFileId"] });
                }
            }

            return RedirectToAction("Edit", "Treatments", (int)TempData["PatientFileId"]);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            treatmentRepo.DeleteTreatmentById(id);
            return RedirectToAction("Index", "PractitionerDashboard");
        }

        public void GetVektisTreatments() {
            vektisTreatments = new List<VektisTreatment>();
            vektisTreatments.Add(new VektisTreatment("Alpha", "First this, then that", true));
            vektisTreatments.Add(new VektisTreatment("Beta", "First that, then this", false));
        }
        private void GetUser()
        {
            string mail = User.Identity.Name;
            appUser = appAccRepo.FindAccountByMail(mail);
        }

        // Private class for loading on Vektis Codes from the server.
        private class VektisTreatment {
            public string treatmentCode { get; set; }
            public string description { get; set; }
            public bool hasMandatoryExplanation { get; set; }

            public VektisTreatment(string treatmentCode, string description, bool hasMandatoryExplanation)
            {
                this.treatmentCode = treatmentCode;
                this.description = description;
                this.hasMandatoryExplanation = hasMandatoryExplanation;
            }
        }
    }
}
