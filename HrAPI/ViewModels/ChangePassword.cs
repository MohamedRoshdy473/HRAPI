using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.ViewModels
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "userName is required")]
        public string userName { get; set; }
        

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; }
    }
}
