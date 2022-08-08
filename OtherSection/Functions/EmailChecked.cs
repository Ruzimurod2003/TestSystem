using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Functions
{
    public class EmailChecked
    {
        public static bool ValidateEmail(string email, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match emailChecked = regex.Match(email ?? "");
            if (emailChecked.Success)
            {
                return true;
            }
            else
            {
                ErrorMessage = "There is an error in writing the email";
                return false;
            }
        }
    }
}
