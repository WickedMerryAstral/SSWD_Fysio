using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSWD_Fysio.Models;
using System;

namespace SSWD_Fysio.Controllers
{
    public class CommentsController : Controller
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
        private ICommentRepository commentRepo;

        private readonly ILogger<CommentsController> _logger;

        public CommentsController(
            ILogger<CommentsController> logger,
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
        }

        [HttpGet]
        public IActionResult Create() {
            GetUser();

            CommentViewModel comment = new CommentViewModel();
            comment.practitionerBar = practitionerBar;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CommentViewModel model) {

            if (ModelState.IsValid)
            {
                GetUser();

                Comment c = new Comment();
                c.practitionerId = appUser.practitionerId;
                c.patientFileId = (int)TempData["PatientFileId"];
                c.postDate = DateTime.Now;

                c.visible = model.comment.visible;
                c.content = model.comment.content;

                commentRepo.AddComment(c);

                return RedirectToAction("Details", "PatientFiles", new { id = (int)TempData["PatientFileId"] });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id) {
            GetUser();
            CommentViewModel vm = new CommentViewModel();

            vm.comment = commentRepo.FindCommentById(id);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            commentRepo.DeleteComment(id);
            return RedirectToAction("Details", "PatientFiles", new { id = (int)TempData["PatientFileId"] });
            TempData.Keep();
        }

        private void GetUser()
        {
            // Finding an account by email
            string mail = User.Identity.Name;
            appUser = appAccRepo.FindAccountByMail(mail);

            // Setting the practitioner bar values
            if (appUser.accountType == AccountType.PRACTITIONER) {
                practitionerBar = new PractitionerBar();
                practitionerBar.amountOfAppointments = treatmentRepo.GetTodaysTreatmentsCount(appUser.practitionerId);
                practitionerBar.isPractitioner = true;
            }
        }
    }
}
