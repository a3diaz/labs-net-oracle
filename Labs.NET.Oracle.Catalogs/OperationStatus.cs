using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.NET.Oracle.Catalogs
{
    public enum OperationStatus
    {
        Success,
        PersonAdded,
        PersonNotFound,
        PersonUpdated,
        PersonRemoved,
        PhoneAssigned,
        PhoneNotFound,
        PhoneOwnerNotFound,
        PhoneUpdated,
        PhoneRemoved,
        PhoneDuplicated,
    }
}
