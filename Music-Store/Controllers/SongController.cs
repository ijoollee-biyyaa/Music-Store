using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;

namespace Music_Store.Controllers
{
    [Authorize]
    public class SongController : Controller
    {
        private readonly MusicDataContext _db;
        private readonly IWebHostEnvironment _webhostenvironment;
        public SongController(MusicDataContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webhostenvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<SongTbl> list = _db.Songs.
                Include(g => g.Genre)
               .Include(c => c.Album)
               .Include(a => a.Singer)
               .Include(b => b.Language)
                .ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> genreList = _db.Genres.ToList().Select(u => new SelectListItem
            {
                Text = u.GenreName,
                Value = u.GenreId.ToString()
            });
            ViewBag.GenreList = genreList;

            IEnumerable<SelectListItem> albumList = _db.Albums.ToList().Select(u => new SelectListItem
            {
                Text = u.AlbumTitle,
                Value = u.AlbumId.ToString()
            });
            ViewBag.AlbumList = albumList;

            IEnumerable<SelectListItem> singerList = _db.Singers.ToList().Select(u => new SelectListItem
            {
                Text = u.SingerName,
                Value = u.SingerId.ToString()
            });
            ViewBag.SingerList = singerList;

            IEnumerable<SelectListItem> languageList = _db.Languages.ToList().Select(u => new SelectListItem
            {
                Text = u.LanguageName,
                Value = u.LanguageId.ToString()
            });
            ViewBag.LanguageList = languageList;

            return View();
        }
        [HttpPost]
        public IActionResult Create(SongTbl songs, IFormFile? file, IFormFile? file1)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webhostenvironment.WebRootPath;
                if (file!= null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string imagePath = Path.Combine(wwwRootPath, @"Images/Songs");
                    using (var fileStream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                        songs.SongPicture = @"\Images\Songs\" + filename;
                }
                if(file1 != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file1.FileName);
                    string musicPath = Path.Combine(wwwRootPath, @"music");
                    using (var fileStream = new FileStream(Path.Combine(musicPath, filename), FileMode.Create))
                    {
                        file1.CopyTo(fileStream);
                    }
                    songs.SongFilepath = @"\music\" + filename;
                }
                    _db.Songs.Add(songs);
                _db.SaveChanges();
                TempData["Success"] = "Song Created Successfully";
                return RedirectToAction("Index");   
            }
            return View(songs);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null|| id == 0)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> genreList = _db.Genres.ToList().Select(u => new SelectListItem
            {
                Text = u.GenreName,
                Value = u.GenreId.ToString()
            });
            ViewBag.GenreList = genreList;

            IEnumerable<SelectListItem> albumList = _db.Albums.ToList().Select(u => new SelectListItem
            {
                Text = u.AlbumTitle,
                Value = u.AlbumId.ToString()
            });
            ViewBag.AlbumList = albumList;

            IEnumerable<SelectListItem> singerList = _db.Singers.ToList().Select(u => new SelectListItem
            {
                Text = u.SingerName,
                Value = u.SingerId.ToString()
            });
            ViewBag.SingerList = singerList;

            IEnumerable<SelectListItem> languageList = _db.Languages.ToList().Select(u => new SelectListItem
            {
                Text = u.LanguageName,
                Value = u.LanguageId.ToString()
            });
            ViewBag.LanguageList = languageList;

            SongTbl? songs = _db.Songs.Find(id);
            if(songs == null)
            {
                return NotFound();
            }
            return View(songs);


        }

        [HttpPost]
        public IActionResult Edit(SongTbl songs, IFormFile? file, IFormFile? file1)
        {
            if(ModelState.IsValid)
            {
                string wwwroothpath = _webhostenvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string imagepath = Path.Combine(wwwroothpath, @"Images/Songs");
                    if (!string.IsNullOrEmpty(songs.SongPicture))
                    {
                        var oldImagePath = Path.Combine(wwwroothpath, songs.SongPicture.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(imagepath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    songs.SongPicture = @"\Images\Songs\" + filename;
                }
                // for music update

                if (file1 != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file1.FileName);
                    string imagePath = Path.Combine(wwwroothpath, @"music");
                    if (!string.IsNullOrEmpty(songs.SongFilepath))
                    {
                        var oldSongPath = Path.Combine(wwwroothpath, songs.SongFilepath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldSongPath))
                        {
                            System.IO.File.Delete(oldSongPath);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create))
                    {
                        file1.CopyTo(filestream);
                    }
                    songs.SongFilepath= @"\music\" + filename;
                }

                var objFromDb = _db.Songs.FirstOrDefault(u => u.SongId == songs.SongId);
                if (objFromDb != null)
                {
                    objFromDb.SongId = songs.SongId;
                    objFromDb.SongName = songs.SongName;
                    objFromDb.SongDescription = songs.SongDescription;
                    objFromDb.SongRelease = songs.SongRelease;
                    objFromDb.AlbumId = songs.AlbumId;
                    objFromDb.GenreId = songs.GenreId;  
                    objFromDb.LanguageId = songs.LanguageId;    
                    objFromDb.SingerId = songs.SingerId;
                    if (songs.SongPicture != null)
                    {
                        objFromDb.SongPicture = songs.SongPicture;
                    }
                   else if(songs.SongFilepath != null)
                    {
                        objFromDb.SongFilepath = songs.SongFilepath;
                    }
                   
                    _db.SaveChanges();
                    TempData["Info"] = "Song Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            return View(songs);
        }

        public IActionResult Delete(int? id)
        {
            if(id== null || id == 0)
            {
                return NotFound();
            }
            SongTbl? songs = _db.Songs.Find(id);
            _db.Songs.Include(a => a.Genre)
                     .Include(b => b.Album)
                     .Include(a => a.Language)
                     .Include(c => c.Singer).ToList();
                
            if(songs == null)
            {
                return NotFound();
            }
            return View(songs);
            
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteSongs(int? id)
        {
            SongTbl? songs = _db.Songs.Find(id);
            if(songs == null)
            {
                return NotFound();
            }
            _db.Remove(songs);
            _db.SaveChanges();
            TempData["Error"] = "Song Deleted Successfully";
            return RedirectToAction("Index");
        }
        }

    }

