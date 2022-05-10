using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Database.Models;
using miniproject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject.DAL.Database
{
    public class AbContext: DbContext
    {
        public AbContext() { }
        public AbContext(DbContextOptions<AbContext> options) : base(options) { }        
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
