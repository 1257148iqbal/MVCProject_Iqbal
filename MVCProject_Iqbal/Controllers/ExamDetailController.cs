using MVCProject_Iqbal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject_Iqbal.Controllers
{
    public class ExamDetailController : Controller
    {
        private MVCProject_Iqbal_DBEntities1 db = new MVCProject_Iqbal_DBEntities1();
        // GET: ExamDetail
        public ActionResult Index()
        {
            var examDetails = db.ExamDetails.Include(t => t.Trainee);
            return View(examDetails.ToList());
        }


        public ActionResult Create()
        {
            ViewBag.TraineeId = new SelectList(db.Trainees, "TraineeID", "TraineeName");   //for dropdown
            return View();
        }

        //
        // POST: /Exam/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamDetail examdetail)
        {
            if (ModelState.IsValid)
            {
                db.ExamDetails.Add(examdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TraineeId = new SelectList(db.Trainees, "TraineeID", "TraineeName", examdetail.TraineeID); //for dropdown
            return View(examdetail);
        }

        //
        // GET: /Exam/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ExamDetail examdetail = db.ExamDetails.Find(id);
            if (examdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.TraineeId = new SelectList(db.Trainees, "TraineeID", "TraineeName", examdetail.TraineeID); //for dropdown
            return View(examdetail);
        }

        //
        // POST: /Exam/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExamDetail examdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TraineeId = new SelectList(db.Trainees, "TraineeID", "TraineeName", examdetail.TraineeID); //for dropdown
            return View(examdetail);
        }

        //
        // GET: /Exam/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ExamDetail examdetail = db.ExamDetails.Find(id);
            if (examdetail == null)
            {
                return HttpNotFound();
            }
            return View(examdetail);
        }

        //
        // POST: /Exam/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamDetail examdetail = db.ExamDetails.Find(id);
            db.ExamDetails.Remove(examdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}