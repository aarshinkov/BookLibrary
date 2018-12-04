using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookLibrary.Entity;
using BookLibrary.Models;
using PagedList;

namespace BookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private BookCatalogDbContext db = new BookCatalogDbContext();

        // GET: Books
        public ActionResult Index(int? page, string bookTitleSearch, string writerFNameSearch, string writerLNameSearch, string sortOrder)
        {
            //TODO for search by genre add string GenreId as parameter
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IQueryable<Book> books = db.Books.AsQueryable();

            ViewBag.TitleSearch = bookTitleSearch;

            if (!String.IsNullOrEmpty(bookTitleSearch) && this.User.Identity.IsAuthenticated)
            {
                books = books.Where(x => x.Title.Contains(bookTitleSearch));
            }
            if (!String.IsNullOrEmpty(writerFNameSearch) && this.User.Identity.IsAuthenticated)
            {
                books = books.Where(x => x.Writer.FirstName.Contains(writerFNameSearch));
            }
            if (!String.IsNullOrEmpty(writerLNameSearch) && this.User.Identity.IsAuthenticated)
            {
                books = books.Where(x => x.Writer.LastName.Contains(writerLNameSearch));
            }

            //TODO for search by genre
            //if (!String.IsNullOrEmpty(GenreId) && this.User.Identity.IsAuthenticated)
            //{
            //    books = books.Where(x => x.Genre.GenreName.Contains(GenreId));
            //}

            ViewBag.CurrentSortParam = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.ReleaseDateSortParam = sortOrder == "release_date_asc" ? "release_date_desc" : "release_date_asc";
            ViewBag.WriterSortParam = sortOrder == "writer_asc" ? "writer_desc" : "writer_asc";
            //TODO for search by genre
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(x => x.Title);
                    break;
                case "release_date_asc":
                    books = books.OrderBy(x => x.ReleaseDate);
                    break;
                case "release_date_desc":
                    books = books.OrderByDescending(x => x.ReleaseDate);
                    break;
                case "writer_asc":
                    books = books.OrderBy(x => x.Writer.UserName);
                    break;
                case "writer_desc":
                    books = books.OrderByDescending(x => x.Writer.UserName);
                    break;
                default:
                    books = books.OrderBy(x => x.Title);
                    break;
            }

            return View(books.ToPagedList(pageNumber, pageSize));

            //var books = db.Books.Include(b => b.Genre).Include(b => b.Writer);
            //return View(books.ToList());
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,ReleaseDate,WriterId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", book.GenreId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName", book.WriterId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", book.GenreId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName", book.WriterId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,ReleaseDate,WriterId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", book.GenreId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName", book.WriterId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
