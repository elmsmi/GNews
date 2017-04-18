using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GNews.Models;
using GNews.ViewModels;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace GNews.Controllers
{
    public class NewsController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: News
        public ActionResult Index(NewViewModel model)
        {


            if (model != null)
            {
                PopulateDropDownWithClients(model);
                PopulateDropDownWithEmployees(model);
                PopulateDropDownWithDates(model);

                int? empID = model.SelectedEmployee;
                int? cliID = model.SelectedClient;
                string date = model.fecha;

                if (model.SelectedEmployee != null)
                {

                    Employee employee = db.Employees.Where(x => x.EmployeeID == empID).Include(x => x.Clients).FirstOrDefault();
                    var lis = db.Clients.Include(x => x.Employees);

                    var query = from x in db.Clients
                                where x.Employees.Any(c => c.EmployeeID == empID)
                                select x;
                    model.ListOfNews = (from x in db.News
                                        join y in query on x.Client.ClientID equals y.ClientID
                                        where x.Client.ClientID == cliID || cliID == null
                                        where x.Date.ToString("dd/MM/yy HH:mm:ss") == date || date == null
                                        select x).OrderByDescending(x => x.Date).ToList();
                    return View(model);

                }

                model.ListOfNews = (from x in db.News
                                    where x.Client.ClientID == cliID || cliID == null
                                    where x.Date.ToString() == date || date == null
                                    select x).OrderByDescending(x => x.Date).ToList();

                return View(model);

            }
            else
            {
                PopulateDropDownWithClients(model);
                PopulateDropDownWithEmployees(model);
                PopulateDropDownWithDates(model);
                model.ListOfNews = db.News.Include(x => x.Client).OrderByDescending(x => x.Date).ToList();
                return View(model);
            }

        }

        // GET: News/Create
        [Authorize]
        public ActionResult Create()
        {
            NewViewModel model = new NewViewModel();
            PopulateDropDownWithClients(model);
            return View(model);
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(NewViewModel model)
        {
            if (ModelState.IsValid)
            {
                Client c = db.Clients.Find(model.SelectedClient);
                db.Clients.Add(c);
                db.Clients.Attach(c);
                model.New.Client = c;
                db.News.Add(model.New);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateDropDownWithClients(model);
            return View(model.New);
        }

        // GET: News/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "NewID,NewText,Date")] New @new)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@new).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@new);
        }

        // GET: News/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            New @new = db.News.Find(id);
            db.News.Remove(@new);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportToExcel()
        {
            return View(db.News.ToList());
        }
        [NonAction]
        private void PopulateDropDownWithClients(NewViewModel model)
        {
            var ClientQuery = from d in db.Clients orderby d.ClientName select d;
            model.ListOfClients = new SelectList(ClientQuery, "ClientID", "ClientName");
        }
        [NonAction]
        private void PopulateDropDownWithEmployees(NewViewModel model)
        {
            var EmployeeQuery = from d in db.Employees orderby d.EmployeeName select d;
            model.ListOfEmployees = new SelectList(EmployeeQuery, "EmployeeID", "EmployeeName");
        }
        [NonAction]
        private void PopulateDropDownWithDates(NewViewModel model)
        {
            var DatesQuery = (from d in db.News orderby d.Date select d.Date).Distinct();
            model.ListOfDates = new SelectList(DatesQuery, "Date", "Date");
        }
        //.ToString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)
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
