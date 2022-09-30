using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.InMemoryRepositories
{
    public class PersonRepo : IPersonRepo
    {
        public Guid Id { get; set; }

        private static List<Person?> _persons = new List<Person?>()
        {
            new Person()
            {
                Id = 1,
                Name = "pesho",
                Age = 20,
            },
            new Person()
            {
                Id = 2,
                Name = "gosho",
                Age = 24,
            },
            new Person()
            {
                Id = 3,
                Name = "tosho",
                Age = 31,
            }
        };

        public Person GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Person AddUser(Person person)
        {
            throw new NotImplementedException();
        }

        public Person UpdateUser(Person person)
        {
            throw new NotImplementedException();
        }

        public Person DeleteUser(int personId)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuidId()
        {
            throw new NotImplementedException();
        }
    }
}
