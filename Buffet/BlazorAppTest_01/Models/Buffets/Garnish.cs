using System.ComponentModel.DataAnnotations;

namespace Hawaso.Models.Buffets;

public class Garnish
{
    /// <summary>
    /// 고명
    /// </summary>
    public int Id { get; set; }

    [Required]
    [StringLength(25)]
    public string? Name { get; set; }

    public int? NoodleId { get; set; }

    public Noodle? Noodle { get; set; }
}
