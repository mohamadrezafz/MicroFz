using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MicroFz.Data.Model
{
   public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string NationalCode { get; set; }

    }
}
