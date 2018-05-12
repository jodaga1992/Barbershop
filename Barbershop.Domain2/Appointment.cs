namespace Barbershop.Domain2
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public int UserId { get; set; }

        public int BarberId { get; set; }

        public int StatusAppointmentId { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is requiered.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Hour")]
        [Required(ErrorMessage = "The field {0} is requiered.")]
        [DataType(DataType.Time)]
        public DateTime Hour { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual Barber Barber { get; set; }

        [JsonIgnore]
        public virtual StatusAppointment StatusAppointment { get; set; }
    }
}
