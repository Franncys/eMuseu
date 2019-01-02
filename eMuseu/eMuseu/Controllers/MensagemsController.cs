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
using Microsoft.AspNet.Identity.EntityFramework;

namespace eMuseu.Controllers
{
    public class MensagemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mensagems
        [Authorize(Roles = "registado,especialista")]
        public ActionResult Index()
        {
            return View(db.Mensagens.ToList());
        }

        [Authorize(Roles = "registado,especialista")]
        public ActionResult IndexEnviadas()
        {
            return View(db.Mensagens.ToList());
        }

        [Authorize(Roles = "administrador")]
        public ActionResult IndexTodas()
        {
            return View(db.Mensagens.ToList());
        }

        // GET: Mensagems/Details/5
        [Authorize(Roles = "administrador,registado,especialista")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagem mensagem = db.Mensagens.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }
        [Authorize(Roles = "registado,especialista")]
        // GET: Mensagems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mensagems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "registado,especialista")]
        public ActionResult Create([Bind(Include = "MensagemID,OrigemID,DestinoID,EmailDest,Msg")] Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                var users = (from x in db.Users where x.Email == mensagem.EmailDest select x).FirstOrDefault();
                string userId = string.Empty;
                if (users != null)
                {
                    var currentId = User.Identity.GetUserId();
                    var currentUser = db.Users.FirstOrDefault(x => x.Id == currentId);
                    var currentEmail = currentUser.Email;
                    userId = users.Id;
                    mensagem.DestinoID = userId;
                    mensagem.OrigemID = currentId;
                    mensagem.EmailOri = currentEmail;
                    db.Mensagens.Add(mensagem);
                    db.SaveChanges();
                    return RedirectToAction("IndexEnviadas");
                }
                ModelState.AddModelError("", "Email inválido ou não existente!");
                return View(mensagem);

            }

            return View(mensagem);
        }

        // GET: Mensagems/Edit/5
        [Authorize(Roles = "administrador,registado,especialista")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagem mensagem = db.Mensagens.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // POST: Mensagems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador,registado,especialista")]
        public ActionResult Edit([Bind(Include = "MensagemID,OrigemID,DestinoID,Msg")] Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensagem);
        }


        // POST: Mensagems/Delete/5
        [Authorize(Roles = "administrador,registado,especialista")]
        public ActionResult Delete(int? id)
        {
            Mensagem mensagem = db.Mensagens.Find(id);
            db.Mensagens.Remove(mensagem);
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
