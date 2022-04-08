using Core.Domain;
using Core.DomainServices;
using Infrastructure.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSWD_Fysio.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSWD_Fysio.Controllers
{
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
        public IActionResult Login()
        {
            SignInViewModel vm = new SignInViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel model) 
        {
            // Searching through the app account database to determine account type.
            AppAccount acc = _appAccRepo.FindAccountByMail(model.mail);

            if (acc == null)
            {
                // In case the patient had not yet gotten an intake
                User user = await _userManager.FindByEmailAsync(model.mail);
                if (user == null)
                {
                    ModelState.AddModelError("mail", "No users with that email adress found.");
                }
                else {
                    // Signing in async, waiting for call-back.
                    var sign = await _signInManager.PasswordSignInAsync(user, HashPassword(model.password), false, false);
                    if (sign.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else 
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByEmailAsync(model.mail);
                    if (user == null)
                    {
                        ModelState.AddModelError("mail", "No users with that email adress found.");
                    }
                    else 
                    {
                        // Signing in async, waiting for call-back.
                        var sign = await _signInManager.PasswordSignInAsync(user, HashPassword(model.password), false, false);

                        if (sign.Succeeded) {
                            if (acc.accountType == AccountType.PRACTITIONER) {
                                return RedirectToAction("Index", "Home");
                            }
                            else if (acc.accountType == AccountType.PATIENT)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            // Default in case the account has no type. (Should not happen.)
                            return RedirectToAction("Index", "Account");
                        }
                    }
                }
            }

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
                    user.Email = model.mail;
                    user.UserName = model.mail;
                    user.password = HashPassword(model.password);

                    IdentityResult create = await _userManager.CreateAsync(user, user.password);
                    if (create.Succeeded)
                    {
                        _appAccRepo.AddAppAccount(acc);

                        var sign = await _signInManager.PasswordSignInAsync(user, HashPassword(model.password), false, false);
                        if (sign.Succeeded)
                        {
                            // Default in case the account has no type. (Should not happen.)
                            return RedirectToAction("Index", "Account");
                        }
                    }
                    else
                    {
                        foreach (IdentityError error in create.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                // ModelState.AddModelError("mail", "Mail address not found. Be sure to use the mail adress used during the intake.");
                // Adding a patient without the intake having happened yet.

                if (ModelState.IsValid)
                {
                    user.mail = model.mail;
                    user.Email = model.mail;
                    user.UserName = model.mail;
                    user.password = HashPassword(model.password);

                    IdentityResult create = await _userManager.CreateAsync(user, user.password);
                    if (create.Succeeded)
                    {
                        var sign = await _signInManager.PasswordSignInAsync(user, HashPassword(model.password), false, false);
                        if (sign.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        foreach (IdentityError error in create.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult RegisterPractitioner()
        {
            RegisterPractitionerViewModel model = new RegisterPractitionerViewModel();

            List<string> options = new List<string>();
            options.Add("Teacher");
            options.Add("Student");

            ViewData["PractitionerType"] = new SelectList(options);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPractitioner(RegisterPractitionerViewModel model)
        {
            Practitioner p = new Practitioner();
            AppAccount a = new AppAccount();
            User user = new User();

            // Setting info based on practitioner type.
            if (model.chosenType.Equals("Student"))
            {
                p.type = PractitionerType.STUDENT;
                p.studentNumber = model.number;
            }
            else if (model.chosenType.Equals("Teacher"))
            {
                p.type = PractitionerType.TEACHER;
                p.employeeNumber = model.number;
                p.BIGNumber = model.BIGnumber;
            }

            // General info.
            p.name = model.name;
            p.mail = model.mail;
            p.phone = model.phone;

            user.mail = model.mail;
            user.Email = model.mail;
            user.UserName = model.mail;
            user.password = HashPassword(model.password);

            // Available on these days
            p.availableMON = model.availableMON;
            p.availableTUE = model.availableTUE;
            p.availableWED = model.availableWED;
            p.availableTHU = model.availableTHU;
            p.availableFRI = model.availableFRI;
            p.availableSAT = model.availableSAT;
            p.availableSUN = model.availableSUN;

            if (ModelState.IsValid)
            {
                IdentityResult create = await _userManager.CreateAsync(user, user.password);
                if (create.Succeeded)
                {
                    // Posting practitioner, getting ID after
                    a.practitionerId = _practRepo.AddPractitioner(p);
                    a.mail = model.mail;
                    a.accountType = AccountType.PRACTITIONER;

                    // Adding app account for linking purposes
                    _appAccRepo.AddAppAccount(a);

                    var sign = await _signInManager.PasswordSignInAsync(user, HashPassword(model.password), false, false);
                    if (sign.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    foreach (IdentityError error in create.Errors)
                    {
                        ModelState.AddModelError("IDENTITY_ERROR", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            TempData.Clear();
            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Account");
        }

        public string HashPassword(string password) {
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
    }
}
