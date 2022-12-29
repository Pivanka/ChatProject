using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.UserDto
{
    public class SignUp
    {
        [Required]
        //[MinLength(2)]
        //[MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "Password should contain more than 6 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords aren't equal")]
        public string ConfirmPassword { get; set; }
    }
}
