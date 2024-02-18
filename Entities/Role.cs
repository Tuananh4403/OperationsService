namespace OperationsService.Entities;

public class Role
{
    public int RoleId  {get; set;}
    public string Title {get; set;}
    public string Name {get; set;}
    public ICollection<UserRole> UserRoles { get; set; }

}