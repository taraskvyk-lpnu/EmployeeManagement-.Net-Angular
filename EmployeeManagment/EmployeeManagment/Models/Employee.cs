using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Models;

using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 32 characters")]
    public string FirstName { get; set; }

    [StringLength(32, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 32 characters")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 characters long")]
    public string Phone { get; set; }

    [Required]
    public string Position { get; set; }
}