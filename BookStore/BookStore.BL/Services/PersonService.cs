using System.Net;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    // business layer
    public class PersonService : IPersonService
    {
        private readonly IPersonRepo _personRepository;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepo ipr, IMapper mapper)
        {
            _personRepository = ipr;
            _mapper = mapper;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _personRepository.GetAllPeople();
        }
        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public Person GetByName(string name)
        {
            return _personRepository.GetByName(name);
        }
        public AddPersonResponse AddPerson(AddPersonRequest personRequest)
        {
            if (_personRepository.GetByName(personRequest.Name) != null)
            {
                return new AddPersonResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = $"Bad request"
                };
            }

            var person = _mapper.Map<Person>(personRequest);
            var result = _personRepository.AddPerson(person);
            return new AddPersonResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Person = person,
            };
        }

        public AddPersonResponse UpdatePerson(AddPersonRequest personRequest)
        {
            if (_personRepository.GetByName(personRequest.Name) != null)
            {
                return new AddPersonResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = $"Bad request"
                };
            }

            var person = _mapper.Map<Person>(personRequest);
            var result = _personRepository.UpdatePerson(person);
            return new AddPersonResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Person = result,
            };
        }

        public Person DeletePerson(int personId)
        {
            return _personRepository.DeletePerson(personId);
        }

    }
}