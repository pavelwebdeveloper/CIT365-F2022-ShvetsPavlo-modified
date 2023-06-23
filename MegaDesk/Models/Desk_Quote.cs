using System.ComponentModel.DataAnnotations;

namespace MegaDesk.Models
{
    public class Desk_Quote
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Customer Name")]
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Desk Width")]
        [Range(24, 96)]
        [Required]
        public int DeskWidth { get; set; }

        [Display(Name = "Desk Depth")]
        [Range(12, 48)]
        [Required]
        public int DeskDepth { get; set; }

        [Display(Name = "Surface Material")]
        [Required]
        public string SurfaceMaterial { get; set; } = string.Empty;

        [Display(Name = "Number Of Drawers")]
        [Range(0, 7)]
        [Required]
        public int NumberOfDrawers { get; set; }

        [Display(Name = "Rush Order Days")]
        public int? ProductionNumberOfDays { get; set; }

        [Display(Name = "Quote Date")]
        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; }

        public decimal Price { get; set; }



        

    }
}
