using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrekSurfing.Web.Models
{
    public class TrekEvent : EntityBase
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime Starts { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime Ends { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser Owner { get; set; }

        [Required(ErrorMessage = "Route is required")]
        public string Route { get; set; }

        public byte[] Image { get; set; }

        public IEnumerable<Review> Reviews { get; set; }

        public bool Confirmed { get; set; }
    }
}