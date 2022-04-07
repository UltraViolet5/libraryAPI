using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        [Required]
        [MaxLength(256)]
        public string Authors { get; set; }
        [MaxLength(32)]
        public string BarcodeNumber { get; set; }
        [Required]
        public DateTime PublishingYear { get; set; }
        [Required] public DateTime AddingDate { get; set; }
        [NotMapped]
        public User Owner { get; set; }
        public string OwnerId { get; set; }
        [Required]
        public bool Read { get; set; }
        [Required]
        public Category Category { get; set; }
        public int Votes { get; set; }
        public int BookcaseId { get; set; }
        public bool Available { get; set; }
        public byte[] Photo { get; set; }
    }
}