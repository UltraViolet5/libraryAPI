using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(128)]
        public string Localization { get; set; }
        [Required]
        public bool TermsAccepted { get; set; }

        [NotMapped]
        public List<User> Friends { get; set; }
        public byte[] Photo { get; set; }
        public int BooksCount { get; set; }
    }
}
