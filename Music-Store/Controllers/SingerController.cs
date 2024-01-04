using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Music_Store.Data;
using Music_Store.Models;


namespace Music_Store.Controllers
{
    [Authorize]
    public class SingerController : Controller
    {

        private readonly MusicDataContext _music;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SingerController(MusicDataContext music, IWebHostEnvironment webHostEnvironment)
        {
            _music = music;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            IEnumerable<SingerTbl> singer = _music.Singers.ToList();
            return View(singer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SingerTbl singer, IFormFile file)
        {

            if (ModelState.IsValid)
            {

                

                string wwwroothpath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string imagepath = Path.Combine(wwwroothpath, @"Images/Singer");
                    using (var filestream = new FileStream(Path.Combine(imagepath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    singer.ProfilePicture = @"\Images\Singer\" + filename;
                }
                _music.Singers.Add(singer);
                _music.SaveChanges();
                TempData["Success"] = "Singer Created Successfully";
                return RedirectToAction("Index");
            }
            return View(singer);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            SingerTbl? singer = _music.Singers.Find(id);
            if (singer == null)
            {
                return NotFound();
            }
            return View(singer);
        }



        [HttpPost]
        public IActionResult Edit(SingerTbl singers, IFormFile? file1)
        {
            if (ModelState.IsValid)
            {
                string wwwroothpath = _webHostEnvironment.WebRootPath;
                if (file1 != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file1.FileName);
                    string imagepath = Path.Combine(wwwroothpath, @"Images/Singer");
                    if (!string.IsNullOrEmpty(singers.ProfilePicture))
                    {
                        var oldImagePath = Path.Combine(wwwroothpath, singers.ProfilePicture.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(imagepath, filename), FileMode.Create))
                    {
                        file1.CopyTo(filestream);
                    }
                    singers.ProfilePicture = @"\Images\Singer\" + filename;
                }


                var objFromDb = _music.Singers.FirstOrDefault(u => u.SingerId == singers.SingerId);
                if (objFromDb != null)
                {
                  objFromDb.SingerId = singers.SingerId;
                    objFromDb.SingerName = singers.SingerName;
                    objFromDb.SingerBio = singers.SingerBio;
                    if (singers.ProfilePicture!= null)
                    {
                        objFromDb.ProfilePicture = singers.ProfilePicture;
                    }

                    _music.SaveChanges();
                    TempData["Info"] = "Singer Updated Successfully";

                }

               
                return RedirectToAction("Index");
            }
            return View(singers);
        }

        public IActionResult Delete(int? id)
        {
            SingerTbl? singer = _music.Singers.Find(id);
            if (singer == null)
            {
                return NotFound();
            }
            return View(singer);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            SingerTbl? singer = _music.Singers.Find(id);
            if (singer == null)
            {
                return NotFound();
            }
            _music.Remove(singer);
            _music.SaveChanges();
            TempData["Error"] = "Singer Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
