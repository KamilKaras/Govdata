using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Firmy
    {
        public string Nazwa { get; set; }
        public string DataRozpoczecia { get; set; }
        public string Status { get; set; }
        public Owner Wlasciciel { get; set; }
        public CompanyAdress AdresDzialalnosci { get; set; }

    }
}
