namespace Barbershop.Backend.Controllers
{
    using Backend.Models;
    using Backend.Helpers;
    using Domain2;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Authorize(Roles = "Admin")]
    public class BarbersController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public async Task<ActionResult> IndexAppointments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schedule = await db.Schedules.FindAsync(id);

            if (schedule == null)
            {
                return HttpNotFound();
            }

            var appointments = await this.GetAppointments(schedule);
            return View(appointments);
        }

        public async Task<List<Appointment>> GetAppointments(Schedule schedule)
        {
            var appointments = new List<Appointment>();
            var barber = await db.Barbers.FindAsync(schedule.BarberId);

            foreach (var item in barber.Appointment)
            {
                var appointment = await db.Appointments.FindAsync(item.AppointmentId);
                if (appointment != null && appointment.Date == schedule.Date)
                {
                    appointment.Hour = appointment.Hour.ToLocalTime();
                    appointments.Add(appointment);
                }
            }

            return appointments;
        }

        public async Task<ActionResult> EditSchedule (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schedule = await db.Schedules.FindAsync(id);

            if (schedule == null)
            {
                return HttpNotFound();
            }
            
            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSchedule(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.HourEnd = schedule.HourEnd.ToUniversalTime();
                schedule.HourStart = schedule.HourStart.ToUniversalTime();
                db.Entry(schedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", schedule.BarberId));
            }
            return View(schedule);
        }

        public async Task<ActionResult> AddSchedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var barber = await db.Barbers.FindAsync(id);

            if (barber == null)
            {
                return HttpNotFound();
            }

            var schedule = new Schedule
            {
                BarberId = barber.BarberId
            };

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSchedule(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.HourEnd = schedule.HourEnd.ToUniversalTime();
                schedule.HourStart = schedule.HourStart.ToUniversalTime();
                db.Schedules.Add(schedule);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", schedule.BarberId));
            }
            
            return View(schedule);
        }

        // GET: Barbers
        public async Task<ActionResult> Index()
        {
            return View(await db.Barbers.ToListAsync());
        }

        // GET: Barbers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var barber = await db.Barbers.FindAsync(id);

            if (barber == null)
            {
                return HttpNotFound();
            }

            var schedules = await this.GetSchedules(barber);
            return View(schedules);
        }

        public async Task<List<Schedule>> GetSchedules(Barber barber)
        {
            var schedules = new List<Schedule>();
            foreach (var item in barber.Schedule)
            {
                var schedule = await db.Schedules.FindAsync(item.ScheduleId);
                if (schedule != null)
                {
                    schedule.HourEnd = schedule.HourEnd.ToLocalTime();
                    schedule.HourStart = schedule.HourStart.ToLocalTime();
                    schedules.Add(schedule);
                }
            }

            return schedules;
        }

        // GET: Barbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Barbers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BarberView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Barbers";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var barber = this.ToBarber(view);
                barber.ImagePath = pic;
                db.Barbers.Add(barber);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Barber ToBarber(BarberView view)
        {
            return new Barber
            {
                ImagePath= view.ImagePath,
                FirstName = view.FirstName,
                LastName = view.LastName,
                BarberId = view.BarberId,
                Email = view.Email,
                Telephone= view.Telephone,
                Password= view.Password,
            };
        }

        // GET: Barbers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var barber = await db.Barbers.FindAsync(id);

            if (barber == null)
            {
                return HttpNotFound();
            }

            var view = this.ToBarberView(barber);
            return View(view);
        }

        private BarberView ToBarberView(Barber barber)
        {
            return new BarberView
            {
                ImagePath = barber.ImagePath,
                FirstName = barber.FirstName,
                LastName = barber.LastName,
                BarberId = barber.BarberId,
                Email = barber.Email,
                Telephone = barber.Telephone,
                Password = barber.Password
            };
        }

        // POST: Barbers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BarberView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Barbers";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var barber = this.ToBarber(view);
                barber.ImagePath = pic;
                db.Entry(barber).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Barbers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barber barber = await db.Barbers.FindAsync(id);
            if (barber == null)
            {
                return HttpNotFound();
            }
            return View(barber);
        }

        // POST: Barbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Barber barber = await db.Barbers.FindAsync(id);
            db.Barbers.Remove(barber);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
