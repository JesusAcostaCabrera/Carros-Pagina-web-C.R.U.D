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
    public class CarroesController : Controller
    {
        private CarrosEntities db = new CarrosEntities();

        // GET: Carroes
        public ActionResult Index()
        {
            var carros = db.Carros.Include(c => c.Compañia).Include(c => c.Modelo).OrderByDescending(x => x.fechacreada);
            return View(carros.ToList());
        }

        public ActionResult Lista()
        {
            var carros = db.Carros.Include(c => c.Compañia).Include(c => c.Modelo).OrderByDescending(x => x.fechacreada);
            return View(carros.ToList());
        }

        // GET: Carroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carros.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        // GET: Carroes/Create
        public ActionResult Create()
        {
            ViewBag.idCompañia = new SelectList(db.Compañia, "id", "compañia1");
            ViewBag.idModelos = new SelectList(db.Modeloes, "id", "nombre");
            return View(new Carro());
        }

        // POST: Carroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipodeCarro,idModelos,idCompañia,precio,traccion,transmision,Imagen,Cantidad")] Carro carro, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                carro.fechacreada = DateTime.Now;
                if(imagen != null)
                {
                    string UrlImagen = System.IO.Path.GetFileName(imagen.FileName);
                    string UrlPath = System.IO.Path.Combine(Server.MapPath("/Public/Carros/"), UrlImagen);

                    imagen.SaveAs(UrlPath);

                    carro.Imagen = UrlImagen;
                }

                db.Carros.Add(carro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompañia = new SelectList(db.Compañia, "id", "nombre", carro.idCompañia);
            ViewBag.idModelos = new SelectList(db.Modeloes, "id", "nombre", carro.idModelos);
            return View(carro);
        }

        // GET: Carroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carros.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompañia = new SelectList(db.Compañia, "id", "nombre", carro.idCompañia);
            ViewBag.idModelos = new SelectList(db.Modeloes, "id", "nombre", carro.idModelos);
            return View(carro);
        }

        // POST: Carroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipodeCarro,idModelos,idCompañia,precio,traccion,transmision,Imagen,Cantidad")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                carro.fechacreada = DateTime.Now;
                db.Entry(carro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompañia = new SelectList(db.Compañia, "id", "nombre", carro.idCompañia);
            ViewBag.idModelos = new SelectList(db.Modeloes, "id", "nombre", carro.idModelos);
            return View(carro);
        }

        // GET: Carroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carros.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        // POST: Carroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carro carro = db.Carros.Find(id);
            db.Carros.Remove(carro);
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
