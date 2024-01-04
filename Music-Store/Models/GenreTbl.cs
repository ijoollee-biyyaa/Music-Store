using System;
using System.Collections.Generic;

namespace Music_Store.Models;

public partial class GenreTbl
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<SongTbl> SongTbls { get; set; } = new List<SongTbl>();
}
