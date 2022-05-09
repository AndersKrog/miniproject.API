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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-DLG4VOAV;Database=abdb2; Trusted_Connection=True");
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
