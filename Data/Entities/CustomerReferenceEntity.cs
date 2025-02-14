

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerReferenceEntity
{
    [Required]
    public int CustomerId { get; set; }

    public CustomerEntity Customer { get; set; } = null!;

    [Required]
    public int ContactPersonId { get; set; }

    public ContactPersonEntity ContactPerson { get; set; } = null!;
}
