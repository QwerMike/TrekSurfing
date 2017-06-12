using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; }

        [ForeignKey(nameof(TargetId))]
        public TrekEvent Target { get; set; }

        public string AuthorId { get; set; }
        public int TargetId { get; set; }
    }
}