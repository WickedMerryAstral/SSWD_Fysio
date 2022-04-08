using Core.Domain;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Drawing;

namespace SSWD_Fysio.Models
{
    public class PatientFileViewModel
    {
        public PatientFile patientFile { get; set; }
        public Patient patient { get; set; }
        public TreatmentPlan treatmentPlan { get; set; }
        public List<Treatment> treatments { get; set; }
        public List<Comment> comments { get; set; }
        public PractitionerBar practitionerBar { get; set; }

        public List<string> sexOptions { get; set; }
        public List<string> patientTypeOptions { get; set; }
        public List<string> diagnosisOptions { get; set; }

        public string type { get; set; }
        public string number { get; set; }
        public string sex { get; set; }
        public string diagnosis { get; set; }
        public IFormFile photo { get; set; }
        public string imgSource { get; set; }

        public PatientFileViewModel() {
        
        
        }

        public PatientFileViewModel(PatientFile patientFile) {

            // Options for Select List consistency
            PopulateOptions();

            this.patientFile = patientFile;
            this.patient = patientFile.patient;
            this.treatmentPlan = patientFile.treatmentPlan;
            this.treatments = patientFile.treatmentPlan.treatments;

            // Empty list error prevention
            if (patientFile.comments != null)
            {
                comments = patientFile.comments;
            }
            else {
                comments = new List<Comment>();
            }

            practitionerBar = new PractitionerBar();

            // Type
            switch (patient.type) {
                case PatientType.STUDENT:
                    type = "Student";
                    number = patient.studentNumber;
                    break;

                case PatientType.EMPLOYEE:
                    type = "Employee";
                    number = patient.employeeNumber;
                    break;
            }

            // Sex
            switch (patient.sex)
            {
                case Sex.MALE:
                    sex = "Male";
                    break;

                case Sex.FEMALE:
                    sex = "Female";
                    break;

                case Sex.OTHER:
                    sex = "Other";
                    break;
            }

        }

        public void PopulateOptions() {
            sexOptions = new List<string>();
            patientTypeOptions = new List<string>();

            sexOptions.Add("Male");
            sexOptions.Add("Female");
            sexOptions.Add("Other");

            patientTypeOptions.Add("Student");
            patientTypeOptions.Add("Employee");
        }
    }
}
