using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Music_Store.Models;

public partial class SingerTbl
{
    [Key]
    public int SingerId { get; set; }
    [Required]
    public string SingerName { get; set; } = null!;
    [Required]
    public string SingerBio { get; set; } = null!;
    public string? ProfilePicture { get; set; }

   

    public virtual ICollection<SongTbl> SongTbls { get; set; } = new List<SongTbl>();
}
