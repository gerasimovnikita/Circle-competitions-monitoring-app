﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CircleCompetitions.Models
{
    public class User
    {
        [Key]
        public int ID_User { get; set; }
        public string UserName { get; set; }
        public string Nickname { get; set; }
        public string E_Mail { get; set; }
        public string Password { get; set; }
        public string RightsOfUser { get; set; }
    }
}
