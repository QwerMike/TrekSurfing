﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrekSurfing.Web.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string About { get; set; }
        public byte[] Image { get; set; }

        public IEnumerable<TrekEvent> TrekEvents { get; set; }
    }
}