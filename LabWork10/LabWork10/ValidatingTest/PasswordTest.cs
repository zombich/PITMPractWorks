using LabWork10;

namespace ValidatingTest
{
    public class PasswordTest
    {
        [Theory]
        [InlineData("passW@rd1")]
        [InlineData("p!SSw0rd1")]
        [InlineData("123456aA@")]
        [InlineData("#Par0lcarol1234679890228!")]
        [InlineData("Adminka!1703")]
        public void CorrectPassword_ReturnsTrue(string password)
        {
            Assert.True(Password.IsValid(password));
        }

        [Theory]
        [InlineData("pswrd")]
        [InlineData("")]
        [InlineData("@34as")]
        [InlineData("@@@@@@@@@@@@@@@@@3344334")]
        [InlineData("!!!!!asd")]
        public void IncorrectPassword_ReturnsFalse(string password)
        {
            Assert.False(Password.IsValid(password));
        }

        [Fact]
        public void NullPassword_ReturnsThrowsException()
        {
            string? password = null;
            Assert.Throws<ArgumentNullException>(()=> Password.IsValid(password));
        }
    }
}