using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmailOTPModule
{
    public class Email_OTP_Module: IDisposable
    {
        string domain, email_address, emailBody, currentOTP;
        int otpLength, otp_try_limit, time_out_limit;
        bool dispose = false, email_sent;                           // flag to check if email is sent or not.
        DateTime optGenTime;   //otp generation time

        public const string STATUS_EMAIL_OK = "email containing OTP has been sent successfully.\n";
        public const string STATUS_EMAIL_FAIL = "email address does not exist or sending to the email has failed.\n";
        public const string STATUS_EMAIL_INVALID = "email address is invalid.\n";
        public const string STATUS_OTP_OK = "OTP is valid and checked\n";
        public const string STATUS_OTP_FAIL = "OTP is wrong after 10 tries.\n";
        public const string STATUS_OTP_TIMEOUT = "timeout after 1 min.\n";
        
        public Email_OTP_Module()
        {
            domain = ".dso.org.sg";
            emailBody = "You OTP Code is {0}. The code is valid for 1 minute.\n";
            otpLength = 6;
            otp_try_limit = 10;
            time_out_limit=60;                         // domain, time_out_seconds and otp_try_limit initialized.
            email_address = "";
            
        }

        public void start()
        {
            Console.WriteLine("Email OTP module started.");
            while (true)
            {
                email_sent=false;
                email_address = Console.ReadLine();  //Email input from the user.
                Console.WriteLine(generate_OTP_email(email_address));     

                IOstream input= new IOstream();    
                if (email_sent)
                {
                    Console.WriteLine(check_OTP(input));
                }
            }
        }
     
        public void close()
        {
            Dispose();
        }

       
        public string generate_OTP_email(string user_email)
        {
            if (check_Valid_Email(user_email))
            {
                if (check_Valid_Domain(user_email))
                {
                    try
                    {
                        send_email(user_email, generate_email());
                        email_sent=true;
                        return STATUS_EMAIL_OK;
                    }
                    catch (Exception ex) {
                        return STATUS_EMAIL_FAIL;
                    }
                    
                }
                else
                {
                    return STATUS_EMAIL_INVALID;
                }
            }
            else {
                return STATUS_EMAIL_INVALID;
            }
        }

        public void send_email(string email_address, string email_body)
        {
            Console.WriteLine("\nEmail Address: "+ email_address);
            Console.WriteLine("Email-Body: " + email_body);
        }

        public string generate_email()
        {
            currentOTP = generate_random_OTP();
            optGenTime = DateTime.Now;
            string finalemailBody = string.Format(emailBody, currentOTP);
            return finalemailBody;
        }

        public string generate_random_OTP()
        {
            Random random=new Random();
            string otp = "";
            for (int i = 0; i < otpLength; i++)
            {
                otp = otp + random.Next(0, 10).ToString();
            }
            return otp;
        }

        public bool check_Valid_Email(string user_email)
        {
            if (!string.IsNullOrEmpty(user_email))
            {
                int indexOfAt = user_email.IndexOf('@');
                int indexOfDot = user_email.LastIndexOf('.');
                if (indexOfAt > 0 && indexOfDot > indexOfAt && indexOfDot < user_email.Length - 1)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool check_Valid_Domain(string user_email)
        {
            if (user_email.EndsWith(domain))
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }

        public string check_OTP(IOstream input)
        {
            string opt;
            DateTime startAttempt=DateTime.Now;
            int attempt = 0;

            while (attempt < otp_try_limit)
            {
                int current_seconds = (int)Math.Round((DateTime.Now - startAttempt).TotalSeconds);

                if (current_seconds > time_out_limit)
                {
                    return STATUS_OTP_TIMEOUT;
                }

                opt = input.readOTP();
                if (opt == currentOTP)
                {
                    return STATUS_OTP_OK;
                }
                else 
                {
                    attempt++;
                }
            }

            return STATUS_OTP_FAIL;
        }

        public void Dispose()
        {
            if (dispose != true)
            {
                currentOTP = null;
                dispose = true;
                Console.WriteLine("Email OTP module cleaned.");
            }
            
            GC.SuppressFinalize(this);
        }
    }
}
