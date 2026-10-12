using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.IdentityDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Display name must be between 3 and 50 characters.")]
        public string DisplayName { get; set; } = default!;
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Phone number is required.")]

        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$",
            ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; } = default!;
    }
}
