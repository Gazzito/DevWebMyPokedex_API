using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Role{
    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Persistent { get; set; }
        public string Ss_Key { get; set; }
        public string Espace_Id { get; set; }


        // Navigation properties
        public ICollection<User_Role> User_Roles { get; set; }
    
}
