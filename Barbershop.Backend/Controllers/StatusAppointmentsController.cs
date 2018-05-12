namespace Barbershop.Backend.Controllers
{
    using Backend.Models;
    using Domain2;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class StatusAppointmentsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: StatusAppointments
        public async Task<ActionResult> Index()
        {
            return View(await db.StatusAppointments.ToListAsync());
        }

        // GET: StatusAppointments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAppointment statusAppointment = await db.StatusAppointments.FindAsync(id);
            if (statusAppointment == null)
            {
                return HttpNotFound();
            }
            return View(statusAppointment);
        }

        // GET: StatusAppointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusAppointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StatusAppointmentId,Name")] StatusAppointment statusAppointment)
        {
            if (ModelState.IsValid)
            {
                db.StatusAppointments.Add(statusAppointment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(statusAppointment);
        }

        // GET: StatusAppointments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAppointment statusAppointment = await db.StatusAppointments.FindAsync(id);
            if (statusAppointment == null)
            {
                return HttpNotFound();
            }
            return View(statusAppointment);
        }

        // POST: StatusAppointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StatusAppointmentId,Name")] StatusAppointment statusAppointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusAppointment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statusAppointment);
        }

        // GET: StatusAppointments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAppointment statusAppointment = await db.StatusAppointments.FindAsync(id);
            if (statusAppointment == null)
            {
                return HttpNotFound();
            }
            return View(statusAppointment);
        }

        // POST: StatusAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StatusAppointment statusAppointment = await db.StatusAppointments.FindAsync(id);
            db.StatusAppointments.Remove(statusAppointment);
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
