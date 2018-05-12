﻿namespace Barbershop.Domain2
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StatusAppointment
    {
        [Key]
        public int StatusAppointmentId { get; set; }

        [Required(ErrorMessage = "The field {0} is requiered.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contains a maximum of {1} characters lenght.")]
        [Index("StatusAppointment_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
