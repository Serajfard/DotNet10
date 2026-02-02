using System.ComponentModel.DataAnnotations;

namespace DemoApi.Dtos;

public class CreateUserRequest
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = "";

    [Range(18, 120)]
    public int Age { get; set; }
}
