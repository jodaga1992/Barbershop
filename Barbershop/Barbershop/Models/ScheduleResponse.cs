namespace Barbershop.Models
{
    using Newtonsoft.Json;
    using System;

    public class ScheduleResponse
    {
        [JsonProperty("ScheduleId")]
        public int ScheduleId { get; set; }

        [JsonProperty("BarberId")]
        public int BarberId { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("HourStart")]
        public DateTime HourStart { get; set; }

        [JsonProperty("HourEnd")]
        public DateTime HourEnd { get; set; }

        [JsonProperty("Barber")]
        public BarberResponse Barber { get; set; }
    }
}
//Offset