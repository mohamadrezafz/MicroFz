using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBase.Api;
using MicroFz.Business;
using MicroFz.Common.DTO.Api.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroFz.Api.Controllers.v1_0
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PersonController : BaseApiController
    {
        private readonly IPersonBusiness personBusiness;
        public PersonController(IPersonBusiness personBusiness)
        {
            this.personBusiness = personBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PersonCreateRequest request)
        {
            var response =await personBusiness.Create(new Common.DTO.Business.PersonCreateBusiness()
            {
                Age = request.Age ,
                NationalCode = request.NationalCode ,
                LastName = request.LastName ,
                FirstName  = request.FirstName
            });
            return GeneralResponse(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonUpdateRequest request)
        {
            var response = await personBusiness.Update(new Common.DTO.Business.PersonUpdateBusiness()
            {
                Id = request.Id ,
                LastName = request.LastName,
                FirstName = request.FirstName
            });
            return GeneralResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await personBusiness.GetAll();
            return GeneralResponse(response);
        }
    }
}
