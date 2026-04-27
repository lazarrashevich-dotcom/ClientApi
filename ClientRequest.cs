using System.ComponentModel.DataAnnotations;
using ClientApi.Models.Enums;

namespace ClientApi.Models;

public class ClientRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Balance { get; set; }

    [Range(0, 120)]
    public int AccountAgeDays { get; set; }

    public ClientStatus Status { get; set; } = ClientStatus.Active;

    public bool HasOverduePayments { get; set; }
}
