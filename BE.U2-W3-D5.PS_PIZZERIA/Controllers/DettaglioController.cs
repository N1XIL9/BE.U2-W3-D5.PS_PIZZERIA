using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BE.U2_W3_D5.PS_PIZZERIA.Models;

namespace BE.U2_W3_D5.PS_PIZZERIA.Controllers
{
    public class DettaglioController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: Dettaglio
        public ActionResult Index()
        {
            var dETTAGLIO = db.DETTAGLIO.Include(d => d.ORDINE).Include(d => d.PIZZA);
            return View(dETTAGLIO.ToList());
        }

        // GET: Dettaglio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETTAGLIO dETTAGLIO = db.DETTAGLIO.Find(id);
            if (dETTAGLIO == null)
            {
                return HttpNotFound();
            }
            return View(dETTAGLIO);
        }

        // GET: Dettaglio/Create
        public ActionResult Create()
        {
            ViewBag.IdOrdine = new SelectList(db.ORDINE, "IdOrdne", "Note");
            ViewBag.IdPizza = new SelectList(db.PIZZA, "IdPizza", "NomePizza");
            return View();
        }

        // POST: Dettaglio/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDettaglio,Quantita,IdPizza,IdOrdine")] DETTAGLIO dETTAGLIO)
        {
            if (ModelState.IsValid)
            {
                db.DETTAGLIO.Add(dETTAGLIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrdine = new SelectList(db.ORDINE, "IdOrdne", "Note", dETTAGLIO.IdOrdine);
            ViewBag.IdPizza = new SelectList(db.PIZZA, "IdPizza", "NomePizza", dETTAGLIO.IdPizza);
            return View(dETTAGLIO);
        }

        // GET: Dettaglio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETTAGLIO dETTAGLIO = db.DETTAGLIO.Find(id);
            if (dETTAGLIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOrdine = new SelectList(db.ORDINE, "IdOrdne", "Note", dETTAGLIO.IdOrdine);
            ViewBag.IdPizza = new SelectList(db.PIZZA, "IdPizza", "NomePizza", dETTAGLIO.IdPizza);
            return View(dETTAGLIO);
        }

        // POST: Dettaglio/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDettaglio,Quantita,IdPizza,IdOrdine")] DETTAGLIO dETTAGLIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dETTAGLIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOrdine = new SelectList(db.ORDINE, "IdOrdne", "Note", dETTAGLIO.IdOrdine);
            ViewBag.IdPizza = new SelectList(db.PIZZA, "IdPizza", "NomePizza", dETTAGLIO.IdPizza);
            return View(dETTAGLIO);
        }

        // GET: Dettaglio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETTAGLIO dETTAGLIO = db.DETTAGLIO.Find(id);
            if (dETTAGLIO == null)
            {
                return HttpNotFound();
            }
            return View(dETTAGLIO);
        }

        // POST: Dettaglio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DETTAGLIO dETTAGLIO = db.DETTAGLIO.Find(id);
            db.DETTAGLIO.Remove(dETTAGLIO);
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
