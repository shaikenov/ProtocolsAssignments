using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        AppContext db = new AppContext();

        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
                else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize(Roles ="admin")]
        public ActionResult AssignmentsList()
        {
            if (Session["UserID"] != null)
            {
                var assignments = db.Assignments.Include(p => p.Protocol);
                return View(assignments.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult ProtocolsList()
        {
            if (Session["UserID"] != null)
            {
                var protocols = db.Protocols.Include(p => p.Organization);
                return View(protocols.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


       
        
        [HttpGet]
        [Authorize(Roles="admin")]
        public ActionResult Create()
        {
          
                SelectList protocols = new SelectList(db.Protocols, "Id", "Title");
                ViewBag.Protocols = protocols;
                ViewBag.Responsibles = db.Responsibles.ToList();
                return View();
            
                
        }

        [HttpPost]

        public ActionResult Create(Assignment assignment, int[] selectedResponsibles)
        {
                if (selectedResponsibles != null)
            {
                foreach (var r in db.Responsibles.Where(re=> selectedResponsibles.Contains(re.Id)))
                {
                    assignment.Responsibles.Add(r);
                }
                
            }
            db.Assignments.Add(assignment);
            db.SaveChanges();
            return RedirectToAction("AssignmentsList");
        }
        
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            
                if (id == null)
                {
                    return HttpNotFound();
                }

                Assignment assignment = db.Assignments.Find(id);
                if (assignment != null)
                {
                    SelectList protocols = new SelectList(db.Protocols, "Id", "Title", assignment.ProtocolId);
                    //SelectList responsibles = new SelectList(db.Responsibles, "Id", "Name");
                    ViewBag.Protocols = protocols;
                    ViewBag.Responsibles = db.Responsibles.ToList();
                    return View(assignment);
                }
            return RedirectToAction("AssignmentsList");
            }
 
        
        [HttpPost]      
        public ActionResult Edit(Assignment assignment, int[] selectedResponsibles)
        {
            Assignment newAssignment = db.Assignments.Find(assignment.Id);
            newAssignment.Title = assignment.Title;
            newAssignment.Deadline = assignment.Deadline;
            newAssignment.ProtocolId = assignment.ProtocolId;
            newAssignment.Responsibles.Clear();

            if (selectedResponsibles != null)
            {
                foreach (var r in db.Responsibles.Where(re => selectedResponsibles.Contains(re.Id)))
                {
                    newAssignment.Responsibles.Add(r);
                }
            }

            db.Entry(newAssignment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AssignmentsList");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Assignment assignment = db.Assignments.Find(id);
            if (assignment != null)
            {
                db.Assignments.Remove(assignment);
                db.SaveChanges();
            }
            return RedirectToAction("AssignmentsList");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateProtocol()

        {
                ViewBag.Responsibles = db.Responsibles.ToList();
                SelectList organizations = new SelectList(db.Organizations, "Id", "Title");
                ViewBag.Organizations = organizations;        
                return View();
        }
        
        [HttpPost]
        
        public ActionResult CreateProtocol(Protocol protocol, int[] selectedResponsibles)
        {
                if (selectedResponsibles != null)
                {
                    foreach (var r in db.Responsibles.Where(re => selectedResponsibles.Contains(re.Id)))
                    {
                    protocol.Responsibles.Add(r);
                    }

                }
                db.Protocols.Add(protocol);
                db.SaveChanges();
                return RedirectToAction("ProtocolsList");
       

        }

        [HttpGet]
        [Authorize(Roles = "admin")]

        public ActionResult EditProtocol(int? id)
        {
                if (id == null)
            {
                return HttpNotFound();
            }

            Protocol protocol = db.Protocols.Find(id);
            if (protocol != null)
            {
                SelectList organizations = new SelectList(db.Organizations, "Id", "Title");
                
                ViewBag.Organizations = organizations;
                ViewBag.Responsibles = db.Responsibles.ToList();
                return View(protocol);
            }
            return RedirectToAction("AssignmentsList");
        }

        [HttpPost]
        
        public ActionResult EditProtocol(Protocol protocol, int[] selectedResponsibles)
        {
            Protocol newProtocol = db.Protocols.Find(protocol.Id);
            newProtocol.Title = protocol.Title;
            newProtocol.DateOfSubmission = protocol.DateOfSubmission;
            newProtocol.ProtID = protocol.ProtID;
            newProtocol.Responsibles.Clear();

            if (selectedResponsibles != null)
            {
                foreach (var r in db.Responsibles.Where(re => selectedResponsibles.Contains(re.Id)))
                {
                    newProtocol.Responsibles.Add(r);
                }
            }

            db.Entry(newProtocol).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ProtocolsList");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteProtocol(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Protocol protocol = db.Protocols.Find(id);

            if (protocol != null)
            {
                db.Protocols.Remove(protocol);
                db.SaveChanges();
            }
            return RedirectToAction("ProtocolsList");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DetailsProtocol(int? id)
        {
            if (Session["UserID"] != null)
            {
             if (id == null)
            {
                return HttpNotFound();
            }

            Protocol protocol = db.Protocols.Find(id);
            if (protocol != null)
            {
                ViewBag.necessaryProtocol = protocol.ProtID;
                var assignments = db.Assignments.Include(p => p.Protocol);
                return View(assignments.ToList());
            }
            return RedirectToAction("ProtocolsList");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
               
        }

        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}