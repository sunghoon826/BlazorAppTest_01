using System.ComponentModel.DataAnnotations;

namespace BlazorAppTest_01.Models.Buffets;

/// <summary>
/// 국수
/// </summary>
public class Noodle
{
    public int Id { get; set; }

    [Required]
    [StringLength(25)]
    public string? Name { get; set; }

    public int? BrothId { get; set; }

    [Required]
    public Broth? Broth { get; set; }
}
