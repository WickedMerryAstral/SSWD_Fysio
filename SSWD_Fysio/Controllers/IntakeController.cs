﻿using Core.DomainServices;
using SSWD_Fysio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core.Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace SSWD_Fysio.Controllers
{
    [AllowAnonymous]
    public class IntakeController : Controller
    {
        private List<VektisDiagnosis> diagnosis;
        private HttpClient client = new HttpClient();

        // Repositories
        private IPatientFileRepository fileRepo;
        private IPatientRepository patientRepo;
        private IPractitionerRepository practitionerRepo;
        private ITreatmentPlanRepository planRepo;
        private ITreatmentRepository treatmentRepo;
        private IAppAccountRepository appAccRepo;

        private readonly ILogger<IntakeController> _logger;
        public IntakeController(
            ILogger<IntakeController> logger,
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

        [HttpGet]
        public IActionResult Index()
        {
            IntakeViewModel model = new IntakeViewModel();

            // Codes for debugging
            PopulateDiagnosis();

            // Preparing View Data for Intake viewmodel
            ViewData["SexOptions"] = new SelectList(model.sexOptions);
            ViewData["PatientTypeOptions"] = new SelectList(model.patientTypes);

            ViewData["Practitioners"] = new SelectList(practitionerRepo.getAllPractitioners(), "practitionerId", "name");
            ViewData["Supervisors"] = new SelectList(practitionerRepo.getAllSupervisors(), "practitionerId", "name");

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(IntakeViewModel model)
        {
            PatientFile file = new PatientFile();

            // Treatment info
            file.treatmentPlan.complaint = model.mainComplaint;
            file.treatmentPlan.weeklySessions = model.weeklySessions;
            file.treatmentPlan.sessionDuration = model.sessionDuration;
            file.treatmentPlan.diagnosis = model.chosenCode;
            file.dischargeDate = model.dischargeDate;

            // Set patient type. 
            // Teachers are employees.
            switch (model.chosenType)
            {
                case "Employee":
                    file.patient.type = PatientType.EMPLOYEE;
                    file.patient.employeeNumber = model.number;
                    break;
                case "Student":
                    file.patient.type = PatientType.STUDENT;
                    file.patient.studentNumber = model.number;
                    break;
            }

            // Setting patient sex
            switch (model.chosenSex)
            {
                case "Male":
                    file.patient.sex = Sex.MALE;
                    break;
                case "Female":
                    file.patient.sex = Sex.FEMALE;
                    break;
                case "Other":
                    file.patient.sex = Sex.OTHER;
                    break;
            }

            // Info
            file.patient.name = model.name;
            file.patient.mail = model.mail;
            file.patient.phone = model.phone;
            file.patient.age = file.patient.CalculateAge(model.birthDate);
            file.birthDate = model.birthDate;

            // Setting date to when the registration happens.
            file.registerDate = System.DateTime.Now;
            
            // Practitioners
            file.supervisedBypractitionerId = model.chosenSupervisor;
            file.intakeByPractitionerId = model.chosenIntaker;
            file.treatmentPlan.practitionerId = model.chosenMainPractitioner;
            file.comments = new List<Comment>();

            fileRepo.AddPatientFile(file);

            return RedirectToAction("Index", "PractitionerDashboard");
        }

        private void PopulateDiagnosis()
        {
            // Fill diagnosis
            try
            {
                ViewData["DiagnosisOptions"] = GetData().Result;
            }
            catch (Exception ex)
            {
                ViewData["DiagnosisOptions"] = new SelectList("0");
            }
        }

        private async Task<SelectList> GetData()
        {
            diagnosis = new List<VektisDiagnosis>();

            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/Diagnosis");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            diagnosis = JsonConvert.DeserializeObject<List<VektisDiagnosis>>(responseBody);
            diagnosis.Sort((x, y) => String.Compare(x.code, y.code));

            List<string> diagnosisOptions = new List<string>();
            foreach (VektisDiagnosis vk in diagnosis)
            {
                diagnosisOptions.Add(vk.code + ", " + vk.pathology);
            }

            SelectList output = new SelectList(diagnosisOptions);
            return output;
        }
    }
}
