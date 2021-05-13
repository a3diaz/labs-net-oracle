using Labs.NET.Oracle.Domain.Data;
using Labs.NET.Oracle.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Infrastructure.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly LabsContext _context;
        public PersonRepository(LabsContext context)
        {
            _context = context;
        }
        public Task Insert(Person person)
        {
            _context.People.Add(person);
            return Task.CompletedTask;
        }

        public Task Insert(IEnumerable<Person> persons)
        {
            _context.People.AddRange(persons);
            return Task.CompletedTask;
        }

        public async Task<int> CountAll()
        {
            var dbCount = await _context.People
                .CountAsync();

            return dbCount;
        }

        public async Task<IList<Person>> SelectAll(int skip = 0, int take = 100)
        {
            var dbPeople = await _context.People
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Lastname)
                .ToListAsync();

            return dbPeople;
        }

        public async Task<Person> SelectFirst(Guid personId)
        {
            var dbPerson = await _context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonId == personId);

            return dbPerson;
        }

        public Task Delete(Guid personId)
        {
            _context.People.Remove(new Person { PersonId = personId });
            return Task.CompletedTask;
        }

        public Task Update(Person person)
        {
            _context.People.Update(person);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid personId)
        {
            var dbExists = await _context.People
                .AnyAsync(p => p.PersonId == personId);

            return dbExists;
        }
    }
}
