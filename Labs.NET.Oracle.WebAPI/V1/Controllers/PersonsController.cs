using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Services;
using Labs.NET.Oracle.WebAPI.V1.Requests;
using Labs.NET.Oracle.WebAPI.V1.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.WebAPI.V1.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : LabsController
    {
        private readonly IPersonManager _personManager;

        public PersonsController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        [HttpPost]
        public async Task<ActionResult<PersonResume>> Add([FromBody] AddPerson request)
        {
            var operationResult = await _personManager.Add(request.Name, request.Lastname, request.Gender, request.BirthDate);
            return Reply(operationResult);
        }

        [HttpGet]
        public async Task<ActionResult<PersonResume>> Get()
        {
            var persons = await _personManager.GetAll();
            return Reply(persons);
        }
    }
}
