using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Music_Store.Models;
using Music_Store.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Music_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicDataContext _music; 
        public HomeController(ILogger<HomeController> logger, MusicDataContext music)
        {
            _logger = logger;
            _music = music;
        }

        public IActionResult Index(string term = "")
        {
            var songs = from s in _music.Songs select s;
            var albums = from a in _music.Albums select a;
           
            if (!string.IsNullOrEmpty(term))
            {
                songs = songs.Where(s => s.SongName.Contains(term)
                                        || s.Singer.SingerName.Contains(term)
                                         || s.Language.LanguageName.Contains(term)
                                         || s.SongRelease.Contains(term)
                                         || s.Album.AlbumTitle.Contains(term));

               albums = albums.Where(a => a.AlbumTitle.Contains(term));
            }
            AlbumSongVm asvm = new AlbumSongVm
            {
                Song = songs.Include(a=>a.Singer).ToList(),
                Album = albums.Include(a=>a.SongTbls).ThenInclude(a=>a.Singer). ToList()
            };
            return View(asvm);
                //return View(songs.Include(a=>a.Singer).ToList());
            
            }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

}
