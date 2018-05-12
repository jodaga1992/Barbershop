namespace barbershop.API.Controllers
{
    using Barbershop.Domain2;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class AppointmentsController : ApiController
    {
        private DataContext db = new DataContext();
        
        [HttpGet]
        [Route("api/GetAppointmentUser")]
        public async Task<IHttpActionResult> GetAppointmentUser(int id)
        {
            var responses = new List<AppointmentResponse>();
            var appointments = await db.Appointments.ToListAsync();

            foreach (var appointment in appointments)
            {
                if (appointment.UserId==id)
                {
                    responses.Add(new AppointmentResponse
                    {
                        BarberId = appointment.BarberId,
                        AppointmentId = appointment.AppointmentId,
                        UserId = appointment.UserId,
                        StatusAppointmentId = appointment.StatusAppointmentId,
                        Date = appointment.Date,
                        Hour = appointment.Hour.ToLocalTime(),
                        User = appointment.User,
                        Barber = appointment.Barber,
                        StatusAppointment = appointment.StatusAppointment
                    });
                }

            }

            return Ok(responses);
        }

        // GET: api/Appointments
        public IQueryable<Appointment> GetAppointments()
        {
            return db.Appointments;
        }

        // GET: api/Appointments/5
        [ResponseType(typeof(Appointment))]
        public async Task<IHttpActionResult> GetAppointment(int id)
        {
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // PUT: api/Appointments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.AppointmentId)
            {
                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Appointments
        [ResponseType(typeof(Appointment))]
        public async Task<IHttpActionResult> PostAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            appointment.Hour = appointment.Hour.ToUniversalTime();
            db.Appointments.Add(appointment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = appointment.AppointmentId }, appointment);
        }

        // DELETE: api/Appointments/5
        [ResponseType(typeof(Appointment))]
        public async Task<IHttpActionResult> DeleteAppointment(int id)
        {
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            await db.SaveChangesAsync();

            return Ok(appointment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.AppointmentId == id) > 0;
        }
    }
}