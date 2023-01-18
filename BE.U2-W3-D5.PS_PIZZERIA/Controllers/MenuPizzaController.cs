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
    public class MenuPizzaController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: MenuPizza
        public ActionResult Index()
        {
            return View(db.PIZZA.ToList());
        }


        [Authorize]
        public ActionResult ListaAdmin()
        {
            return View(db.PIZZA.ToList());
        }

        // GET: MenuPizza/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PIZZA pIZZA = db.PIZZA.Find(id);
            if (pIZZA == null)
            {
                return HttpNotFound();
            }
            return View(pIZZA);
        }

        // GET: MenuPizza/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuPizza/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPizza,NomePizza,Ingredienti,Prezzo,Foto")] PIZZA pIZZA)
        {
            if (ModelState.IsValid)
            {
                db.PIZZA.Add(pIZZA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pIZZA);
        }

        // GET: MenuPizza/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PIZZA pIZZA = db.PIZZA.Find(id);
            if (pIZZA == null)
            {
                return HttpNotFound();
            }
            return View(pIZZA);
        }

        // POST: MenuPizza/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPizza,NomePizza,Ingredienti,Prezzo,Foto")] PIZZA pIZZA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pIZZA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pIZZA);
        }

        // GET: MenuPizza/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PIZZA pIZZA = db.PIZZA.Find(id);
            if (pIZZA == null)
            {
                return HttpNotFound();
            }
            return View(pIZZA);
        }

        // POST: MenuPizza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PIZZA pIZZA = db.PIZZA.Find(id);
            db.PIZZA.Remove(pIZZA);
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
