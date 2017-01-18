// ---------------------------------------------------------------------------
// File name:Project2.cs
// Project name:Movie Viewer
// ---------------------------------------------------------------------------
// Creator’s name and email: Stan Seiferth zsjs19@etsu.edu					
// Course-Section:CSCI-3110-001
//	Creation Date:	11/07/2016		
// ---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Models
{
    public class Profile
    {
        [Key]
        [Column(Order = 1)]
        public string Email { get; set; }
        [Key]
        [Column(Order = 2)]
        [Required]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        [MaxLength(2), MinLength(2)]
        public string State { get; set; }
        [Required]
        [MaxLength(5), MinLength(5)]
        public string Zip { get; set; }





    }
}