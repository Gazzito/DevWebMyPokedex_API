using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
 
 
 public class User_Role
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
