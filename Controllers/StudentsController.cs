using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AjaxCRUD.Models;

namespace AjaxCRUD.Controllers
{
    public class StudentsController : Controller
    {
        private Entities db = new Entities();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/CreateOrEdit/
        // GET: Students/CreateOrEdit/5

        public ActionResult CreateOrEdit(int? id)
        {
            if (id == null)
            {
                return View(new Student() { }) ;
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/CreateOrEdit/
        // POST: Students/CreateOrEdit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(int id,[Bind(Include = "ID,Name,Age")] Student student)
        {
            if (ModelState.IsValid)
            {
                if(id==0)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();

                }

 
                return RedirectToAction("Index","Home");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
