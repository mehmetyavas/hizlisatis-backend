using Core.Entities.Abstract;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class loginResDTO : IDto
    {
        public AccessToken AccessToken { get; set; }
        public int  UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
