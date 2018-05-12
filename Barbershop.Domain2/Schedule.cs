namespace Barbershop.Domain2
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public int BarberId { get; set; }

        [Display(Name = "Date time")]
        [Required(ErrorMessage = "The field {0} is requiered.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Hour Start")]
        [Required(ErrorMessage = "The field {0} is requiered.")]
        [DataType(DataType.Time)]
        public DateTime HourStart { get; set; }

        [Display(Name = "Hour End")]
        [Required(ErrorMessage = "The field {0} is requiered.")]
        [DataType(DataType.Time)]
        public DateTime HourEnd { get; set; }

        [JsonIgnore]
        public virtual Barber Barber { get; set; }

    }
}
