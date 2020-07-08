using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDTS_Test_07_01.Services
{
    public class UserPreference
    {
        public string LanguagePreference { get; set; }

        public string Culture { get; set; }

        public int CurrentPageIndex { get; set; } = 0;
    }
}
