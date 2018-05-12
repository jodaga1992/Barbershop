namespace Barbershop.Backend.Models
{
    using Barbershop.Domain2;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Barbershop.Domain2.User> Users { get; set; }

        public System.Data.Entity.DbSet<Barbershop.Domain2.UserType> UserTypes { get; set; }

        public System.Data.Entity.DbSet<Barbershop.Domain2.Barber> Barbers { get; set; }

        public System.Data.Entity.DbSet<Barbershop.Domain2.StatusAppointment> StatusAppointments { get; set; }

        public System.Data.Entity.DbSet<Barbershop.Domain2.Appointment> Appointments { get; set; }
    }
}