﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CircleCompetitions.Models
{
    public class Competition
    {
        [Key]
        public int ID_Competition { get; set; }
        public string NameOfCompetition { get; set; }
        public string Place { get; set; }
        public string TypeOfCompetition { get; set; }
        public int StageCount { get; set; }
        public int AgeLimit { get; set; }
        public int PartLimit { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
    }
}
