using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Carros_Full;

namespace Carros_Full.Controllers
{
    public class CompañiaController : Controller
    {
        private CarrosEntities db = new CarrosEntities();

        // GET: Compañia
        public ActionResult Index()
        {
            return View(db.Compañia.ToList());
        }

        // GET: Compañia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compañia compañia = db.Compañia.Find(id);
            if (compañia == null)
            {
                return HttpNotFound();
            }
            return View(compañia);
        }

        // GET: Compañia/Create
        public ActionResult Create()
        {
            return View(new Compañia());
        }

        // POST: Compañia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,compañia1")] Compañia compañia)
        {
            if (ModelState.IsValid)
            {
                compañia.fechacreada = DateTime.Now;
                db.Compañia.Add(compañia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compañia);
        }

        // GET: Compañia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compañia compañia = db.Compañia.Find(id);
            if (compañia == null)
            {
                return HttpNotFound();
            }
            return View(compañia);
        }

        // POST: Compañia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombre,compañia1")] Compañia compañia)
        {
            if (ModelState.IsValid)
            {
                compañia.fechacreada = DateTime.Now;
                db.Entry(compañia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compañia);
        }

        // GET: Compañia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compañia compañia = db.Compañia.Find(id);
            if (compañia == null)
            {
                return HttpNotFound();
            }
            return View(compañia);
        }

        // POST: Compañia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compañia compañia = db.Compañia.Find(id);
            db.Compañia.Remove(compañia);
            db.SaveChanges();
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
