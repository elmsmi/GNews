using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GNews.Models;
using GNews.ViewModels;
using System;

namespace GNews.Controllers
{
    public class EmployeesController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: Employees
        [Authorize]
        public ActionResult Index(string search)
        {
            var employees = from x in db.Employees select x;

            if (!String.IsNullOrEmpty(search))
            {
                employees = employees.Where(s => s.EmployeeName.Contains(search));
                                      
            }
            return View(employees.OrderBy(s => s.EmployeeName));
        }

        // GET: Employees/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Where(x => x.EmployeeID == id).Include(x => x.Clients).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize]
        public ActionResult Create()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            PopulateDropDownWithClients(model);
            return View(model);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(EmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                db.Employees.Add(model.employee);
                if (model.SelectedClients != null)
                {
                    foreach (var x in model.SelectedClients)
                    {
                        Client s = new Client { ClientID = x };
                        db.Clients.Add(s);
                        db.Clients.Attach(s);
                        model.employee.Clients.Add(s);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model.employee);
        }

        // GET: Employees/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel model = new EmployeeViewModel();
            model.employee = db.Employees.Find(id);
            if (model.employee == null)
            {
                return HttpNotFound();
            }
            //PopulateDropDownWithClients(model);
            var ClientQuery = from d in db.Clients orderby d.ClientName select d;
            model.ListOfClients = new MultiSelectList(ClientQuery, "ClientID", "ClientName",null,null);
            return View(model);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.employee).State = EntityState.Modified;
                if (model.SelectedClients != null)
                {
                    foreach (var x in model.SelectedClients)
                    {
                        Client s = new Client { ClientID = x };
                        db.Clients.Add(s);
                        db.Clients.Attach(s);
                        model.employee.Clients.Add(s);
                    }
                }
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            PopulateDropDownWithClients(model);
            return View(model.employee);
        }

        // GET: Employees/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult deleteClientFromEmployee(int? clientId, int? employeeId)
        {
            if (clientId != null && employeeId != null)
            {
                var employee = db.Employees.Find(employeeId);
                var client = db.Clients.Find(clientId);
                employee.Clients.Remove(client);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            return RedirectToAction("ServerError", "Error");
        }

        [NonAction]
        private void PopulateDropDownWithClients(EmployeeViewModel model)
        {
            var ClientQuery = from d in db.Clients orderby d.ClientName select d;
            model.ListOfClients = new MultiSelectList(ClientQuery, "ClientID", "ClientName");
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
