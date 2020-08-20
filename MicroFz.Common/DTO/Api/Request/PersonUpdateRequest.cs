using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFz.Common.DTO.Api.Request
{
   public class PersonUpdateRequest
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
