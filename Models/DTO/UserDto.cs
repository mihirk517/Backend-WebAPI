﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class UserDto
    {
        [Required, EmailAddress]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
