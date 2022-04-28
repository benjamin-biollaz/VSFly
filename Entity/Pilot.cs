using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Pilot")]
    public class Pilot:Employee
    {
        public int FlightHours { get; set; }
        public string FlightSchool { get; set; }
        public DateTime LicenseDate { get; set; }
    }
}
