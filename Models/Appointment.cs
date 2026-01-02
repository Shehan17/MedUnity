using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedUnity.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }


        public Patient? Patient { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string DoctorSpecialty { get; set; } = null!;

        private string? _status;
        public string Status
        {
            get => string.IsNullOrWhiteSpace(_status) ? "Pending" : _status;
            set => _status = value;
        }

        public string? RejectedReason { get; set; }
    }
}
