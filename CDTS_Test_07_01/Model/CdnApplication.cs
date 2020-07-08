using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDTS_Test_07_01.Model
{
    public class CdnApplication
    {
        public PersonalInformation PersonalInformation { get; set; }
        public Address Address { get; set; }

        public CdnApplication()
        {
            PersonalInformation = new PersonalInformation();
            Address = new Address();
        }
    }
}
