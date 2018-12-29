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
    public class RececoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rececoes
        public ActionResult Index()
        {
            var result = db.Emprestimos.Join(db.Users, x => x.userID, y => y.Id, 
                (x,y) => new CustomModelRececoes{
                    EmprestimoID = x.EmprestimoID,
                    NomeP = y.NomeP,
                    NomeU = y.NomeU,
                    data_fim = x.data_fim,
                    data_inicio = x.data_inicio,
                    devolvido = x.devolvido
                }).ToList();
            
            ViewBag.data = result;

            return View(result);
        }

        // GET: Rececoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rececao rececao = db.Rececoes.Find(id);
            if (rececao == null)
            {
                return HttpNotFound();
            }
            return View(rececao);
        }

        //POST: Rececoes/getPeca
        public ActionResult GetPeca(int? id)
        {
            Peca peca = db.Pecas.Where(x => x.PecaID == id).First();

            return Json(new { EstadoAtual = peca.Estado }, JsonRequestBehavior.AllowGet);
        }

        // GET: Rececoes/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Peca> pecas = db.Pecas.ToList();
            List<Emp_Peca> pecas_Emp = db.Emp_Peca.Where(x => x.EmprestimoID == id).ToList();
            List<Peca> newList = new List<Peca>();
            foreach(Emp_Peca empPeca in pecas_Emp)
            {
                foreach(Peca peca in pecas)
                {
                    if (peca.PecaID.Equals(empPeca.PecaID)) 
                        newList.Add(peca);
                }
            }

            ViewBag.Pecas = new SelectList(newList, "PecaID", "nomePeca");
            return View();
        }



        // POST: Rececoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rececaoID,formulario,antes,depois,cumprimento")] Rececao rececao)
        {
            if (ModelState.IsValid)
            {
                db.Rececoes.Add(rececao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rececao);
        }

        // GET: Rececoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rececao rececao = db.Rececoes.Find(id);
            if (rececao == null)
            {
                return HttpNotFound();
            }
            return View(rececao);
        }

        // POST: Rececoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rececaoID,formulario,antes,depois,cumprimento")] Rececao rececao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rececao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rececao);
        }

        // GET: Rececoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rececao rececao = db.Rececoes.Find(id);
            if (rececao == null)
            {
                return HttpNotFound();
            }
            return View(rececao);
        }

        // POST: Rececoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rececao rececao = db.Rececoes.Find(id);
            db.Rececoes.Remove(rececao);
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
