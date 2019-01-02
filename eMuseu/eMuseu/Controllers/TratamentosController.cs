using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eMuseu.Models;

namespace eMuseu.Controllers
{
    public class TratamentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tratamentos
        [Authorize(Roles = "especialista")]
        public ActionResult Index()
        {
            return View(db.Tratamentos.ToList());
        }

        // GET: Tratamentos/Details/5
        [Authorize(Roles = "especialista")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamentos tratamentos = db.Tratamentos.Find(id);
            int PecaID = tratamentos.PecaID;
            Peca peca = db.Pecas.Find(PecaID);
            ViewBag.pecaName = peca.nomePeca;


            if (tratamentos == null)
            {
                return HttpNotFound();
            }

            
            return View(tratamentos);
        }

        // GET: Tratamentos/Create
        [Authorize(Roles = "especialista")]
        public ActionResult Create()
        {
            ViewBag.Pecas = new SelectList(db.Pecas.ToList(), "PecaID", "nomePeca");
            return View();
        }

        // POST: Tratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "especialista")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TratamentoID,NomeTratamento")] Tratamentos tratamentos, int PecaID, String Estado)
        {
            if (ModelState.IsValid)
            {
                tratamentos.PecaID = PecaID;
                db.Tratamentos.Add(tratamentos);
                db.SaveChanges();

                Peca peca = db.Pecas.Single(x => x.PecaID == PecaID);
                peca.Estado = Estado;
                db.Entry(peca).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pecas = new SelectList(db.Pecas.ToList(), "PecaID", "nomePeca");
            return View(tratamentos);
        }

        // GET: Tratamentos/Edit/5
        [Authorize(Roles = "especialista")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamentos tratamentos = db.Tratamentos.Find(id);
            if (tratamentos == null)
            {
                return HttpNotFound();
            }
            return View(tratamentos);
        }

        // POST: Tratamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "especialista")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TratamentoID,NomeTratamento")] Tratamentos tratamentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tratamentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tratamentos);
        }

        // GET: Tratamentos/Delete/5
        [Authorize(Roles = "especialista")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamentos tratamentos = db.Tratamentos.Find(id);
            if (tratamentos == null)
            {
                return HttpNotFound();
            }
            return View(tratamentos);
        }

        // POST: Tratamentos/Delete/5
        [Authorize(Roles = "especialista")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tratamentos tratamentos = db.Tratamentos.Find(id);
            db.Tratamentos.Remove(tratamentos);
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
