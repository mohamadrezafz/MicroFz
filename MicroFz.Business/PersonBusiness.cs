using CoreBase.Api.Response;
using MicroFz.Common.Constant;
using MicroFz.Common.DTO.Api.Response;
using MicroFz.Common.DTO.Business;
using MicroFz.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroFz.Business
{

    public interface IPersonBusiness
    {
        Task<GeneralBaseResponse> Create(PersonCreateBusiness data);
        Task<GeneralBaseResponse<List<PersonGetAllResponse>>> GetAll();
        Task<GeneralBaseResponse> Update(PersonUpdateBusiness data);
    }

    public class PersonBusiness : IPersonBusiness
    {
        private IMicroFzUnitOfWork unitOfWork;
        public PersonBusiness(IMicroFzUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GeneralBaseResponse> Create(PersonCreateBusiness data)
        {

            GeneralBaseResponse response = new GeneralBaseResponse();
            await unitOfWork.Person.InsertAsync(new Data.Model.Person
            {
                Age = data.Age,
                FirstName = data.FirstName,
                LastName = data.LastName,
                NationalCode = data.NationalCode

            });
            response.Result = FinalResult.Created;
            return response;
        }

        public async Task<GeneralBaseResponse> Update(PersonUpdateBusiness data)
        {
            GeneralBaseResponse response = new GeneralBaseResponse();

            var person = unitOfWork.Person.GetById(data.Id);
            if(person == null)
            {
                response.Result = FinalResult.NotFound;
                return response;
            }
            
            await unitOfWork.Person.UpdateAsync(new Data.Model.Person()
            {
                Id = data.Id ,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Age = person.Age ,
                NationalCode = person.NationalCode
            });
            response.Result = FinalResult.Ok;
            return response;
        }

        public async Task<GeneralBaseResponse<List<PersonGetAllResponse>>> GetAll()
        {
            GeneralBaseResponse<List<PersonGetAllResponse>> response = new GeneralBaseResponse<List<PersonGetAllResponse>>();
            var persons = await unitOfWork.Person.GetAllAsync();

            response.Data = persons.Select(x => new PersonGetAllResponse()
            {
                Id = x.Id ,
                Age = x.Age,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalCode = x.NationalCode

            }).ToList();

            response.Result = FinalResult.Ok;
            return response;
        }

    }
}
