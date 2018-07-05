using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using System.Data.Entity;
using System.Web.Security;
using App.Controllers;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorize]
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
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserAccount model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = null;
                using (AppContext db = new AppContext())
                {
                    user = db.UserAccounts.FirstOrDefault(u => u.Username == model.Username);
                }
                if (user == null)
                {
                    using (AppContext db = new AppContext())
                    {
                        db.UserAccounts.Add(new UserAccount
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            Username = model.Username,
                            Password = model.Password,
                            ConfirmPassword = model.ConfirmPassword,
                            RoleId = 2
                        });
                        db.SaveChanges();

                        user = db.UserAccounts.Where(u => u.Username == model.Username &&
                        u.Password == model.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, true);
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (AppContext db = new AppContext())
                {
                    var usr = db.UserAccounts.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                    if (usr != null)
                    {
                        Session["UserID"] = usr.UserID.ToString();
                        Session["Username"] = usr.Username.ToString();
                        FormsAuthentication.SetAuthCookie(usr.Username, true);
                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password is wrong.");
                    }
                }
                return View();
            }
            return View(model);

        }

        [Authorize]
        public ActionResult LoggedIn()
        {
                AppContext db = new AppContext();
                var protocols = db.Protocols.Include(p=>p.Responsible).Include(p => p.Organization);
                return View(protocols.ToList());
            
        }

        [Authorize]
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

        [Authorize]
        public ActionResult userProtocols()
        {
            if (Session["UserID"] != null)
            {
                AppContext db = new AppContext();
                var protocols = db.Protocols.Include(p => p.Organization).Include(p => p.Responsible);
                return View(protocols.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }


        
    }
