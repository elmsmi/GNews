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
    public class NewsController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: News
        public ActionResult Index()
        {
            NewViewModel model = new NewViewModel();
            PopulateDropDownWithClients(model);
            PopulateDropDownWithEmployees(model);
            PopulateDropDownWithDates(model);
            model.ListOfNews = db.News.Include(x => x.Client).OrderByDescending(x => x.Date).ToList();
            return View(model);
        }

        // GET: News/Create
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

        private void PopulateDropDownWithClients(NewViewModel model)
        {
            var ClientQuery = from d in db.Clients orderby d.ClientName select d;
            model.ListOfClients = new SelectList(ClientQuery, "ClientID", "ClientName");
        }
        private void PopulateDropDownWithEmployees(NewViewModel model)
        {
            var EmployeeQuery = from d in db.Employees orderby d.EmployeeName select d;
            model.ListOfEmployees = new SelectList(EmployeeQuery, "EmployeeID", "EmployeeName");
        }

        private void PopulateDropDownWithDates(NewViewModel model)
        {
            var DatesQuery = from d in db.News orderby d.Date select d;
            model.ListOfDates = new SelectList(DatesQuery, "Date", "Date");
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
