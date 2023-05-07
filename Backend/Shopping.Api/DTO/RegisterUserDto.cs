using System.ComponentModel.DataAnnotations;

namespace Shopping.Api.DTO
{
    public class RegisterUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(CurrentDate), ErrorMessage = "Date must not be older than the current date.")]
        public DateTime Birthday { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(Customer|Seller)$", ErrorMessage = "Role must be either 'Customer' or 'Seller'.")]
        public string Role { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public DateTime CurrentDate { get; set; } = DateTime.Now;

    }
}
