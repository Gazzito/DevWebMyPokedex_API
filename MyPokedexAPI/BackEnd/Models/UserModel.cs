// User.cs (Model)

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class User
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public string Email { get; set; }
  public string MobilePhone { get; set; }
  public DateTime Creation_Date { get; set; }
  public DateTime Last_Login { get; set; }
  public bool Is_Active { get; set; }


  // Navigation properties
  public ICollection<Pokemon> CreatedPokemons { get; set; }
  public ICollection<Pokemon> UpdatedPokemons { get; set; }
  public ICollection<Region> CreatedRegions { get; set; }
  public ICollection<Region> UpdatedRegions { get; set; }
  public ICollection<UserRole> UserRoles { get; set; }

}
