using Labs.NET.Oracle.Domain.Data;
using Labs.NET.Oracle.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Infrastructure.Data
{
    public sealed class PhoneRepository : IPhoneRepository
    {
        private readonly LabsContext _context;
        public PhoneRepository(LabsContext context)
        {
            _context = context;
        }
        public Task Insert(Phone phone)
        {
            _context.Phones.Add(phone);
            return Task.CompletedTask;
        }

        public Task Delete(Guid phoneId)
        {
            _context.Phones.Remove(new Phone { PhoneId = phoneId });
            return Task.CompletedTask;
        }

        public async Task<IList<Phone>> SelectAll(Func<Phone, bool> filter)
        {
            var dbPhones = await _context.Phones
                .AsNoTracking()
                .Where(p => filter(p))
                .OrderBy(p => p.Number)
                .ToListAsync();

            return dbPhones;
        }

        public async Task<Phone> SelectFirst(Func<Phone, bool> filter)
        {
            var dbPhones = await _context.Phones
               .AsNoTracking()
               .FirstOrDefaultAsync(p => filter(p));

            return dbPhones;
        }

        public Task Update(Phone phone)
        {
            _context.Phones.Update(phone);
            return Task.CompletedTask;
        }

        public Task Update(IEnumerable<Phone> phone)
        {
            _context.Phones.UpdateRange(phone);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(string number)
        {
            var dbExists = await _context.Phones
                .AnyAsync(p => p.Number == number);

            return dbExists;
        }

        public async Task<bool> Exists(Guid phoneId)
        {
            var dbExists = await _context.Phones
                .AnyAsync(p => p.PhoneId == phoneId);

            return dbExists;
        }
    }
}
