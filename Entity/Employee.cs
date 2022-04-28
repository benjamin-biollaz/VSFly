using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Employee")]
    public class Employee:Person
    {
        public DateTime HireDate { get; set; }
        public string PassportNumber { get; set; }
        public int Salary { get; set; }
        public Employee Supervisor { get; set; }
    }
}
