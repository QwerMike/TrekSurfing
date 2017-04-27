using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrekSurfing.Web.Models
{
    public class EventCreationModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime Starts { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime Ends { get; set; }

        public String Image { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Route is required")]
        public string Route { get; set; }
    }
}