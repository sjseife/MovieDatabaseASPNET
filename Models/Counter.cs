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
    public class Counter
    {
        [Key]
        public string UserEmail { get; set; }

        public int MovieCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double MoneySpent { get; set; }

    }
}