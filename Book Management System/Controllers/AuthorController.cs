using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Book_Management_System.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        
            private readonly BookDBEntities _context = new BookDBEntities(); // Replace with your DbContext class

            // GET: Author
            public ActionResult Index()
            {
                var authors = _context.Authors.Include(a => a.Books).ToList(); // Eager loading Books
                return View(authors);
            }

            // GET: Author/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == id);
                if (author == null)
                {
                    return HttpNotFound();
                }

                return View(author);
            }

            // GET: Author/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: Author/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "AuthorId,AuthorName")] Author author)
            {
                if (ModelState.IsValid)
                {
                    _context.Authors.Add(author);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(author);
            }

            // GET: Author/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var author = _context.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }

                return View(author);
            }

            // POST: Author/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "AuthorId,AuthorName")] Author author)
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(author).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(author);
            }

            // GET: Author/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var author = _context.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }

                return View(author);
            }

            // POST: Author/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                var author = _context.Authors.Find(id);
                if (author != null)
                {
                    _context.Authors.Remove(author);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                base.Dispose(disposing);
            }
        }
    }