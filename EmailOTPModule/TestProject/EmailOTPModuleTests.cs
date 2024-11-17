using EmailOTPModule;

namespace TestProject
{
    public class EmailOTPModuleTests
    {
        [Fact]
        public void TestGenerateOTP_ShouldGenerateSixDigitOTP()
        {
            var emailOTPModule = new Email_OTP_Module();
            string otp = emailOTPModule.generate_random_OTP();
            Assert.NotNull(otp);
            Assert.Equal(6, otp.Length);
            Assert.Matches(@"^\d{6}$", otp);  
        }

        [Fact]
        public void TestCheckValidEmail_ValidEmail_ReturnsTrue()
        {
           
            var emailOTPModule = new Email_OTP_Module();

    
            bool isValid = emailOTPModule.check_Valid_Email("user@dso.org.sg");

         
            Assert.True(isValid);
        }

        [Fact]
        public void TestCheckValidEmail_InvalidEmail_ReturnsFalse()
        {
           
            var emailOTPModule = new Email_OTP_Module();

           
            bool isValid = emailOTPModule.check_Valid_Email("user@xyz");

            
            Assert.False(isValid);
        }

        [Fact]
        public void TestCheckValidDomain_ValidDomain_ReturnsTrue()
        {
            
            var emailOTPModule = new Email_OTP_Module();

            
            bool isValid = emailOTPModule.check_Valid_Domain("user@dso.org.sg");

            
            Assert.True(isValid);
        }

        [Fact]
        public void TestCheckValidDomain_InvalidDomain_ReturnsFalse()
        {
           
            var emailOTPModule = new Email_OTP_Module();

           
            bool isValid = emailOTPModule.check_Valid_Domain("user@xyz.com");

        
            Assert.False(isValid);
        }

        [Fact]
        public void TestGenerateOTPEmail_WithValidEmail_SendsOTP()
        {
           
            var emailOTPModule = new Email_OTP_Module();
            var email = "user@dso.org.sg";

           
            var result = emailOTPModule.generate_OTP_email(email);

           
            Assert.Equal(Email_OTP_Module.STATUS_EMAIL_OK, result);
        }

    }
}