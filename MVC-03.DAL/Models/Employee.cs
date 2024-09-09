using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male =1,
        [EnumMember(Value = "Female")]
        Female =2
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public int? Age { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; }
        public Gender Gender { get; set; }
    }
}
