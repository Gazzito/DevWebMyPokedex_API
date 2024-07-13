using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

   public class UserProfile
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public string FullName { get; set; }
        public byte[] ProfilePic { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }

        // Navigation properties
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
    }
