namespace Barbershop.Backend.Controllers
{
    using Barbershop.Backend.Models;
    using Barbershop.Domain2;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class AppointmentsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Appointments
        public async Task<ActionResult> Index()
        {
            var appointments = db.Appointments.Include(a => a.Barber).Include(a => a.StatusAppointment).Include(a => a.User);
            return View(await appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.BarberId = new SelectList(db.Barbers, "BarberId", "FirstName");
            ViewBag.StatusAppointmentId = new SelectList(db.StatusAppointments, "StatusAppointmentId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BarberId = new SelectList(db.Barbers, "BarberId", "FirstName", appointment.BarberId);
            ViewBag.StatusAppointmentId = new SelectList(db.StatusAppointments, "StatusAppointmentId", "Name", appointment.StatusAppointmentId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BarberId = new SelectList(db.Barbers, "BarberId", "FirstName", appointment.BarberId);
            ViewBag.StatusAppointmentId = new SelectList(db.StatusAppointments, "StatusAppointmentId", "Name", appointment.StatusAppointmentId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AppointmentId,UserId,BarberId,StatusAppointmentId,Date,Hour")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BarberId = new SelectList(db.Barbers, "BarberId", "FirstName", appointment.BarberId);
            ViewBag.StatusAppointmentId = new SelectList(db.StatusAppointments, "StatusAppointmentId", "Name", appointment.StatusAppointmentId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Appointment appointment = await db.Appointments.FindAsync(id);
            db.Appointments.Remove(appointment);
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
