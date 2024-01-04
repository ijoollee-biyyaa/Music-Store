using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Music_Store.Data;
using Music_Store.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Music_Store.Controllers
{
    
    
    public class AlbumController : Controller
    {
        private readonly MusicDataContext _music;

        public AlbumController(MusicDataContext music)
        {
            _music = music;
        }
        public IActionResult Index()
        {
            List<AlbumTbl> albums = _music.Albums.ToList();
           

            return View(albums);
        }

        public IActionResult AlbumShow()
        {
            
        List<AlbumTbl> album = _music.Albums.
                Include(a=>a.SongTbls).ThenInclude(b=>b.Singer)
                .ToList();
              
            return View(album);
        }

        public IActionResult SongList(int? id)
        {
           if(id.Value == null || id== 0)
            {
                return NotFound();
            }

         var album = _music.Songs.Where(x => x.AlbumId ==id).
                Include(a=>a.Album).
                Include(s=>s.Singer).
                Include(l=>l.Language).
               
                ToList();
              if (album == null)
                {
                   return NotFound();
                }

            return View(album);

        }

        [Authorize]
        public IActionResult Create()
        {
         
            return View();
        }
        [HttpPost]
        public IActionResult Create(AlbumTbl album)
        {
            if (ModelState.IsValid)
            {
                _music.Albums.Add(album);
                _music.SaveChanges();
                TempData["Success"] = "Album Created Successfully";
                return RedirectToAction("Index");
            }
            return View(album);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            AlbumTbl? album = _music.Albums.Find(id);
            if (album == null)
            {
                return NotFound();

            }

                  

            return View(album);
        }
        [HttpPost]
        public IActionResult Edit(AlbumTbl album)
        {
            if (ModelState.IsValid)
            {
                _music.Albums.Update(album);
                _music.SaveChanges();
                TempData["Info"] = "Album Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(album);
        }

        public IActionResult Delete(int? id)
        {
            AlbumTbl? album = _music.Albums.Find(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            AlbumTbl? album1 = _music.Albums.Find(id);
            if (album1 == null)
            {
                return NotFound();
            }
            _music.Remove(album1);
            _music.SaveChanges();
            TempData["Error"] = "Album Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
