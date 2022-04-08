using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain
{
    // Mail is unique in the system. Use as an identifier.
    // Once intake happened, patients can register by making an account using the mail they have given the intaking practitioner.
    public class AppAccount
    {
        // Identifiers
        [Key]
        public int accountId { get; set; }

        // Login
        // Mail is system-wide unique.
        [Required]
        public string mail { get; set; }

        public AccountType accountType { get; set; }
        public int patientId { get; set; }
        public int practitionerId { get; set; }

        public AppAccount() {

        }
    }

    public enum AccountType { 
        PRACTITIONER,
        PATIENT
    }
}
