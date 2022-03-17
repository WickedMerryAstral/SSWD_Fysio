using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Patient
    {
        // Identifiers
        [Key]
        public int patientId { get; set; }
        public int patientFileId { get; set; }

        // Info
        public string studentNumber { get; set; }
        public string employeeNumber { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string photoURL { get; set; }
        public int age { get; set; }
        public Sex sex { get; set; }

        // Might not use this. Check again later.
        public PatientType type { get; set; }

        public Patient(string studentNumber, string employeeNumber, string name, string mail, string phone, string photoURL, DateTime birthDate, Sex sex, PatientType type)
        {
            this.studentNumber = studentNumber;
            this.employeeNumber = employeeNumber;
            this.name = name;
            this.mail = mail;
            this.phone = phone;
            this.photoURL = photoURL;
            this.age = CalculateAge(birthDate);
            this.sex = sex;
            this.type = type;
        }

        public Patient()
        {

        }

        private int CalculateAge(DateTime birthDate) {
            return DateTime.Now.Year - birthDate.Year;
        }
    }



    // Patients can be either students or employees. Fill Student -or EmployeeNumber respectively.
    public enum PatientType {
        STUDENT,
        TEACHER
    } 

    public enum Sex {
        MALE,
        FEMALE,
        OTHER
    }
}
