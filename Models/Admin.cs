    using System.ComponentModel.DataAnnotations;

    namespace MedUnity.Models
    {
        public class Admin
   
        {
            [Key]
            public int AdminId { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string PasswordHash { get; set; }
        }
    }
