using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialHand.DTOs.Auth;

public class CreateUserDTO
{
  [Required]
  [JsonPropertyName("usuario")]
  public string Username { get; set; } = "";
  [Required]
  [JsonPropertyName("email")]
  public string Email { get; set; } = "";
  [Required]
  [JsonPropertyName("senha")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = "";
  [Required]
  [Compare("Password")]
  [JsonPropertyName("re-senha")]
  public string RePassword { get; set; } = "";
}