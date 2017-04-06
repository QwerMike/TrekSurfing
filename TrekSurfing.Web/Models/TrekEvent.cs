using System;
using System.ComponentModel.DataAnnotations;

namespace TrekSurfing.Web.Models
{
    public class TrekEvent
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime Starts { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime Ends { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public ApplicationUser Owner { get; set; }

        [Required(ErrorMessage = "Route is required")]
        public string Route { get; set; }

        public byte[] Image { get; set; }
    }
}