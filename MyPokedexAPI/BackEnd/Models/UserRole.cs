using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

  public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Role Role { get; set; }
    }
