using miniproject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject.TEST
{
    public class DummyData
    {
        public static List<Author> Authorlist { get; set; } = new List<Author>()
            {
                new Author{Id = 1, Age = 12, Name="Hans", IsAlive=true, Password="1234"},
                new Author{Id = 2, Age = 12, Name="Gert", IsAlive=true, Password="1234"},
                new Author{Id = 3, Age = 12, Name="Gerda", IsAlive=true, Password="1234"},
                new Author{Id = 4, Age = 12, Name="Erik", IsAlive=true, Password="1234"}
            };

    }
}
