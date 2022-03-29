using Core.Domain;
using Core.DomainServices;
using Infrastructure.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SSWD_Fysio.Models;
using System.Threading.Tasks;

namespace SSWD_Fysio.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // Repositories
        private IPractitionerRepository _practRepo;
        private IAppAccountRepository _appAccRepo;
        private IPatientRepository _patRepo;

        // Identity
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(
            IPractitionerRepository practitioner,
            IAppAccountRepository appAccRepo,
            IPatientRepository patient,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this._appAccRepo = appAccRepo;
            this._practRepo = practitioner;
            this._patRepo = patient;

            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterPatient()
        {
            RegisterPatientViewModel model = new RegisterPatientViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterPatient(RegisterPatientViewModel model)
        {
            Patient p = _patRepo.FindPatientByMail(model.mail);
            AppAccount acc = new AppAccount();
            User user = new User();

            if (p != null)
            {
                if (ModelState.IsValid)
                {
                    acc.mail = model.mail;
                    acc.accountType = AccountType.PATIENT;
                    acc.patientId = p.patientId;
                    user.mail = model.mail;

                    IdentityResult result = await _userManager.CreateAsync(user, user.password);
                    if (result.Succeeded)
                    {
                        _appAccRepo.AddAppAccount(acc);

                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Mail address not found. Be sure to use the mail adress used during the intake.");
            }

            return View();
        }

        [HttpGet]
        public IActionResult RegisterPractitioner()
        {
            RegisterPractitionerViewModel model = new RegisterPractitionerViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult RegisterPractitioner(RegisterPractitionerViewModel model)
        {

            Practitioner p = new Practitioner();

            if (ModelState.IsValid)
            {
                if (model.chosenType.Equals("Student"))
                {
                    p.type = PractitionerType.STUDENT;
                    p.studentNumber = model.number;
                }
                else if (model.chosenType.Equals("Teacher"))
                {
                    p.type = PractitionerType.TEACHER;
                    p.employeeNumber = model.number;
                }

                _practRepo.AddPractitioner(p);
            }
            return View("Index");
        }
    }
}
