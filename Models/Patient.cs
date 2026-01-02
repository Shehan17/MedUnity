using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MedUnity.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; } 

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public required string PasswordHash { get; set; } 

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; } 

        [MaxLength(500)]
        public string? SpecialNote { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
