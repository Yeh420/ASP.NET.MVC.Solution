using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.EFModels;

namespace WebApplication1.Controllers
{
    public class GuestBooksController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: GuestBooks
        public ActionResult Index()
        {
            return View(db.GuestBooks.ToList());
        }

        // GET: GuestBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestBook guestBook = db.GuestBooks.Find(id);
            if (guestBook == null)
            {
                return HttpNotFound();
            }
            return View(guestBook);
        }

        // GET: GuestBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuestBooks/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,CellPhone,Message,CreatedTime")] GuestBook guestBook)
        {
            if (ModelState.IsValid)
            {
                db.GuestBooks.Add(guestBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guestBook);
        }

        // GET: GuestBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestBook guestBook = db.GuestBooks.Find(id);
            if (guestBook == null)
            {
                return HttpNotFound();
            }
            return View(guestBook);
        }

        // POST: GuestBooks/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,CellPhone,Message,CreatedTime")] GuestBook guestBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guestBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guestBook);
        }

        // GET: GuestBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestBook guestBook = db.GuestBooks.Find(id);
            if (guestBook == null)
            {
                return HttpNotFound();
            }
            return View(guestBook);
        }

        // POST: GuestBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuestBook guestBook = db.GuestBooks.Find(id);
            db.GuestBooks.Remove(guestBook);
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
