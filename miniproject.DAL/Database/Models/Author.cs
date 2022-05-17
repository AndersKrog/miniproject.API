using miniproject.DAL.Database.Models;
using miniproject.DAL.repository;
using System;
using System.Collections.Generic;

namespace miniproject.DAL.Models
{
    public class Author : Ientity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public bool IsAlive { get; set; }
        public List<Book> Books { get; set; }
    }
}
