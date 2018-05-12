namespace barbershop.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Barbershop.Domain2;

    public class AppointmentResponse
    {
        public int AppointmentId { get; set; }

        public int UserId { get; set; }

        public int BarberId { get; set; }

        public int StatusAppointmentId { get; set; }
        
        public DateTime Date { get; set; }
        
        public DateTime Hour { get; set; }
        
        public User User { get; set; }
        
        public Barber Barber { get; set; }
        
        public StatusAppointment StatusAppointment { get; set; }
    }
}