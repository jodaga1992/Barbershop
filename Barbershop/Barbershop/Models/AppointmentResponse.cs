namespace Barbershop.Models
{
    using Newtonsoft.Json;
    using System;

    public class AppointmentResponse
    {
        [JsonProperty("AppointmentId")]
        public int AppointmentId { get; set; }

        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("BarberId")]
        public int BarberId { get; set; }

        [JsonProperty("StatusAppointmentId")]
        public int StatusAppointmentId { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Hour")]
        public DateTime Hour { get; set; }
    }
}
