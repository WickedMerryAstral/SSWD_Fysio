using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSWD_Fysio.Models;
using System;

namespace SSWD_Fysio.Controllers
{
    [Authorize]
    public class PractitionerDashboardController : Controller
    {
        private AppAccount appUser;
        private PractitionerBar practitionerBar;

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
            // Getting the current user, writing to appaccount
            GetUser();

            if (appUser.accountType == AccountType.PRACTITIONER) {
                // Generating the object for data viewing
                PractitionerDashboardViewModel vm = new PractitionerDashboardViewModel();
                vm.allFiles = fileRepo.GetPatientFiles();
                vm.practitionerFiles = fileRepo.FindPatientFilesByPractitionerId(appUser.practitionerId);

                // Finding and sorting treatments
                vm.treatments = treatmentRepo.GetTodaysTreatmentsByPractitionerId(appUser.practitionerId);
                vm.treatments.Sort((x, y) => DateTime.Compare(x.treatmentDate, y.treatmentDate));

                vm.practitionerBar.amountOfAppointments = vm.treatments.Count;
                vm.practitionerBar.isPractitioner = true;

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
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
    }
}
