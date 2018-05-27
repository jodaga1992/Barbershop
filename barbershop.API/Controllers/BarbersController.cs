namespace barbershop.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Barbershop.Domain2;
    using Models;

    public class BarbersController : ApiController
    {
        private DataContext db = new DataContext();

        [HttpGet]
        [Route("api/GetBarberSchedules")]
        public async Task<IHttpActionResult> GetBarberSchedules(int id)
        {
            var responses = new List<ScheduleResponse>();
            var schedules = await db.Schedules.ToListAsync();

            if (schedules == null)
            {
                return NotFound();
            }

            foreach (var schedule in schedules)
            {
                if (schedule.BarberId == id && schedule.Date >= DateTime.Today)
                {
                    responses.Add(new ScheduleResponse
                    {
                        BarberId = schedule.BarberId,
                        ScheduleId = schedule.ScheduleId,
                        Date = schedule.Date,
                        HourStart = schedule.HourStart.ToLocalTime(),
                        HourEnd = schedule.HourEnd.ToLocalTime(),
                        Barber = schedule.Barber,
                    });
                }
            }
            if (responses == null)
            {
                return NotFound();
            }

            return Ok(responses);
        }

        [HttpGet]
        [Route("api/GetBarberAvailability")]
        public async Task<IHttpActionResult> GetBarberAvailability(int id, DateTime date)
        {
            var responses = new List<AppointmentResponse>();
            var appointments = await db.Appointments.ToListAsync();

            if (appointments == null)
            {
                return NotFound();
            }

            foreach (var appointment in appointments)
            {
                if (appointment.BarberId == id && appointment.Date == date)
                {
                    responses.Add(new AppointmentResponse
                    {
                        BarberId = appointment.BarberId,
                        AppointmentId = appointment.AppointmentId,
                        UserId = appointment.UserId,
                        StatusAppointmentId = appointment.StatusAppointmentId,
                        Date = appointment.Date,
                        Hour = appointment.Hour,
                        User = appointment.User,
                        Barber = appointment.Barber,
                        StatusAppointment = appointment.StatusAppointment
                    });
                }
            }
            var schedules = await db.Schedules.ToListAsync();

            if (schedules == null)
            {
                return NotFound();
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            
            foreach (var schedule in schedules)
            {
                if (schedule.Date == date && schedule.BarberId== id)
                {
                    start = schedule.HourStart;
                    end = schedule.HourEnd;
                    break;
                }
            }

            if (start == end)
            {
                return NotFound();
            }

            var response = new List<AppointmentResponse>();

            while (start <= end)
            {
                bool add = false;
                foreach (var appointment in responses)
                {
                    if (appointment.Hour == start && appointment.StatusAppointmentId != 3)
                    {
                        add = true;
                        break;
                    }
                }
                if (!add)
                {
                    response.Add(new AppointmentResponse
                    {
                        BarberId = id,
                        Date = date,
                        Hour = start.ToLocalTime(),
                    });
                }
                start = start.AddMinutes(30);
            }

            return Ok(response);
        }

        // GET: api/Barbers
        public IQueryable<Barber> GetBarbers()
        {
            return db.Barbers;
        }

        // GET: api/Barbers/5
        [ResponseType(typeof(Barber))]
        public async Task<IHttpActionResult> GetBarber(int id)
        {
            Barber barber = await db.Barbers.FindAsync(id);
            if (barber == null)
            {
                return NotFound();
            }

            return Ok(barber);
        }

        // PUT: api/Barbers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBarber(int id, Barber barber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != barber.BarberId)
            {
                return BadRequest();
            }

            db.Entry(barber).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarberExists(id))
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

        // POST: api/Barbers
        [ResponseType(typeof(Barber))]
        public async Task<IHttpActionResult> PostBarber(Barber barber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Barbers.Add(barber);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = barber.BarberId }, barber);
        }

        // DELETE: api/Barbers/5
        [ResponseType(typeof(Barber))]
        public async Task<IHttpActionResult> DeleteBarber(int id)
        {
            Barber barber = await db.Barbers.FindAsync(id);
            if (barber == null)
            {
                return NotFound();
            }

            db.Barbers.Remove(barber);
            await db.SaveChangesAsync();

            return Ok(barber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BarberExists(int id)
        {
            return db.Barbers.Count(e => e.BarberId == id) > 0;
        }
    }
}