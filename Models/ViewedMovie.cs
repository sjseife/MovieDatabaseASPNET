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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class ViewedMovie
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        public int LengthInMinutes { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price cannot be {1}")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }
        [Required]
        [Url]
        public string IMDBUrl { get; set; }
        public List<string> MovieTags { get; set; }
    }
}