using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyPokedexAPI.Models{

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Persistent { get; set; }
        public string SSKey { get; set; }
        public int EspaceId { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<UserRole> UserRoles { get; set; }
    }
}