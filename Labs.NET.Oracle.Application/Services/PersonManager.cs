using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Data;
using Labs.NET.Oracle.Domain.Models;
using Labs.NET.Oracle.Domain.Results;
using Labs.NET.Oracle.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Application.Services
{
    public sealed class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PersonManager(IPersonRepository personRepository,
            IPhoneRepository phoneRepository,
            IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _phoneRepository = phoneRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<Person>> Add(string name, string lastname, Gender gender, DateTime birthDate)
        {
            var person = new Person
            {
                PersonId = Guid.NewGuid(),
                BirthDate = birthDate,
                Gender = gender,
                Name = name,
                Lastname = lastname
            };

            await _personRepository.Insert(person);
            await _unitOfWork.Save();

            return OperationResult.PersonAdded(person);
        }
        public async Task<Person> Get(Guid personId)
        {
            var person = await _personRepository
                .SelectFirst(personId);

            return person;
        }

        public async Task<IList<Person>> GetAll()
        {
            var person = await _personRepository
                .SelectAll();

            return person;
        }

        public async Task<OperationResult<Person>> Remove(Guid personId)
        {
            var exists = await _personRepository.Exists(personId);
            if (!exists)
                return OperationResult.PersonNotFound();

            var phones = await _phoneRepository.SelectAll(p => p.OwnerId == personId);
            foreach (var phone in phones)
                await _phoneRepository.Delete(phone.PhoneId);

            await _personRepository.Delete(personId);
            await _unitOfWork.Save();

            return OperationResult.PersonRemoved();
        }
        public async Task<OperationResult<Person>> Update(Guid personId, string newName = null, string newLastname = null,
            Gender? newGender = null, DateTime? newBirthDate = null)
        {
            var person = await _personRepository
                .SelectFirst(personId);

            if (person is null)
                return OperationResult.PersonNotFound();

            person.Name = newName ?? person.Name;
            person.Lastname = newLastname ?? person.Lastname;
            person.Gender = newGender ?? person.Gender;
            person.BirthDate = newBirthDate ?? person.BirthDate;

            await _personRepository.Update(person);
            await _unitOfWork.Save();

            return OperationResult.PersonUpdated(person);
        }
    }
}
