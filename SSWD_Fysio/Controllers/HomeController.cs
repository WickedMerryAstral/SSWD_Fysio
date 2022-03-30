using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSWD_Fysio.Models;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.EF;
using Core.DomainServices;
using System.Globalization;

namespace SSWD_Fysio.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // Repositories
        private IPatientFileRepository fileRepo;
        private IPatientRepository patientRepo;
        private IPractitionerRepository practitionerRepo;
        private ITreatmentPlanRepository planRepo;
        private ITreatmentRepository treatmentRepo;
        private IAppAccountRepository appAccRepo;

        private readonly ILogger<HomeController> _logger;

        public HomeController(
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
        }

        public IActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();
            vm.practitionerBar.amountOfAppointments = 3;
            vm.practitionerBar.isPractitioner = true;

            // Test Code Area
            if (practitionerRepo.getAllSupervisors().Count() == 0) {
                FillData();
            }

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void FillData() {

            // Some default data to fill the void
            // In order of creation in business logic:
            // Account
            // Practitioner
            // PatientFile
            // Patients, TreatmentPlans
            // Treatments, Comments

            //// Practitioner, initializing Lists within constructor
            Practitioner pr1 = new Practitioner(
                PractitionerType.TEACHER,
                "John Doe",
                "John@johnmail.com",
                null,
                "00001",
                "0612345678",
                "B00001");

            Practitioner pr2 = new Practitioner(
                PractitionerType.STUDENT,
                "Jane Doe",
                "Jane@janemail.com",
                null,
                "00002",
                "0612345678",
                "B00001");

            // PatientFile, initializing Lists within constructor
            PatientFile pf1 = new PatientFile(
                pr1.practitionerId,
                pr1.practitionerId,
                DateTime.ParseExact("1990-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                PatientFileType.STUDENT,
                DateTime.Now,
                DateTime.Now
                );

            practitionerRepo.AddPractitioner(pr1);
            practitionerRepo.AddPractitioner(pr2);

            pf1.patient = new Patient("00001",
                null,
                "Patience",
                "patient@patientmail.com",
                null,
                DateTime.ParseExact("1990-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Sex.OTHER,
                PatientType.STUDENT);

            pf1.treatmentPlan = new TreatmentPlan(pr1.practitionerId,
                "Alpha",
                "Ouch",
                1,
                60);

            pf1.intakeByPractitionerId = pr1.practitionerId;
            pf1.supervisedBypractitionerId = pr1.practitionerId;

            fileRepo.AddPatientFile(pf1);

            AppAccount practitionerAcc1 = new AppAccount();
            practitionerAcc1.practitionerId = pr1.practitionerId;
            practitionerAcc1.mail = pr1.mail;
            practitionerAcc1.accountType = AccountType.PRACTITIONER;

            AppAccount practitionerAcc2 = new AppAccount();
            practitionerAcc2.practitionerId = pr2.practitionerId;
            practitionerAcc2.mail = pr2.mail;
            practitionerAcc2.accountType = AccountType.PRACTITIONER;

            appAccRepo.AddAppAccount(practitionerAcc1);
            appAccRepo.AddAppAccount(practitionerAcc2);

            //AppAccount patientAcc1 = new AppAccount();
            //patientAcc1.patientId = pf1.patient.patientId;
            //patientAcc1.mail = pf1.patient.mail;
            //patientAcc1.accountType = AccountType.PATIENT;
            // appAccRepo.AddAppAccount(patientAcc1);
        }
    }
}
