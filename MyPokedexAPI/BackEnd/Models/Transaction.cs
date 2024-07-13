using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyPokedexAPI.Models{
public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PackId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Pack Pack { get; set; }
       
    }

     public class TransactionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PackId { get; set; }
    }

}