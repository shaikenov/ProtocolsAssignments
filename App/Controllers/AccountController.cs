using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using System.Data.Entity;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (AppContext db = new AppContext())
            {
                return View(db.UserAccounts.ToList());
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (AppContext db = new AppContext())
                {
                    db.UserAccounts.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + " successsfully registered";
                
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (AppContext db = new AppContext())
            {
                var usr = db.UserAccounts.Single(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong.");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                AppContext db = new AppContext();
                var protocols = db.Protocols.Include(p=>p.Responsible).Include(p => p.Organization);
                return View(protocols.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult userAssignments()
        {
            if (Session["UserID"] != null)
            {
                AppContext db = new AppContext();
                var assignments = db.Assignments.Include(p => p.Protocol).Include(p => p.Responsible);
                return View(assignments.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


    }


        
    }
