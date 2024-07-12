using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class TotalPacksOpenedRanking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
        public User CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public User UpdatedBy { get; set; }

        // Navigation properties
        public User User { get; set; }
    }