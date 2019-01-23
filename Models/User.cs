using System;
using System.ComponentModel.DataAnnotations;
using APIFinancas.Interfaces;

namespace APIFinancas.Models
{
    public class User : ITimeStampedModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }    
}
