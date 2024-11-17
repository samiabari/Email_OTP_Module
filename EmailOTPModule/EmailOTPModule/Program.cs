namespace EmailOTPModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Email_OTP_Module email=new Email_OTP_Module();
            email.start();
            email.close();
        }
    }
}
