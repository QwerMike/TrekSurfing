using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrekSurfing.Web.Models
{
    public class Notification : EntityBase
    {
        [Required(ErrorMessage = "Receiver is required")]
        public string ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; }

        public string Message { get; set; }
    }
}