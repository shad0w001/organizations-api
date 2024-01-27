using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.DTOs
{
    public class LoginUserDTO
    {
        [EmailAddress]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
