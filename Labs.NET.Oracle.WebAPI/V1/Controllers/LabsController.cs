using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Models;
using Labs.NET.Oracle.Domain.Results;
using Labs.NET.Oracle.WebAPI.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.WebAPI.V1.Controllers
{
    public class LabsController : ControllerBase
    {
        [NonAction]
        protected ActionResult Reply(OperationResult<Person> personResult)
        {
            SetHeaders(personResult.Status);
            return StatusCode(MapHttpStatusCode(personResult.Status), MapPersonResume(personResult.Out));
        }

        [NonAction]
        protected ActionResult Reply(IList<Person> personResult)
        {
            SetHeaders(OperationStatus.Success);
            return StatusCode(MapHttpStatusCode(OperationStatus.Success), 
                personResult.Select(p => MapPersonResume(p)));
        }

        private void SetHeaders(OperationStatus operationStatus)
        {
            Response.Headers.Add("x-operation-status", operationStatus.ToString("d"));
            Response.Headers.Add("x-operation-status-description", operationStatus.ToString("g"));
        }

        [NonAction]
        private int MapHttpStatusCode(OperationStatus operationStatus)
        {
            return operationStatus switch
            {
                OperationStatus.Success => (int)HttpStatusCode.OK,
                OperationStatus.PersonAdded => (int)HttpStatusCode.Created,
                OperationStatus.PersonNotFound => (int)HttpStatusCode.NotFound,
                OperationStatus.PersonRemoved => (int)HttpStatusCode.NoContent,
                OperationStatus.PersonUpdated => (int)HttpStatusCode.OK,
                OperationStatus.PhoneAssigned => (int)HttpStatusCode.OK,
                OperationStatus.PhoneDuplicated => (int)HttpStatusCode.Conflict,
                OperationStatus.PhoneNotFound => (int)HttpStatusCode.NotFound,
                OperationStatus.PhoneOwnerNotFound => (int)HttpStatusCode.BadRequest,
                OperationStatus.PhoneRemoved => (int)HttpStatusCode.NoContent,
                OperationStatus.PhoneUpdated => (int)HttpStatusCode.OK,
                _ => (int)HttpStatusCode.ServiceUnavailable
            };
        }

        [NonAction]
        private PersonResume MapPersonResume(Person person)
        {
            return new PersonResume
            {
                PersonId = person.PersonId,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                Lastname = person.Lastname,
                Name = person.Name
            };
        }
    }
}
