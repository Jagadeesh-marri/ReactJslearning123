using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Login
    {
        [Key]   
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
