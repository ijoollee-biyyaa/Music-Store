using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Music_Store.Models;

public  class AlbumTbl
{
    [Key]
    public int AlbumId { get; set; }

    public string AlbumTitle { get; set; } = null!;

    public virtual ICollection<SongTbl> SongTbls { get; set; } = new List<SongTbl>();
}
