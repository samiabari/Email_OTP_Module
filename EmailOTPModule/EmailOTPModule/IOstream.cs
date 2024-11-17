using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTPModule
{
    public class IOstream
    {
        public string readOTP()
        {
            string otp;
           
            otp = Console.ReadLine();
            if (otp.Length == 6 && long.TryParse(otp, out _))
            {
                  return otp;
            }
            return string.Empty;
            
        }
    }
}
