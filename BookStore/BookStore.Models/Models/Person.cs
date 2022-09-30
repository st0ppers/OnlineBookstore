using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class Person
    {
        //id name age datetime birotofa

        public int  Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthOfDate { get; set; }
    }
}
