﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }


        public ICollection<Stop> Stops { get; set; }
    }
}

