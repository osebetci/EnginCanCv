using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnginCan.Dto.Systems
{
    public class Login
    {
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string Password { get; set; }
    }

    public class ResponseLogin
    {
        public string Token { get; set; }
        public LoginUser LoginUser { get; set; }
    }

    public class LoginUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public string FirstFireLink { get; set; }
        public string IpAddress { get; set; }
        public string HostName { get; set; }
    }
}
