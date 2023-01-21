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

    public class OrdineController : Controller
    {

        private ModelDBcontext db = new ModelDBcontext();

        // GET: Ordine
        public ActionResult Index()
        {

            var oRDINE = db.ORDINE.Include(o => o.USER);
            return View(oRDINE.ToList());
        }

        public ActionResult OrdineConfermato()
        {
            
                USER u = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
                List<ORDINE> oRDINEs = db.ORDINE.Where(x => x.IdUser == u.IdUser).ToList();
                return View(oRDINEs);
            
        }

        // GET: Ordine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            return View(oRDINE);
        }

        // GET: Ordine/Create
        public ActionResult Create()
        {
            ORDINE o = new ORDINE();
            USER u = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<DETTAGLIO> d = db.DETTAGLIO.Where(x => x.IdOrdine == null && x.IdUser == u.IdUser).ToList();
            o.TotaleImporto = d.Sum(x => x.PrezzoTotale);         
            o.IdUser = u.IdUser;
            o.DETTAGLIO = d;
            o.USER = u;


            return View(o);
            
        }

        // POST: Ordine/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ORDINE o)
        {

            o.Confermato = "Si";
            o.Evaso = "No";
            db.ORDINE.Add(o);
            db.SaveChanges();
            return RedirectToAction("OrdineConfermato");
                    
        }

        // GET: Ordine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Username", oRDINE.IdUser);
            return View(oRDINE);
        }

        // POST: Ordine/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrdne,Note,Confermato,Importo,TotaleImporto,Evaso,IdUser")] ORDINE oRDINE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDINE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Username", oRDINE.IdUser);
            return View(oRDINE);
        }

        // GET: Ordine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            return View(oRDINE);
        }

        // POST: Ordine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDINE oRDINE = db.ORDINE.Find(id);
            db.ORDINE.Remove(oRDINE);
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
