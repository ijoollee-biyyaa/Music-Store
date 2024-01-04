using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Music_Store.Models;

public partial class LanguageTbl
{
    [Key]
    public int LanguageId { get; set; }
    [Required]
    public string LanguageName { get; set; } = null!;

    public virtual ICollection<SongTbl> SongTbls { get; set; } = new List<SongTbl>();
}
