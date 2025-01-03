﻿using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class AdminInputDTO
    {


        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } // Admin's first name

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; } // Admin's last name

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } // Contact number

        [Required(ErrorMessage = "Civil ID is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Civil ID must be between 8 and 20 characters.")]
        public string CivilId { get; set; } // Unique Civil ID

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } // Validated email

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public Address Address { get; set; } // Updated to use Enum
    }
}
