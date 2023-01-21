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
    [Authorize]
    public class DettaglioController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: Dettaglio
        public ActionResult Index(int id, int quantity)
        {
                USER utente = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            if (id > 0)
            {
                DETTAGLIO d = new DETTAGLIO();
                d.IdPizza = id;
                d.Quantita = quantity;
                PIZZA p = db.PIZZA.Find(id);
                d.PrezzoTotale = p.Prezzo * d.Quantita;
                d.IdUser = utente.IdUser;
                

                db.DETTAGLIO.Add(d);
                db.SaveChanges();
            }

            return View(db.DETTAGLIO.Where(x => x.IdOrdine == null && x.IdUser == utente.IdUser).ToList());
        }

        public ActionResult Carrello()
        {
            USER utente = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<DETTAGLIO> dtg = db.DETTAGLIO.Include(x =>x.PIZZA).Where(d => d.IdOrdine == null && d.IdUser == utente.IdUser).ToList();
            return View(dtg);
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
            DETTAGLIO d = db.DETTAGLIO.Find(id);
           
           
            return View(d);
        }

        // POST: Dettaglio/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DETTAGLIO d)
        {
            if (ModelState.IsValid)
            {
                USER utente = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
                DETTAGLIO dettaglio = db.DETTAGLIO.Find(d.IdDettaglio);
                d.PrezzoTotale = dettaglio.PIZZA.Prezzo * d.Quantita;
                d.IdUser = utente.IdUser;
                ModelDBcontext db1 = new ModelDBcontext();
                db1.Entry(d).State = EntityState.Modified;
                db1.SaveChanges();
            }
          
            return RedirectToAction("Carrello");
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
            return RedirectToAction("Carrello");
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
