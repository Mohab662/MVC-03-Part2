using MVC_03.DAL.Models;
using System.Runtime.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_03.PL.ViewModels
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required!")]
        [MaxLength(50, ErrorMessage = "Max Length Is 50!")]
        [MinLength(3, ErrorMessage = "Min Length Is 3!")]
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Range(21, 60)]
        public int? Age { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public Gender Gender { get; set; }
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
    }
}
