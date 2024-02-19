namespace OperationsService.Entities;

using System.Text.Json.Serialization;

public class User
{
    public User()
    {
        UserRoles = new List<UserRole>(); // Initialize the collection in the constructor
    }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
}