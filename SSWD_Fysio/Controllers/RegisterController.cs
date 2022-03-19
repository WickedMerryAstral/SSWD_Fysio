using Core.Domain;
using Core.DomainServices;
using Infrastructure.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSWD_Fysio.Models;
using System.Threading.Tasks;

namespace SSWD_Fysio.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly FysioDBContext _context;
        private IPractitionerRepository _practRepo;

        public RegisterController(IPractitionerRepository practitioner,
            FysioDBContext context)
        {
            this._practRepo = practitioner;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Practitioner()
        {
            PractitionerRegisterViewModel vm = new PractitionerRegisterViewModel();
            return View(vm);
        }
        public IActionResult Patient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Practitioner(PractitionerRegisterViewModel model)
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
