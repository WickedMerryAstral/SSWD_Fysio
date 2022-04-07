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
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace SSWD_Fysio.Controllers
{
    public class HomeController : Controller
    {
        private AppAccount appUser;

        // Identity
        private UserManager<User> _userManager;

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
            ITreatmentRepository treatment,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            appAccRepo = app;
            fileRepo = file;
            patientRepo = patient;
            practitionerRepo = practitioner;
            planRepo = plan;
            treatmentRepo = treatment;

            // Identity
            _userManager = userManager;

            // Logging
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Getting Identity user
            GetUser();

            // Account with this e-mail gets created during seeding. This will enforce the existence of these 4 accounts.
            if (appAccRepo.FindAccountByMail("friday@outlook.com") == null) {
                Seed();
            }

            HomeViewModel model = new HomeViewModel();

            if (!User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Account");
            }

            if (appUser != null)
            {
                switch (appUser.accountType)
                {
                    case AccountType.PRACTITIONER:
                        return RedirectToAction("Index", "PractitionerDashboard");

                    case AccountType.PATIENT:
                        Patient patient = patientRepo.FindPatientById(appUser.patientId);
                        if (patient != null)
                        {
                            return RedirectToAction("Details", "PatientFiles", new { id = patient.patientFileId });
                        }
                        break;
                }
            }
            else {
                model.noAccount = true;
            }

            return View(model);
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

        public void Seed() {

            // Making 2 practitioner accounts, 2 patient accounts.
            // ################################################################
            // Friday : Supervisor / Teacher Practitioner (Only works fridays.)
            // Weekend: Student Practitioner (Only works weekends.)
            // John Doe: Empty patient for intake later.
            // Jane Doe: Empty patient which will not have an intake.

            #region PRACTITIONER 1: Friday - Supervisor / Teacher

            // Practitioner details
            Practitioner friday = new Practitioner();
            AppAccount fridayAcc = new AppAccount();
            User fridayUser = new User();

            string fridayEmail = "friday@outlook.com";

            friday.type = PractitionerType.TEACHER;
            friday.employeeNumber = "12345";
            friday.BIGNumber = "BIG12345";
            friday.name = "Friday Chiron";
            friday.mail = fridayEmail;
            friday.phone = "0612345678";

            // Availability
            friday.availableMON = false;
            friday.availableTUE = false;
            friday.availableWED = false;
            friday.availableTHU = false;
            friday.availableFRI = true;
            friday.availableSAT = false;
            friday.availableSUN = false;

            // Identity user
            fridayUser.mail = fridayEmail;
            fridayUser.Email = fridayEmail;
            fridayUser.UserName = fridayEmail;
            fridayUser.password = HashPassword("!Friday123");
            _userManager.CreateAsync(fridayUser, fridayUser.password);

            fridayAcc.practitionerId = practitionerRepo.AddPractitioner(friday);
            fridayAcc.mail = friday.mail;
            fridayAcc.accountType = AccountType.PRACTITIONER;
            appAccRepo.AddAppAccount(fridayAcc);

            #endregion

            #region PRACTITIONER 2: Weekend - Student

            // Practitioner details
            Practitioner weekend = new Practitioner();
            AppAccount weekendAcc = new AppAccount();
            User weekendUser = new User();

            string weekendEmail = "weekend@outlook.com";

            weekend.type = PractitionerType.STUDENT;
            weekend.studentNumber = "6789";
            weekend.name = "Weekend Studious";
            weekend.mail = weekendEmail;
            weekend.phone = "0687654321";

            // Availability
            weekend.availableMON = false;
            weekend.availableTUE = false;
            weekend.availableWED = false;
            weekend.availableTHU = false;
            weekend.availableFRI = false;
            weekend.availableSAT = true;
            weekend.availableSUN = true;

            // Identity user
            weekendUser.mail = weekendEmail;
            weekendUser.Email = weekendEmail;
            weekendUser.UserName = weekendEmail;
            weekendUser.password = HashPassword("!Weekend123");
            _userManager.CreateAsync(weekendUser, weekendUser.password);

            weekendAcc.practitionerId = practitionerRepo.AddPractitioner(weekend);
            weekendAcc.mail = weekend.mail;
            weekendAcc.accountType = AccountType.PRACTITIONER;
            appAccRepo.AddAppAccount(weekendAcc);

            #endregion

            #region PATIENT 1: John 
            User johnUser = new User();

            string johnEmail = "john@outlook.com";

            johnUser.mail = johnEmail;
            johnUser.Email = johnEmail;
            johnUser.UserName = johnEmail;
            johnUser.password = HashPassword("!John123");

            _userManager.CreateAsync(johnUser, johnUser.password);
            #endregion

            #region PATIENT 2: Jane
            User janeUser = new User();

            string janeEmail = "jane@outlook.com";

            janeUser.mail = janeEmail;
            janeUser.Email = janeEmail;
            janeUser.UserName = janeEmail;
            janeUser.password = HashPassword("!Jane123");

            _userManager.CreateAsync(janeUser, janeUser.password);
            #endregion
        }


        private void GetUser()
        {
            if (User.Identity.Name != null) {
                string mail = User.Identity.Name;
                appUser = appAccRepo.FindAccountByMail(mail);

                if (appUser == null) {
                    Patient p = patientRepo.FindPatientByMail(mail);

                    if (p != null) {
                        AppAccount app = new AppAccount();
                        app.mail = mail;
                        app.accountType = AccountType.PATIENT;
                        app.patientId = p.patientId;

                        appAccRepo.AddAppAccount(app);
                        appUser = app;
                    }
                }
            }
        }

        public string HashPassword(string password)
        {
            string salt = "aD3LCOSR4G0NR0LSc";
            byte[] bytes = Encoding.ASCII.GetBytes(salt);

            // Hashing the password using HMACSHA256
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: bytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public void FillData_old()
        {

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
        }

    }
}
