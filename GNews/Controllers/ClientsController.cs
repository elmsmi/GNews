using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GNews.Models;
using GNews.ViewModels;

namespace GNews.Controllers
{
    public class ClientsController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Where(x => x.ClientID == id).Include(x => x.Employees).FirstOrDefault();
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ClientViewModel model = new ClientViewModel();
            PopulateDropDownWithEmployees(model);
            return View(model);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(model.client);
                if (model.SelectedEmployees != null)
                {
                    foreach (var x in model.SelectedEmployees)
                    {
                        Employee s = new Employee { EmployeeID = x };
                        db.Employees.Add(s);
                        db.Employees.Attach(s);
                        model.client.Employees.Add(s);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model.client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientViewModel model = new ClientViewModel();
            model.client = db.Clients.Find(id);
            if (model.client == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownWithEmployees(model);
            return View(model);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.client).State = EntityState.Modified;
                if (model.SelectedEmployees != null)
                {
                    foreach (var x in model.SelectedEmployees)
                    {
                        Employee s = new Employee { EmployeeID = x };
                        db.Employees.Add(s);
                        db.Employees.Attach(s);
                        model.client.Employees.Add(s);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateDropDownWithEmployees(model);
            return View(model.client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult deleteEmployeeFromClient(int? clientId, int? employeeId)
        {
            if (clientId != null && employeeId != null)
            {
                var employee = db.Employees.Find(employeeId);
                var client = db.Clients.Find(clientId);
                client.Employees.Remove(employee);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            return RedirectToAction("ServerError", "Error");
        }

        private void PopulateDropDownWithEmployees(ClientViewModel model)
        {
            var EmployeeQuery = from d in db.Employees orderby d.EmployeeName select d;
            model.ListOfEmployees = new MultiSelectList(EmployeeQuery, "EmployeeID", "EmployeeName");
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
