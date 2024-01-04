using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;

namespace Music_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class searchapiController : ControllerBase
    {
        private readonly MusicDataContext _music;
        public searchapiController(MusicDataContext music)
        {
                _music = music;
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task< IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                //var songs = from s in _music.Songs select s;
                //songs = songs.Where(s => s.SongName.Contains(term) || s.Singer.SingerName.Contains(term));
                //return Ok(songs);

                var songs = _music.Songs.Where(s => s.SongName.Contains(term) || s.Singer.SingerName.Contains(term)).Select(s => s.SongName ).ToList();
                  
                return Ok(songs);
            }
           catch
            {
                return BadRequest();
            }
                        
        }
    }
}
