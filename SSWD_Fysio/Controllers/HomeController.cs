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

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IPatientFileRepository file,
            IPatientRepository patient,
            IPractitionerRepository practitioner,
            ITreatmentPlanRepository plan,
            ITreatmentRepository treatment)
        {
            fileRepo = file;
            patientRepo = patient;
            practitionerRepo = practitioner;
            planRepo = plan;
            treatmentRepo = treatment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Test Code Area
            FillData();

            return View();
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
            // Practitioner
            // Treatment Plans and Patient File
            // The Patient
            // Treatments and Comments

            //// Practitioner, initializing Lists within constructor
            Practitioner pr1 = new Practitioner(
                PractitionerType.TEACHER,
                "John Doe",
                "John@johnmail.com",
                null,
                "00001",
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
            fileRepo.AddPatientFile(pf1);
        }
    }
}
