﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Reg_User.Models
{
    public class UserClass
    {
        [Required(ErrorMessage = "Enter First Name!")]
        [Display(Name = "First Name:")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only text is allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name!")]
        [Display(Name = "Last Name:")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only text is allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Select Date of Birth!")]
        [Display(Name = "Date of Birth:")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name ="Gender:")]
        [Required(ErrorMessage = "Select Gender!")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Phone Number!")]
        [Display(Name = "Phone Number:")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Please Enter The Email Address !")]
        [Display(Name ="Email Id:")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "Enter The Address !")]
        [Display(Name = "Address :")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter The State !")]
        [Display(Name = "State :")]
        public string State { get; set; }

        [Required(ErrorMessage = "Enter The City !")]
        [Display(Name = "City :")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter The Username !")]
        [Display(Name = "Username :")]
        public string Username { get; set; }


        [Required(ErrorMessage ="Enter The Password !")]
        [Display(Name ="Password :")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password!")]
        [Display(Name = "Confirm Password:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Upload Profile Image")]
        [Display(Name ="Profile Image:")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,gif", ErrorMessage = "Only JPEG and GIF images are allowed.")]
        public byte[] Uimg { get; set; }


    }
}