using Core.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace SSWD_Fysio.Models
{
    public class TreatmentViewModel
    {
        public int chosenPractitioner { get; set; }
        public Practitioner currentPractitioner { get; set; }
        public string patientName { get; set; }
        public Treatment treatment { get; set; }
        public PatientFile patientFile { get; set; }
        public PractitionerBar practitionerBar { get; set; }

        [Required(ErrorMessage = "Please enter a date")]
        [FutureValidation]
        public DateTime treatmentDate { get; set; }
        public string chosenCode { get; set; }

        public TreatmentViewModel() {
            treatment = new Treatment();
            practitionerBar = new PractitionerBar();
        }

        public class FutureValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime dt = (DateTime)value;
                if (dt < DateTime.Now)
                {
                    return new ValidationResult("Date cannot be before today.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}
