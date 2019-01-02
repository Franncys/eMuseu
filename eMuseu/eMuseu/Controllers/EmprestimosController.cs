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
        [Authorize(Roles = "administrador")]
        public ActionResult Index()
        {
            return View(db.Emprestimos.ToList());
        }
        [Authorize(Roles = "administrador")]
        public ActionResult IndexNotValid()
        {
            return View(db.Emprestimos.Where(x => x.validado == false).ToList());
        }
        [Authorize(Roles = "administrador, registado")]
        public ActionResult IndexPessoal()
        {
            String id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return View(db.Emprestimos.Where(x => x.userID == id).ToList());
        }

        // GET: Emprestimos/Details/5
        [Authorize(Roles = "resgistado, administrador")]
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
        [Authorize(Roles = "registado")]
        public ActionResult Create()
        {
            var idEmpPecas = db.Emp_Peca.Where(x => x.data_Entregue == null).ToList();
            var idPecas = db.Pecas.ToList();
            var aux = db.Pecas.ToList();

            foreach (Emp_Peca emp_Peca in idEmpPecas)
            {
                foreach(Peca peca in idPecas)
                {
                    if (peca.PecaID == emp_Peca.PecaID)
                        aux.Remove(peca);
                }
            }
            
            ViewBag.pecas = new SelectList(aux, "PecaID", "nomePeca");
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "registado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmprestimoID,data_fim")] Emprestimo emprestimo, int []pecasID)
        {
            if (ModelState.IsValid && pecasID != null)
            {
                if(emprestimo.data_fim > DateTime.Now) {
                    ModelState.AddModelError("", "Data Incorreta!");
                    return View(emprestimo);
                }

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

                return RedirectToAction("IndexPessoal");
            }

            var idEmpPecas = db.Emp_Peca.Where(x => x.data_Entregue == null).ToList();
            var idPecas = db.Pecas.ToList();
            var aux = db.Pecas.ToList();

            foreach (Emp_Peca emp_Peca in idEmpPecas)
            {
                foreach (Peca peca in idPecas)
                {
                    if (peca.PecaID == emp_Peca.PecaID)
                        aux.Remove(peca);
                }
            }

            ViewBag.pecas = new SelectList(aux, "PecaID", "nomePeca");

            ModelState.AddModelError("", "Tem de Inserir Produtos");
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        [Authorize(Roles = "registado")]
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
        [Authorize(Roles = "registado, administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmprestimoID,data_inicio,data_fim")] Emprestimo emprestimo, int[] pecasID)
        {
            string query = "SELECT Pecas.* "
                + "FROM Pecas "
                + "INNER JOIN Emp_Peca ON Pecas.PecaID = Emp_Peca.PecaID "
                + "WHERE Emp_Peca.EmprestimoID = " + emprestimo.EmprestimoID + "";

            ViewBag.pecasEmp = new SelectList(db.Database.SqlQuery<Peca>(query).ToList(), "PecaID", "nomePeca");

            string queryV2 = "SELECT Pecas.* "
                + "FROM Pecas "
                + "WHERE Pecas.PecaID NOT IN (SELECT PecaID FROM Emp_Peca)";

            ViewBag.pecas = new SelectList(db.Database.SqlQuery<Peca>(queryV2).ToList(), "PecaID", "nomePeca");
            
            if (ModelState.IsValid && pecasID != null)
            {
                int i = 0;
                if (emprestimo.data_fim < emprestimo.data_inicio)
                {
                    ModelState.AddModelError("", "Datas Incorretas!");
                    return View(emprestimo);
                }
                emprestimo.data_inicio = DateTime.Now;
                emprestimo.userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.Entry(emprestimo).State = EntityState.Modified;
                var emp_Peca = db.Emp_Peca.Where(x => x.EmprestimoID.Equals(emprestimo.EmprestimoID)).ToList();
                foreach(Emp_Peca peca in emp_Peca)
                {
                    i++;
                    db.Emp_Peca.Remove(peca);
                }
               
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
                
                return RedirectToAction("IndexPessoal");
            }

            ModelState.AddModelError("", "Tem de Adicionar Pecas!");
            return View(emprestimo);
        }
        [Authorize(Roles = "administrador")]
        public ActionResult Validate(int? id)
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
            emprestimo.validado = true;
            db.SaveChanges();
            return RedirectToAction("IndexNotValid");
        }

        [Authorize(Roles = "registado, administrador")]
        public ActionResult Delete(int? id)
        {
            Emprestimo emprestimo = db.Emprestimos.Find(id);

            if (emprestimo == null)
            {
                return HttpNotFound();
            }

            var pecasEmp = db.Emp_Peca.Where(x => x.EmprestimoID == id);
            foreach (Emp_Peca emp_Peca in pecasEmp)
                db.Emp_Peca.Remove(emp_Peca);

            db.Emprestimos.Remove(emprestimo);
            db.SaveChanges();
            return RedirectToAction("IndexPessoal");
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
