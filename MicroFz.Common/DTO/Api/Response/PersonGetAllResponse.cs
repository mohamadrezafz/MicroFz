using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFz.Common.DTO.Api.Response
{
   public class PersonGetAllResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string NationalCode { get; set; }
        public long Id { get; set; }
    }
}
