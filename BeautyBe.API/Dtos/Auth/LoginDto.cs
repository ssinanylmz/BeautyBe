using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyBe.API.Dtos.Auth
{
    public class LoginDto
    {
        public string UserName { get; set; }
        [Required]
        public string PasswordCrypt { get; set; }
        public string Email { get; set; }
    }
}
