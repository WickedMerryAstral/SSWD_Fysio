using Core.Domain;
using Core.DomainServices;
using Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSWD_Fysio.Models;

namespace SSWD_Fysio.Controllers
{
    public class PatientFilesController : Controller
    {
        AppAccount appUser;

        // Repositories
        private IPatientFileRepository fileRepo;
        private IPatientRepository patientRepo;
        private IPractitionerRepository practitionerRepo;
        private ITreatmentPlanRepository planRepo;
        private ITreatmentRepository treatmentRepo;
        private IAppAccountRepository appAccRepo;

        private readonly ILogger<HomeController> _logger;

        public PatientFilesController(
            ILogger<HomeController> logger,
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

            // Getting the user, writing to appUser.
            // Conditionally updating Practitioner Bar PartialView
            GetUser();
            if (appUser.accountType == AccountType.PRACTITIONER) {
                vm.practitionerBar.isPractitioner = true;
                vm.practitionerBar.amountOfAppointments = treatmentRepo.GetTodaysTreatmentsCount(appUser.practitionerId);
            }

            return View(vm);
        }

        private void GetUser() {
            string mail = User.Identity.Name;
            appUser = appAccRepo.FindAccountByMail(mail);
        }
    }
}
