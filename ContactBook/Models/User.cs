using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactBook.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public char genderM { get; set; }
        public char genderF { get; set; }


    }
}