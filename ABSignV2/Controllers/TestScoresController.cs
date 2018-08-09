using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ABSignV2;
using Microsoft.AspNet.Identity;

namespace ABSignV2.Controllers
{
    public class TestScoresController : Controller
    {
        private AbSignV2Entities db = new AbSignV2Entities();

        [HttpPost]
        public JsonResult SaveScore(TestScore testScore)
        {
            var user = HttpContext.User.Identity.GetUserId();
            var proID = (from key in db.Profiles
                         where key.UserName == user
                         select key.ProfileID).Single();
            testScore.ProfileID = proID;

            db.TestScores.Add(testScore);
            db.SaveChanges();
            return Json(testScore.Score);
        }

        public ActionResult QuizView()
        {
            var testScores = db.TestScores.Include(t => t.Profile);
            return View(testScores.ToList());
        }

        // GET: TestScores
        public ActionResult Index()
        {
            var testScores = db.TestScores.Include(t => t.Profile);
            return View(testScores.ToList());
        }

        // GET: TestScores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestScore testScore = db.TestScores.Find(id);
            if (testScore == null)
            {
                return HttpNotFound();
            }
            return View(testScore);
        }

        // GET: TestScores/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName");
            return View();
        }

        // POST: TestScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestScoreID,Score,ProfileID")] TestScore testScore)
        {
            if (ModelState.IsValid)
            {
                db.TestScores.Add(testScore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", testScore.ProfileID);
            return View(testScore);
        }

        // GET: TestScores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestScore testScore = db.TestScores.Find(id);
            if (testScore == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", testScore.ProfileID);
            return View(testScore);
        }

        // POST: TestScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestScoreID,Score,ProfileID")] TestScore testScore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testScore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", testScore.ProfileID);
            return View(testScore);
        }

        // GET: TestScores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestScore testScore = db.TestScores.Find(id);
            if (testScore == null)
            {
                return HttpNotFound();
            }
            return View(testScore);
        }

        // POST: TestScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestScore testScore = db.TestScores.Find(id);
            db.TestScores.Remove(testScore);
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
