using System.ComponentModel.DataAnnotations;

namespace TeamManager.Database.Models
{
    public class EmployeeContact
    {
        [Key]
        public int EmpContactId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        // Employee One-One
        public int EmployeeId { get; set; }
    }
}
