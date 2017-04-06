using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrekSurfing.Web.Models
{
    public class Review : EntityBase
    {
        [Required(ErrorMessage = "Score is required")]
        [Range(0, 1)]
        public double Score { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public ApplicationUser Author { get; set; }

        public ApplicationUser Target { get; set; }
    }
}