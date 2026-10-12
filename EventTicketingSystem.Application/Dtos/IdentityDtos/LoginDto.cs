using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.IdentityDtos
{
    public sealed record LoginDto
    {
        [EmailAddress]
        [Required(ErrorMessage ="Email is required to login")]
        public string Email { get; set; } = default!;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required to login")]
        public string Password { get; set; } = default!;
    }
}
