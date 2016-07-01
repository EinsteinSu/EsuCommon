using System.Net;
using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.Model;

namespace Management.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionManager _manager;

        public QuestionController()
        {
            _manager = new QuestionManager();
        }

        public QuestionController(IQuestionManager manager)
        {
            this._manager = manager;
        }

        // GET: Question
        public ActionResult Index()
        {
            return View(_manager.SelectList());
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _manager.SelectById(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionId,Type,Content,Score,CorrectAnswer")] Question question)
        {
            if (ModelState.IsValid)
            {
                _manager.Create(question);
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _manager.SelectById(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionId,Type,Content,Score,CorrectAnswer")] Question question)
        {
            if (ModelState.IsValid)
            {
                _manager.Modify(question);
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _manager.SelectById(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _manager.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _manager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}