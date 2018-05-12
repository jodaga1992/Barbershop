namespace barbershop.API.Models
{
    using Barbershop.Domain2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ScheduleResponse
    {
        public int ScheduleId { get; set; }

        public int BarberId { get; set; }
        
        public DateTime Date { get; set; }
        
        public DateTime HourStart { get; set; }
        
        public DateTime HourEnd { get; set; }
        
        public Barber Barber { get; set; }
    }
}