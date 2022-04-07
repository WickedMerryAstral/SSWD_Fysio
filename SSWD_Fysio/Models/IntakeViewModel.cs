using Core.Domain;
using Infrastructure.EF;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSWD_Fysio.Models
{
    public class IntakeViewModel
    {
        // Patient info:
        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }

        [Required]
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
        [Required]
        public string number { get; set; }

        // Set during registration.
        [Required]
        public DateTime registryDate { get; set; }

        [Required]
        public DateTime dischargeDate { get; set; }
        [Required]
        public DateTime entryDate { get; set; }

        // Treatment Plan
        // Duration in minutes.
        [Required]
        public string mainComplaint { get; set; }
        [Required]
        [Range(1, 100)]
        public int weeklySessions { get; set; }

        [Required]
        [Range(5, 300)]
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
        }
    }
}
