using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PackId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Pack Pack { get; set; }
    }