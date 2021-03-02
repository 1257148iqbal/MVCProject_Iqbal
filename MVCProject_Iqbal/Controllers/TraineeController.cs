using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


// Add new namespase
using System.Threading.Tasks;
using AutoMapper;
using MVCProject_Iqbal.Models.ViewModels;
using MVCProject_Iqbal.Models;
using System.Net;
using System.Data.Entity;
using PagedList;

namespace MVCProject_Iqbal.Controllers
{
    [RoutePrefix("TraineeInfo")]
    public class TraineeController : Controller
    {
        private MVCProject_Iqbal_DBEntities1 db = new MVCProject_Iqbal_DBEntities1();

        // GET: Trainee
        [Route("Home")]
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var trainees = from t in db.Trainees
                           select t;
            if (!string.IsNullOrEmpty(searchString))
            {
                trainees = trainees.Where(t => t.TraineeName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    trainees = trainees.OrderByDescending(t => t.TraineeName);
                    break;
                default:
                    trainees = trainees.OrderBy(t => t.TraineeName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(trainees.ToPagedList(pageNumber, pageSize));
        }

        // GET: Details
        [Route("Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = db.Trainees.Single(t => t.TraineeID == id);
            var trainee = Mapper.Map<Trainee, TraineeVM>(query);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // GET: Create
        public ActionResult Create()
        {
            var teacherslist = db.Teachers.ToList();
            ViewBag.TeacherID = new SelectList(teacherslist, "TeacherID", "TeacherName");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TraineeVM traineeVM)
        {
            if (ModelState.IsValid)
            {
                var teacherslist = db.Teachers.ToList();
                ViewBag.TeacherID = new SelectList(teacherslist, "TeacherID", "TeacherName");
                var trainee = Mapper.Map<Trainee>(traineeVM);
                db.Trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(traineeVM);
        }

        // GET: Edit
        public ActionResult Edit(int? id)
        {
            var query = db.Trainees.Single(t => t.TraineeID == id);
            var trainee = Mapper.Map<Trainee, TraineeVM>(query);
            return View(trainee);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TraineeVM traineeVM)
        {
            if (ModelState.IsValid)
            {
                var trainee = Mapper.Map<Trainee>(traineeVM);
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(traineeVM);
        }

        // GET: Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            var query = db.Trainees.Single(t => t.TraineeID == id);
            var trainee = Mapper.Map<Trainee, TraineeVM>(query);
            return View(trainee);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, TraineeVM traineeVM)
        {
            var query = db.Trainees.Single(t => t.TraineeID == id);
            var trainee = Mapper.Map<Trainee, TraineeVM>(query);
            db.Trainees.Remove(query);  
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