using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public string name { get; set; }

        [Required]
        public string mail { get; set; }
        public string phone { get; set; }
        public byte[] photo { get; set; }
        public int age { get; set; }
        public Sex sex { get; set; }



        // Might not use this. Check again later.
        public PatientType type { get; set; }

        public Patient(string studentNumber, string employeeNumber, string name, string mail, string phone, DateTime birthDate, Sex sex, PatientType type)
        {
            this.studentNumber = studentNumber;
            this.employeeNumber = employeeNumber;
            this.name = name;
            this.mail = mail;
            this.phone = phone;
            this.age = CalculateAge(birthDate);
            this.sex = sex;
            this.type = type;
        }

        public Patient()
        {

        }

        // Calculating age based on birthdate.
        public int CalculateAge(DateTime birthDate) {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day)) {
                age--;
            }
            return age;
        }

        public bool IsPatientOver16(DateTime birthDate) {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
            {
                age--;
            }
            if (age < 16) {
                return false;
            }
            return true;
        }
    }

    // Patients can be either students or employees. Fill Student -or EmployeeNumber respectively.
    public enum PatientType {
        STUDENT,
        EMPLOYEE
    } 

    public enum Sex {
        MALE,
        FEMALE,
        OTHER
    }
}
