using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOM
{
    [Serializable]
    public class RegistrationDOM:Base
    {
        public int RegistrationId { get; set; }
        public string LoginName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
