using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject_Iqbal.Models;

namespace MVCProject_Iqbal.Controllers
{
    public class TeacherController : Controller
    {
        private MVCProject_Iqbal_DBEntities1 db = new MVCProject_Iqbal_DBEntities1();

        // GET: Teacher
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(Teacher model)
        {
            if (ModelState.IsValid)
            {

                db.Teachers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_Detail", db.Teachers.ToList());
        }

        public ActionResult Edit(int id)
        {
            return PartialView(db.Teachers.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Teacher model, int id)
        {
            if (ModelState.IsValid)
            {

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return PartialView("_Detail", db.Teachers.ToList());
        }

        public ActionResult Delete(int id)
        {

            db.Teachers.Remove(db.Teachers.Find(id));
            db.SaveChanges();
            return PartialView("Index", db.Teachers.ToList());
        }
    }
}
