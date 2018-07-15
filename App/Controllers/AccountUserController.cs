using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;

namespace App.Controllers
{
    public class AccountUserController : Controller
    {

        AppContext db = new AppContext();

        
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var users = db.UserAccounts;
            return View(users.ToList());
        }


        [Authorize(Roles ="admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            UserAccount user = db.UserAccounts.Find(id);
            
            if (user != null)
            {
                db.UserAccounts.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}



