
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ContactPersonModel
{
    public string Company { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;


}
