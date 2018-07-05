using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;

namespace App.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: Organization
        AppContext db = new AppContext();


        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
                var organizations = db.Organizations;
                return View(organizations.ToList());
        }


        [HttpGet]
        [Authorize(Roles = "admin")]

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Create(Organization organization)
        {
            db.Organizations.Add(organization);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        [Authorize(Roles = "admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Organization organization = db.Organizations.Find(id);
            if (organization != null)
            {
                return View(organization);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public ActionResult Edit(Organization organization)
        {
           db.Entry(organization).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [Authorize(Roles ="admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Organization organization = db.Organizations.Find(id);

            if (organization != null)
            {
                db.Organizations.Remove(organization);
                
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



    }
}