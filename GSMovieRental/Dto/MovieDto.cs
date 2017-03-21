using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GSMovieRental.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Released On")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 10)]
        public decimal Rating { get; set; }

        [StringLength(1024, MinimumLength = 1)]
        public string ImageUrl { get; set; }


        public short CopiesAvailable { get; set; }

        [Range(0, 20)]
        public short CopiesInStock { get; set; }

    }
}