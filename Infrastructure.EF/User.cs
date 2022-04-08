using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF
{
    public class User : IdentityUser
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(3)]
        public string password { get; set; }

        public User(string mail, string password)
        {
            this.mail = mail;
            this.password = password;
        }

        public User() {
        
        }
    }
}
