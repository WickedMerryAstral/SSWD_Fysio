using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSWD_Fysio.Models;

namespace SSWD_Fysio.Controllers
{
    [Authorize]
    public class PractitionerDashboardController : Controller
    {
        private AppAccount appUser;

        // Repositories
        private IPatientFileRepository fileRepo;
        private IPatientRepository patientRepo;
        private IPractitionerRepository practitionerRepo;
        private ITreatmentPlanRepository planRepo;
        private ITreatmentRepository treatmentRepo;
        private IAppAccountRepository appAccRepo;

        private readonly ILogger<PractitionerDashboardController> _logger;

        public PractitionerDashboardController(
            ILogger<PractitionerDashboardController> logger,
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

        public IActionResult Index()
        {
            string mail = User.Identity.Name;
            appUser = appAccRepo.FindAccountByMail(mail);

            PractitionerDashboardViewModel vm = new PractitionerDashboardViewModel();
            vm.allFiles = fileRepo.GetPatientFiles();
            vm.practitionerFiles = fileRepo.FindPatientFilesByPractitionerId(appUser.practitionerId);
            vm.PopulateTreatments(appUser.practitionerId);

            vm.practitionerBar.amountOfAppointments = treatmentRepo.GetTodaysTreatmentsCount(appUser.practitionerId);
            vm.practitionerBar.isPractitioner = true;

            return View(vm);
        }
    }
}
