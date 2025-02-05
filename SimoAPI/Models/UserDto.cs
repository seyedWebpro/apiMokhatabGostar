using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class UserDto
    {
        public string Username { get; set; } = "";
        public string? Roles { get; set; } = "";
        public string Pic { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = ""; // اضافه شدن رمز عبور
    }


}