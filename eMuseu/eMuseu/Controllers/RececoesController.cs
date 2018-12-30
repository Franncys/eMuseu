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
                }).Where(x => x.devolvido == false).ToList();
            
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
        public ActionResult GetPeca(int? id, int? emprestimoID)
        {
            //Alterar para ir buscar o Estado na Tabela Emp_Peca
            Emp_Peca peca = db.Emp_Peca.Where(x => x.PecaID == id && x.EmprestimoID == emprestimoID).First();

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
            List<Emp_Peca> pecas_Emp = db.Emp_Peca.Where(x => x.EmprestimoID == id && x.data_Entregue == null).ToList();
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
            ViewBag.emprestimoID = id;
            return View();
        }



        // POST: Rececoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rececaoID,formulario,antes,depois,cumprimento")] Rececao rececao, int emprestimoID, int PecaID)
        {
            List<Peca> pecas = db.Pecas.ToList();
            List<Emp_Peca> pecas_Emp = db.Emp_Peca.Where(x => x.EmprestimoID == emprestimoID).ToList();
            List<Peca> newList = new List<Peca>();
            foreach (Emp_Peca empPeca in pecas_Emp)
            {
                foreach (Peca peca in pecas)
                {
                    if (peca.PecaID.Equals(empPeca.PecaID))
                        newList.Add(peca);
                }
            }

            ViewBag.Pecas = new SelectList(newList, "PecaID", "nomePeca");
            ViewBag.emprestimoID = emprestimoID;

            if (ModelState.IsValid)
            {
                //Altera DataFinal na Table Emp_Peca
                Emp_Peca empPeca = db.Emp_Peca.Single(x => x.EmprestimoID == emprestimoID && x.PecaID == PecaID);
                empPeca.data_Entregue = System.DateTime.Now;
                db.Entry(empPeca).State = EntityState.Modified;
                db.SaveChanges();

                //Altera Estado na Tabela da Peca
                Peca peca = db.Pecas.Single(x => x.PecaID == PecaID);
                peca.Estado = rececao.depois;
                db.Entry(peca).State = EntityState.Modified;

                //Verificar se Existe mais pecas neste emprestimo
                int result = db.Emp_Peca.Where(x => x.EmprestimoID == emprestimoID && x.data_Entregue == null).Count();
                if (result == 0)
                {
                    Emprestimo emp = db.Emprestimos.Single(x => x.EmprestimoID == emprestimoID);
                    emp.devolvido = true;
                    db.Entry(emp).State = EntityState.Modified;
                }
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
