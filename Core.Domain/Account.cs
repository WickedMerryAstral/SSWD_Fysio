using System;

namespace Core.Domain
{
    // Mail is unique in the system. Use as an identifier.
    // Once intake happened, patients can register by making an account using the mail they have given the intaking practitioner.
    public class Account
    {
        // Identifiers
        public string accountId { get; set; }
        public string practitionerId { get; set; }
        public string patientId { get; set; }

        // Login
        // Mail is system-wide unique.
        public string mail { get; set; }
        public string password { get; set; }
    }
}
