using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFront_end.Model.DTO_s
{
    public class RegisterDTO : LoginDTO
    {
       
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Address { get; set; }
    }
}
