using System;
using System.ComponentModel.DataAnnotations;

namespace GSMovieRental.Models
{
    public class Customer
    {

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public bool IsDeleted { get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [CustomAgeValidation]
        public DateTime? BirthDate { get; set; }

        public static byte Unknown = 0;
        public static byte PayAsYouGo = 1;

    }
}