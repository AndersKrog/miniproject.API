using System;

namespace miniproject.DAL.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public bool IsAlive { get; set; }
    }
}
