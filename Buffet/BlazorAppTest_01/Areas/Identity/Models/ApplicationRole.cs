using Microsoft.AspNetCore.Identity;

namespace BlazorAppTest_01.Areas.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}