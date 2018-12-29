using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eMuseu.Models;
using Microsoft.AspNet.Identity;

namespace eMuseu.Controllers
{
    public class EmprestimosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Emprestimos
        public ActionResult Index()
        {
            return View(db.Emprestimos.ToList());
        }

        // GET: Emprestimos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public ActionResult Create()
        {
            ViewBag.pecas = new SelectList(db.Pecas.ToList(), "PecaID", "nomePeca");
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmprestimoID,data_fim")] Emprestimo emprestimo, int []pecasID)
        {
            if (ModelState.IsValid)
            {
                emprestimo.data_inicio = DateTime.Now;
                emprestimo.userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.Emprestimos.Add(emprestimo);
                db.SaveChanges();

                int lastEmprestimoID = emprestimo.EmprestimoID;

                foreach (int pecaID in pecasID)
                {
                    Emp_Peca empPeca = new Emp_Peca();
                    empPeca.EmprestimoID = lastEmprestimoID;
                    empPeca.PecaID = pecaID;
                    var estado = db.Pecas.Where(x => x.PecaID.Equals(pecaID)).Select(x => x.Estado).ToList();
                    empPeca.Estado = estado.First();
                    db.Emp_Peca.Add(empPeca);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.pecas = new SelectList(db.Pecas.ToList(), "PecaID", "nomePeca");
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);

            string query = "SELECT Pecas.* "
                + "FROM Pecas "
                + "INNER JOIN Emp_Peca ON Pecas.PecaID = Emp_Peca.PecaID "
                + "WHERE Emp_Peca.EmprestimoID = " + id + "";
            
            ViewBag.pecasEmp = new SelectList(db.Database.SqlQuery<Peca>(query).ToList(), "PecaID", "nomePeca");

            string queryV2 = "SELECT Pecas.* "
                + "FROM Pecas "
                + "WHERE Pecas.PecaID NOT IN (SELECT PecaID FROM Emp_Peca)";

            ViewBag.pecas = new SelectList(db.Database.SqlQuery<Peca>(queryV2).ToList(), "PecaID", "nomePeca");

            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmprestimoID,data_inicio,data_fim")] Emprestimo emprestimo, int[] pecasID)
        {
            if (ModelState.IsValid)
            {
                emprestimo.data_inicio = DateTime.Now;
                db.Entry(emprestimo).State = EntityState.Modified;
                var emp_Peca = db.Emp_Peca.Where(x => x.EmprestimoID.Equals(emprestimo.EmprestimoID)).ToList();
                foreach(Emp_Peca peca in emp_Peca)
                {
                    db.Emp_Peca.Remove(peca);
                }
                //Remover da Base de dados
               /* while (emp_Peca != null)
                {
                    db.Emp_Peca.Remove(emp_Peca);
                    emp_Peca = db.Emp_Peca.Find(emprestimo.EmprestimoID);
                }*/

                //Inserir novas Pecas
                foreach (int pecaID in pecasID)
                {
                    Emp_Peca empPeca = new Emp_Peca();
                    empPeca.EmprestimoID = emprestimo.EmprestimoID;
                    empPeca.PecaID = pecaID;
                    var estado = db.Pecas.Where(x => x.PecaID.Equals(pecaID)).Select(x => x.Estado).ToList();
                    empPeca.Estado = estado.First();
                    db.Emp_Peca.Add(empPeca);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            db.Emprestimos.Remove(emprestimo);
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
