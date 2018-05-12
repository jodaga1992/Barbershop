namespace Barbershop.Domain2
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        #region Properties
        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<Barber> Barbers { get; set; }

        public DbSet<StatusAppointment> StatusAppointments { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
        #endregion
    }
}
