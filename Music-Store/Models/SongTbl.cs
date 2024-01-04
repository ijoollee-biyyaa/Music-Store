using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music_Store.Models;

public  class SongTbl
{
    [Key]
    public int SongId { get; set; }
    [Required]
    public string SongName { get; set; } = null!;
    [Required]
    public string SongDescription { get; set; } = null!;
    [Required]
    public string SongRelease { get; set; } = null!;
    [DisplayName("Song Picture *")]
    public string? SongPicture { get; set; }

    //[NotMapped]
    //public IFormFile ImageFile { get; set; }
    [DisplayName(" Song File *")]
    public string? SongFilepath { get; set; }
    [Required]
    public int GenreId { get; set; }
    [Required]
    public int AlbumId { get; set; }
    [Required]
    public int SingerId { get; set; }
    [Required]
    public int LanguageId { get; set; }
    [ValidateNever]
    public virtual AlbumTbl Album { get; set; } = null!;
    [ValidateNever]
    public virtual GenreTbl Genre { get; set; } = null!;
    [ValidateNever]
    public virtual LanguageTbl Language { get; set; } = null!;
   [ValidateNever]
    public virtual SingerTbl Singer { get; set; } = null!;

   
}
