using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Passenger")]
    public class Passenger:Person
    {
        public DateTime CustomerSince { get; set; }
        public string Status { get; set; }
    }
}
