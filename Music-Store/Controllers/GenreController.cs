using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music_Store.Data;
using Music_Store.Models;

namespace Music_Store.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly MusicDataContext _music;
        public GenreController(MusicDataContext music)
        {
            _music = music;
        }
        public IActionResult Index()
        {
            List<GenreTbl> genre = _music.Genres.ToList();
            return View(genre);
        }
        public IActionResult Create()
        {
            return View();
        }
        //public IActionResult searchsonng( String name)

        //{
        //    List<SongTbl> temp= _music.Songs.Where(x=>x.SongName.Contains(name)).ToList();
           
        //    return View();
        //}

        [HttpPost]
        public IActionResult Create(GenreTbl genres)
        {
            if (ModelState.IsValid)
            {
                _music.Genres.Add(genres);
                _music.SaveChanges();
                TempData["Success"] = "Genre Created Successfully";
                return RedirectToAction("Index");
            }
            return View(genres);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            GenreTbl? genre = _music.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();

            }
            return View(genre);
        }
        [HttpPost]
        public IActionResult Edit(GenreTbl genre)
        {
            if (ModelState.IsValid)
            {
                _music.Genres.Update(genre);
                _music.SaveChanges();
                TempData["Info"] = "Genre Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        public IActionResult Delete(int? id)
        {
            GenreTbl? genre = _music.Genres.Find(id);
            if (genre == null)
            {

                return NotFound();
            }
            return View(genre);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            GenreTbl? genre = _music.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }
            _music.Remove(genre);
            _music.SaveChanges();
            TempData["Error"] = "Genre Deleted Successfully";
            return RedirectToAction("Index");
        }




    }
}
