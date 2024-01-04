using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music_Store.Data;
using Music_Store.Models;

namespace Music_Store.Controllers
{
    [Authorize]
    public class LanguageController : Controller
    {
        private readonly MusicDataContext _db;
        public LanguageController(MusicDataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<LanguageTbl> list = _db.Languages.ToList();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LanguageTbl language)
        {
            if (ModelState.IsValid)
            {
                _db.Languages.Add(language);
                _db.SaveChanges();
                TempData["Success"] = "Language Created Successfully";
                return RedirectToAction("Index");
            }
            return View(language);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            LanguageTbl? language1 = _db.Languages.Find(id);
            if (language1 == null)
            {
                return NotFound();
            }
            return View(language1);
        }
        [HttpPost]
        public IActionResult Edit(LanguageTbl language)
        {

            if (ModelState.IsValid)
            {
                _db.Languages.Update(language);
                _db.SaveChanges();
                TempData["Info"] = "Language Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(language);
        }

        public IActionResult Delete(int? id)
        {
            LanguageTbl? languages = _db.Languages.Find(id);
            if (languages == null)
            {
                return NotFound();
            }
            return View(languages);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            LanguageTbl? language = _db.Languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }
            _db.Remove(language);
            _db.SaveChanges();
            TempData["Error"] = "Language Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
