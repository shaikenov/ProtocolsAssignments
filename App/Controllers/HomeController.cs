﻿using App.Models;
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
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AssignmentsList()
        {
            var assignments = db.Assignments.Include(p => p.Protocol).Include(p => p.Responsible);
            return View(assignments.ToList());
        }

        public ActionResult ProtocolsList()
        {
            var protocols = db.Protocols.Include(p => p.Responsible).Include(p => p.Organization);
            return View(protocols.ToList());
        }

        

        public ActionResult ResponsiblesList()
        {
            var responsibles = db.Responsibles;
            return View(responsibles.ToList());
        }
    
        [HttpGet]

        public ActionResult Create()
        {
            SelectList protocols = new SelectList(db.Protocols, "Id", "Title");
            SelectList responsibles = new SelectList(db.Responsibles, "Id", "Name");
            ViewBag.Protocols = protocols;
            ViewBag.Responsibles = responsibles;
            return View();
        }

        [HttpPost]

        public ActionResult Create(Assignment assignment)
        {
            db.Assignments.Add(assignment);
            db.SaveChanges();

            return RedirectToAction("AssignmentsList");

        }

        [HttpGet]

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
                SelectList responsibles = new SelectList(db.Responsibles, "Id", "Name");
                ViewBag.Protocols = protocols;
                ViewBag.Responsibles = responsibles;
                return View(assignment);
            }
            return RedirectToAction("AssignmentsList");
        }

        [HttpPost]

        public ActionResult Edit(Assignment assignment)
        {
            db.Entry(assignment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AssignmentsList");

        }

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

        public ActionResult CreateProtocol()
        {
            SelectList organizations = new SelectList(db.Organizations, "Id", "Title");
            SelectList responsibles = new SelectList(db.Responsibles, "Id", "Name");
            ViewBag.Organizations = organizations;
            ViewBag.Responsibles = responsibles;
            return View();
        }

        [HttpPost]

        public ActionResult CreateProtocol(Protocol protocol)
        {
            db.Protocols.Add(protocol);
            db.SaveChanges();

            return RedirectToAction("ProtocolsList");

        }

        [HttpGet]

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
                SelectList responsibles = new SelectList(db.Responsibles, "Id", "Name");
                ViewBag.Organizations = organizations;
                ViewBag.Responsibles = responsibles;
                return View(protocol);
            }
            return RedirectToAction("AssignmentsList");
        }

        [HttpPost]

        public ActionResult EditProtocol(Protocol protocol)
        {
            db.Entry(protocol).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ProtocolsList");
        }

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

        public ActionResult DetailsProtocol(int? id)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}