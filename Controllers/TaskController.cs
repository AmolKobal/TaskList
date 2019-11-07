using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class TaskController : Controller
    {
        TaskContext db = new TaskContext();

        // GET: Display list of Tasks
        public ActionResult Index()
        {
            IEnumerable<Task> tasks = db.Tasks.ToList();

            return View(tasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Create new Task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task)
        {
            task.StartDate = DateTime.Now;
            task.CompleteDate = task.StartDate.AddDays(7);
            db.Tasks.Add(task);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Save task to Database
        public ActionResult Save()
        {

            return RedirectToAction("Index");
        }

        public ActionResult Complete()
        {
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            Task task = db.Tasks.FirstOrDefault(t => t.ID == Id);
            if (task != null)
            {
                return View(task);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task, string Edit)
        {
            if (task == null)
            {
                return RedirectToAction("Index");
            }

            switch (Edit)
            {
                case "Update":

                    Task temp = db.Tasks.FirstOrDefault(t => t.ID == task.ID);
                    if (temp != null)
                    {
                        temp.Name = task.Name;
                        temp.Complete = task.Complete;
                        if (task.Complete)
                            task.CompleteDate = DateTime.Now;

                        db.Entry(temp).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();
                    }
                    break;
                case "Cancel":
                    // Do Nothing
                    break;
            }

            return RedirectToAction("View", new { id = task.ID });
        }

        [HttpGet]
        public ActionResult View(int? id)
        {
            Task task = db.Tasks.FirstOrDefault(t => t.ID == id);

            if (task == null)
                return View("NotFound");

            return View(task);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Task task = db.Tasks.FirstOrDefault(t => t.ID == id);
            if (task == null)
                return View("NotFound");
            else
            {
                db.Entry(task).State = System.Data.Entity.EntityState.Deleted;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Cancel(Task task)
        {
            return RedirectToAction("View", task);
        }
    }
}