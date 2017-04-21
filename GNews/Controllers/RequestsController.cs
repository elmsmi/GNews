using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GNews.Models;
using GNews.ViewModels;

namespace GNews.Controllers
{
    public class RequestsController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: Requests
        [Authorize]
        public ActionResult Index()
        {
            var requests = db.Requests.Include("Employee").ToList();
            return View(requests);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            RequestViewModel model = new RequestViewModel();
            PopulateDropDownWithEmployees(model);
            return View(model);
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                Employee s = new Employee { EmployeeID = model.SelectedEmployee };
                db.Employees.Add(s);
                db.Employees.Attach(s);
                model.request.Employee= s;
                db.Requests.Add(model.request);
                db.SaveChanges();
                return RedirectToAction("Index","News",null);
            }
            PopulateDropDownWithEmployees(model);
            return View(model);
        }

        // GET: Requests/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "RequestID,RequestText,Resolved")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: Requests/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            request.Employee = null;
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [NonAction]
        private void PopulateDropDownWithEmployees(RequestViewModel model)
        {
            var EmployeeQuery = (from d in db.Employees orderby d.EmployeeName select d).ToList();
            model.ListOfEmployees = new SelectList(EmployeeQuery, "EmployeeID", "EmployeeName");
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
