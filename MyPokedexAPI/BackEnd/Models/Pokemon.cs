using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema; 
      public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
        public int BaseAttackPoints { get; set; }
        public int BaseHealthPoints { get; set; }
        public int BaseDefensePoints { get; set; }
        public int BaseSpeedPoints { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }

        // Navigation property
    public Region Region { get; set; }
     public User CreatedBy { get; set; }
    public User UpdatedBy { get; set; }
    }
