using System.ComponentModel.DataAnnotations;

namespace GSMovieRental.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]

        [MinLength(4)]
        public string Name { get; set; }

        public short SignUpFee { get; set; }
        [Range(1, 12, ErrorMessage = "Enter number between 1 and 12 only.")]
        public byte DurationInMonths { get; set; }
        [Range(1, 50, ErrorMessage = "Discount should be between 1 and 50 .")]
        public byte DiscountRate { get; set; }

    }
}