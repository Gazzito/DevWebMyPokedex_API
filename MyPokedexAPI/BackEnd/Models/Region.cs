using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public User CreatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public User UpdatedBy { get; set; }
  

        // Navigation properties
        public ICollection<Pokemon> Pokemon { get; set; }
}
