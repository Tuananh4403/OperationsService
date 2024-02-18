namespace OperationsService.Models.Roles;

using System.ComponentModel.DataAnnotations;

public class CreateRole
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Name { get; set; }
}