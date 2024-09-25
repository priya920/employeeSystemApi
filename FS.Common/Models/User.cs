using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.Common.Models
{
    public class User
    {
      
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Pasword { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string email { get; set; }
    }
}
