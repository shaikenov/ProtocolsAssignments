using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;

namespace App.Controllers
{
    public class ResponsibleController : Controller
    {

        AppContext db = new AppContext();

        public ActionResult Index()
        {
            var responsibles = db.Responsibles;
            return View(responsibles.ToList());
        }


        [HttpGet]

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Create(Responsible responsible)
        {
            db.Responsibles.Add(responsible);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Responsible responsible = db.Responsibles.Find(id);
            if (responsible != null)
            {
                return View(responsible);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public ActionResult Edit(Responsible responsible)
        {
            db.Entry(responsible).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Responsible responsible = db.Responsibles.Find(id);
            
            if (responsible != null)
            {
                db.Responsibles.Remove(responsible);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}



