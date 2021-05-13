using Labs.NET.Oracle.Application.Extensions;
using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Data;
using Labs.NET.Oracle.Domain.Models;
using Labs.NET.Oracle.Domain.Results;
using Labs.NET.Oracle.Domain.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Application.Services
{
    public sealed class PhoneManager : IPhoneManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly ILogger<PhoneManager> _logger;

        public PhoneManager(IUnitOfWork unitOfWork,
            IPersonRepository personRepository, 
            IPhoneRepository phoneRepository,
            ILogger<PhoneManager> logger)
        {
            _personRepository = personRepository;
            _phoneRepository = phoneRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult<Phone>> Assign(string number, Guid ownerId, PhoneType type)
        {
            _logger.LogInformation($"Assingning phone {number.MaskPhoneNumber()} to person {ownerId}.");
            var personExists = await _personRepository.Exists(ownerId);
            if (!personExists)
            {
                _logger.LogWarning($"There is not any person with id {ownerId}.");
                return OperationResult.PhoneOwnerNotFound();
            }
                
            var phone = await _phoneRepository.SelectFirst(p => p.Number == number.Trim());
            if(phone is null)
            {
                phone = new Phone
                {
                    PhoneId = Guid.NewGuid(),
                    OwnerId = ownerId,
                    Number = number,
                    Type = type
                };

                _logger.LogDebug($"Inserting phone with id {phone.PhoneId}.");
                await _phoneRepository.Insert(phone);
            }
            else
            {
                phone.OwnerId = ownerId;
                phone.Type = type;

                _logger.LogDebug($"Phone already exists. Updating phone with id {phone.PhoneId}.");
                await _phoneRepository.Update(phone);
            }

            await _unitOfWork.Save();
            _logger.LogInformation("Phone assigned.");
            return OperationResult.PhoneAssigned(phone);
        }

        public async Task<IList<Phone>> Get(Guid personId)
        {
            var phones = await _phoneRepository.SelectAll(p => p.OwnerId == personId);
            return phones;
        }

        public async Task<OperationResult<Phone>> Remove(Guid phoneId)
        {
            _logger.LogInformation($"Removing phone with id {phoneId}.");
            var exists = await _phoneRepository.Exists(phoneId);
            if (!exists)
            {
                _logger.LogWarning($"There is not any phone with id {phoneId}.");
                return OperationResult.PhoneNotFound();
            }
                
            await _phoneRepository.Delete(phoneId);
            await _unitOfWork.Save();

            _logger.LogInformation($"Phone removed.");
            return OperationResult.PhoneRemoved(); 
        }

        public async Task<OperationResult<Phone>> Update(Guid phoneId, string newNumber = null, Guid? newOwnerId = null,
            PhoneType? newType = null)
        {
            _logger.LogInformation($"Removing phone with id {phoneId}.");
            var phone = await _phoneRepository.SelectFirst(p => p.PhoneId == phoneId);
            if (phone is null)
            {
                _logger.LogWarning($"There is not any phone with id {phoneId}.");
                return OperationResult.PhoneNotFound();
            }
                
            phone.Number = newNumber ?? phone.Number;
            phone.OwnerId = newOwnerId ?? phone.OwnerId;
            phone.Type = newType ?? phone.Type;

            await _phoneRepository.Update(phone);
            await _unitOfWork.Save();

            _logger.LogInformation($"Phone updated.");
            return OperationResult.PhoneUpdated(phone);
        }
    }
}
