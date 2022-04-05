using Core.Domain;
using Core.DomainServices;
using Infrastructure.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSWD_Fysio.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SSWD_Fysio.Controllers
{
    public class PatientFilesController : Controller
    {
        AppAccount appUser;
        PractitionerBar practitionerBar;
        private List<VektisDiagnosis> diagnosis;
        static readonly HttpClient client = new HttpClient();

        // Repositories
        private IPatientFileRepository fileRepo;
        private IPatientRepository patientRepo;
        private IPractitionerRepository practitionerRepo;
        private ITreatmentPlanRepository planRepo;
        private ITreatmentRepository treatmentRepo;
        private IAppAccountRepository appAccRepo;
        private ICommentRepository commentRepo;

        private readonly ILogger<HomeController> _logger;

        public PatientFilesController(
            ILogger<HomeController> logger,
            IAppAccountRepository app,
            IPatientFileRepository file,
            IPatientRepository patient,
            IPractitionerRepository practitioner,
            ITreatmentPlanRepository plan,
            ITreatmentRepository treatment,
            ICommentRepository comment)
        {
            appAccRepo = app;
            fileRepo = file;
            patientRepo = patient;
            practitionerRepo = practitioner;
            planRepo = plan;
            treatmentRepo = treatment;
            commentRepo = comment;
            _logger = logger;

            appUser = new AppAccount();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id) {

            // Setting tempdata for future child editing
            TempData["PatientFileId"] = id;
            TempData.Keep();

            // Get patient file, fill in the fields
            PatientFile file = fileRepo.FindPatientFileById(id);
            PatientFileViewModel vm = new PatientFileViewModel(file);
            vm.treatments = treatmentRepo.GetTreatmentsByTreatmentPlanId(file.treatmentPlan.treatmentPlanId);
            vm.comments = commentRepo.GetCommentsByFileId(id);

            // Loading photo if present
            if (vm.patient.photo != null) {
                vm.imgSource = ImageFromByteArray(vm.patient.photo);
            }

            // Getting the user, writing to appUser.
            // Conditionally updating Practitioner Bar PartialView
            GetUser();
            vm.practitionerBar = practitionerBar;

            if (appUser.accountType == AccountType.PATIENT)
            {
                vm.comments = vm.comments.Where(c => c.visible == true).ToList();
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit() {

            // Getting the ID from earlier.
            int id = (int)TempData["PatientFileId"];
            TempData.Keep();

            // Get patient file, fill in the fields
            PatientFile file = fileRepo.FindPatientFileById(id);
            PatientFileViewModel vm = new PatientFileViewModel(file);

            // Populating from Vektis server
            PopulateDiagnosis();

            // Preparing View Data for Intake viewmodel
            ViewData["SexOptions"] = new SelectList(vm.sexOptions);
            ViewData["PatientTypeOptions"] = new SelectList(vm.patientTypeOptions);

            // Getting the user, writing to appUser.
            // Conditionally updating Practitioner Bar PartialView
            GetUser();
            vm.practitionerBar = practitionerBar;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(PatientFileViewModel model) {

            // New file to edit
            PatientFile file = new PatientFile();

            GetUser();
            ViewData["DiagnosisOptions"] = GetDiagnosisData().Result;

            if (ModelState.IsValid) {

                // Setting ID
                file.patientFileId = (int)TempData["PatientFileId"];
                TempData.Keep();

                // Treatment plan
                file.treatmentPlan.diagnosis = model.diagnosis;
                file.treatmentPlan.complaint = model.treatmentPlan.complaint;
                file.treatmentPlan.sessionDuration = model.treatmentPlan.sessionDuration;
                file.treatmentPlan.weeklySessions = model.treatmentPlan.weeklySessions;
                file.treatmentPlan.practitionerId = appUser.practitionerId;

                // Patient photo
                if (model.photo != null) {
                    file.patient.photo = FileToByteArray(model.photo);
                }

                // Patient info
                file.patient.name = model.patientFile.patient.name;
                file.patient.mail = model.patientFile.patient.mail;
                file.patient.phone = model.patientFile.patient.phone;
                file.birthDate = model.patientFile.birthDate;
                file.patient.age = file.patient.CalculateAge(file.birthDate);

                // Changing relevant data based on patient type.
                switch (model.type)
                {
                    case "Employee":
                        file.patient.type = PatientType.EMPLOYEE;
                        file.patient.employeeNumber = model.number;
                        break;

                    case "Student":
                        file.patient.type = PatientType.STUDENT;
                        file.patient.studentNumber = model.number;
                        break;
                }

                // Updating patients sex 
                switch (model.sex)
                {
                    case "Female":
                        file.patient.sex = Sex.FEMALE;
                        break;
                    case "Male":
                        file.patient.sex = Sex.MALE;
                        break;
                    case "Other":
                        file.patient.sex = Sex.OTHER;
                        break;
                }

                // Update file and redirect
                fileRepo.UpdatePatientFile(file);
                return RedirectToAction("Details", "PatientFiles", new { id = (int)TempData["PatientFileId"] });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            fileRepo.DeletePatientFile(id);
            return RedirectToAction("Index", "Home" );
        }

        private void GetUser()
        {
            // Finding an account by email
            string mail = User.Identity.Name;
            appUser = appAccRepo.FindAccountByMail(mail);
            practitionerBar = new PractitionerBar();

            // Setting the practitioner bar values
            if (appUser.accountType == AccountType.PRACTITIONER)
            {
                practitionerBar.amountOfAppointments = treatmentRepo.GetTodaysTreatmentsByPractitionerId(appUser.practitionerId).Count;
                practitionerBar.isPractitioner = true;
            }
        }

        private void PopulateDiagnosis() {
            // Fill diagnosis
            try
            {
                ViewData["DiagnosisOptions"] = GetDiagnosisData().Result;
            }
            catch (Exception ex)
            {
                ViewData["DiagnosisOptions"] = new SelectList("0");
            }
        }

        private async Task<SelectList> GetDiagnosisData() {
            diagnosis = new List<VektisDiagnosis>();

            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/Diagnosis");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            diagnosis = JsonConvert.DeserializeObject<List<VektisDiagnosis>>(responseBody);

            List<string> diagnosisOptions = new List<string>();
            foreach (VektisDiagnosis vk in diagnosis)
            {
                diagnosisOptions.Add(vk.code + ", " + vk.pathology);
            }

            SelectList output = new SelectList(diagnosisOptions);
            return output;
        }

        private byte[] FileToByteArray(IFormFile file) {

            // Converting file to byte array for storage.
            long length = file.Length;
            byte[] bytes;
            using var fileStream = file.OpenReadStream();
            {
                bytes = new byte[length];
                fileStream.Read(bytes, 0, (int)file.Length);
            }

            return bytes;
        }

        private string ImageFromByteArray(byte[] bytes) 
        {
            string pictureData = Convert.ToBase64String(bytes);
            string toReturn = string.Format("data:/image/jpg;base64,{0}", pictureData);

            return toReturn;
        }
    }
}
