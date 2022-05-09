﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject.DAL.Database.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public int Wordcount { get; set; }
        public bool Binding { get; set; }
        public DateTime Releasedate { get; set; }
    }
}
