using System;

namespace Core.Domain
{
    // Mail is unique in the system. Use as an identifier.
    // Once intake happened, patients can register by making an account using the mail they have given the intaking practitioner.
    public class Account
    {
        // Identifiers
        public string accountId;
        public string practitionerId;
        public string patientId;
        
        // Login
        public string mail;
        public string password;
    }
}
