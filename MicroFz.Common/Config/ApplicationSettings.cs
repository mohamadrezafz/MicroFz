using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFz.Common.Config
{
     public class Settings
    {
        public DataBaseSettings DatabaseSettings { get; set; }
        public ExternalService ExternalService { get; set; }
    }
    public class DataBaseSettings
    {
        public string Connection { get; set; }
    }
    public class ExternalService
    {
    }

}
