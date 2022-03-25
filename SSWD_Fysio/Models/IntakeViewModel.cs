using Core.Domain;
using Infrastructure.EF;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace SSWD_Fysio.Models
{
    public class IntakeViewModel
    {
        // Patient info:
        public string name { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public DateTime birthDate { get; set; }

        // Selectors:
        public List<string> sexOptions { get; set; }
        public string chosenSex { get; set; }
        public List<string> patientTypes { get; set; }
        public string chosenType { get; set; }
        public List<string> diagnosisCodes { get; set; }
        public string chosenCode { get; set; }
        public List<Practitioner> availablePractitioners { get; set; }
        public List<Practitioner> availableSupervisors { get; set; }
        public int chosenSupervisor { get; set; }
        public int chosenIntaker { get; set; }
        public int chosenMainPractitioner { get; set; }

        // Student or Employee Number
        public string number { get; set; }

        // Set during registration.
        public DateTime registryDate { get; set; }
        public DateTime dischargeDate { get; set; }

        // Treatment Plan
        // Duration in minutes.
        public string mainComplaint { get; set; }
        public int weeklySessions { get; set; }
        public int sessionDuration { get; set; }

        public IntakeViewModel() {
            // Types of sex
            sexOptions = new List<string>();
            sexOptions.Add("Male");
            sexOptions.Add("Female");
            sexOptions.Add("Other");

            // Types of patients
            patientTypes = new List<string>();
            patientTypes.Add("Student");
            patientTypes.Add("Employee");

            // Fill diagnosis codes in from Vektis server later.
            // Call API.
            diagnosisCodes = new List<string>();

            // Add practitioners to list.
            // Filter out for supervisison list.
            // Only practitioners who are teachers may supervise.
        }
    }
}
