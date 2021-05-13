using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.NET.Oracle.Domain.Results
{
    public interface IOperationResult<T>
    {
        public OperationStatus Status { get; set; }
        public T @Out { get; }
    }

    public class OperationResult<T> : IOperationResult<T>
    {
        public OperationStatus Status { get; set; }
        public T @Out { get; private set; }

        public OperationResult(OperationStatus status, T @out)
        {
            Status = status;
            Out = @out;
        }
    }

    public static class OperationResult
    {
        public static OperationResult<Person> PersonAdded(Person @out) => new(OperationStatus.PersonAdded, @out);
        public static OperationResult<Person> PersonNotFound() => new(OperationStatus.PersonNotFound, default);
        public static OperationResult<Person> PersonUpdated(Person @out) => new(OperationStatus.PersonUpdated, @out);
        public static OperationResult<Person> PersonRemoved() => new(OperationStatus.PersonRemoved, default);
        public static OperationResult<Phone> PhoneAssigned(Phone @out) => new(OperationStatus.PhoneAssigned, @out);
        public static OperationResult<Phone> PhoneNotFound() => new(OperationStatus.PhoneNotFound, default);
        public static OperationResult<Phone> PhoneOwnerNotFound() => new(OperationStatus.PhoneOwnerNotFound, default);
        public static OperationResult<Phone> PhoneUpdated(Phone @out) => new(OperationStatus.PhoneUpdated, @out);
        public static OperationResult<Phone> PhoneRemoved() => new(OperationStatus.PhoneRemoved, default);
        public static OperationResult<Phone> PhoneDuplicated() => new(OperationStatus.PhoneDuplicated, default);
    }
}
