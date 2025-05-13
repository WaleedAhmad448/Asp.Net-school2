using System;
using System.ComponentModel.DataAnnotations;

namespace AspNet_school2.Models
{

    public class Administrator
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string AdministratorNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public string ImagePath { get; set; }
    }

}   