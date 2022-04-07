using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSWD_Fysio.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SSWD_Fysio.Controllers
{
    [Authorize]
    public class TreatmentsController : Controller
    {
        private AppAccount appUser;
        private HttpClient client = new HttpClient();
        private List<VektisTreatment> treatments;

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
            PopulateTreatments();

            // Getting all practitioners to fill the selector
            // Might change it to just take the logged on Practitioner instead.
            ViewData["Practitioners"] = new SelectList(practitionerRepo.getAllPractitioners(), "practitionerId", "name");

            TreatmentViewModel vm = new TreatmentViewModel();
            vm.treatment.treatmentDate = DateTime.Now;
            vm.patientFile = fileRepo.FindPatientFileById(id);
            vm.treatmentDate = DateTime.Now.Date;

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
            PopulateTreatments();
            ViewData["Practitioners"] = new SelectList(practitionerRepo.getAllPractitioners(), "practitionerId", "name");

            // Getting the active patient file
            PatientFile pf = fileRepo.FindPatientFileById((int)TempData["PatientFileId"]);
            TempData.Keep();
            model.patientFile = pf;

            // Setting the right plan Id
            Treatment t = new Treatment();
            t.treatmentPlanId = (int)TempData["TreatmentPlanId"];
            TempData.Keep();

            // Pre-post checks
            if (!planRepo.IsPractitionerWorkingOnDay(model.chosenPractitioner, model.treatmentDate))
            {
                ModelState.AddModelError("treatmentDate", "Practitioner does not work on this day. Please pick another date.");
            }
            if (!planRepo.IsPractitionerAvailable(model.chosenPractitioner,
                model.treatmentDate, model.treatmentDate.AddMinutes(pf.treatmentPlan.sessionDuration)))
            {
                ModelState.AddModelError("treatmentDate", "Practitioner is unavailable at this timeslot. Please pick another date.");
            }
            if (planRepo.HasReachedWeeklyLimit(model.treatmentDate.Date, pf.treatmentPlan.treatmentPlanId))
            {
                ModelState.AddModelError("treatmentDate", "Limit of treatments for this week has been reached. Please pick another date.");
            }
            if (model.treatmentDate > pf.dischargeDate)
            {
                ModelState.AddModelError("treatmentDate", "Treatment date can't be after discharge date. Please pick another date.");
            }
            if (model.treatmentDate < pf.entryDate)
            {
                ModelState.AddModelError("treatmentDate", "Treatment date can't be before entry date. Please pick another date.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Getting info from the code list
                    VektisTreatment vektisTreatment = treatments.Find(vt => vt.code == model.treatment.type);
                    t.hasMandatoryExplanation = false;
                    t.description = vektisTreatment.description;
                    if (vektisTreatment.explanation.Equals("Ja"))
                    {
                        t.hasMandatoryExplanation = true;
                    };

                    // Data
                    t.type = vektisTreatment.code;
                    t.location = model.treatment.location;
                    t.practitionerId = model.chosenPractitioner;
                    t.treatmentDate = model.treatmentDate;
                    t.treatmentEndDate = model.treatmentDate.AddMinutes(pf.treatmentPlan.sessionDuration);

                    treatmentRepo.AddTreatment(t);
                    return RedirectToAction("Details", "PatientFiles", new {id = (int)TempData["PatientFileId"]});
                }
            }

            return View(model);
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
            PopulateTreatments();

            // Getting the active patient file
            PatientFile pf = fileRepo.FindPatientFileById((int)TempData["PatientFileId"]);
            TempData.Keep();
            ViewData["Practitioners"] = new SelectList(practitionerRepo.getAllPractitioners(), "practitionerId", "name");


            // Setting the current variables
            TreatmentViewModel vm = new TreatmentViewModel();
            vm.treatment = treatmentRepo.FindTreatmentById(id);
            vm.patientFile = pf;
            vm.treatmentDate = vm.treatment.treatmentDate;
            vm.treatment.treatmentId = id;

            // Setting active editing file ID
            TempData["TreatmentId"] = id;
            TempData.Keep();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TreatmentViewModel model) 
        {
            GetUser();
            PopulateTreatments();
            ViewData["Practitioners"] = new SelectList(practitionerRepo.getAllPractitioners(), "practitionerId", "name");

            // Getting the active patient file
            PatientFile pf = fileRepo.FindPatientFileById((int)TempData["PatientFileId"]);
            TempData.Keep();

            model.patientFile = pf;

            model.treatment.treatmentId = (int)TempData["TreatmentId"];
            TempData.Keep();

            // Setting the right plan Id
            Treatment t = new Treatment();

            // Pre-post checks
            if (!planRepo.IsPractitionerWorkingOnDay(model.chosenPractitioner, model.treatmentDate))
            {
                ModelState.AddModelError("treatmentDate", "Practitioner does not work on this day. Please pick another date.");
            }
            if (!planRepo.IsPractitionerAvailable(model.chosenPractitioner,
                model.treatmentDate, model.treatmentDate.AddMinutes(pf.treatmentPlan.sessionDuration)))
            {
                ModelState.AddModelError("treatmentDate", "Practitioner is unavailable at this timeslot. Please pick another date.");
            }
            if (planRepo.HasReachedWeeklyLimit(model.treatmentDate.Date, pf.treatmentPlan.treatmentPlanId))
            {
                ModelState.AddModelError("treatmentDate", "Limit of treatments for this week has been reached. Please pick another date.");
            }
            if (model.treatmentDate > pf.dischargeDate)
            {
                ModelState.AddModelError("treatmentDate", "Treatment date can't be after discharge date. Please pick another date.");
            }
            if (model.treatmentDate < pf.entryDate)
            {
                ModelState.AddModelError("treatmentDate", "Treatment date can't be before entry date. Please pick another date.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Getting info from the code list
                    VektisTreatment vektisTreatment = treatments.Find(vt => vt.code == model.treatment.type);
                    t.hasMandatoryExplanation = false;
                    t.description = vektisTreatment.description;
                    if (vektisTreatment.explanation.Equals("Ja")) {
                        t.hasMandatoryExplanation = true;
                    };

                    // Data
                    t.type = vektisTreatment.code;
                    t.location = model.treatment.location;
                    t.practitionerId = model.chosenPractitioner;
                    t.treatmentDate = model.treatmentDate;
                    t.treatmentEndDate = model.treatmentDate.AddMinutes(pf.treatmentPlan.sessionDuration);
                    t.treatmentId = (int)TempData["TreatmentId"];
                    TempData.Keep();

                    treatmentRepo.UpdateTreatment(t);
                    
                    return RedirectToAction("Details", "PatientFiles", new { id = (int)TempData["PatientFileId"] });
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            GetUser();

            if (appUser.accountType == AccountType.PATIENT)
            {
                if (treatmentRepo.CanPatientCancel(id))
                {
                    treatmentRepo.DeleteTreatmentById(id);
                }
            }
            else if (appUser.accountType == AccountType.PRACTITIONER){

                treatmentRepo.DeleteTreatmentById(id);
            }

            return RedirectToAction("Index", "PractitionerDashboard");
        }

        private void GetUser()
        {
            string mail = User.Identity.Name;
            appUser = appAccRepo.FindAccountByMail(mail);
        }

        private void PopulateTreatments()
        {
            // Fill diagnosis
            try
            {
                ViewData["TreatmentOptions"] = GetData().Result;
            }
            catch
            {
                ViewData["TreatmentOptions"] = new SelectList("0");
            }
        }

        private async Task<SelectList> GetData()
        {
            treatments = new List<VektisTreatment>();

            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/Treatment");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            treatments = JsonConvert.DeserializeObject<List<VektisTreatment>>(responseBody);
            treatments.Sort((x, y) => String.Compare(x.code, y.code));

            List<string> treatmentOptions = new List<string>();
            foreach (VektisTreatment vt in treatments)
            {
                treatmentOptions.Add(vt.code);
            }

            SelectList output = new SelectList(treatmentOptions);
            return output;
        }
    }
}
