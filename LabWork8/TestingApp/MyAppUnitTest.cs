using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using System.Text.Json.Serialization;

namespace TestingApp
{
    public class MyAppUnitTest
    {
        private Application _app;
        private UIA3Automation _automation;
        private readonly string _path = @"C:\Users\fetma\source\repos\zombich\PITMLabWork8\LabWork8\bin\Debug\net8.0-windows\LabWork8.exe";

        [Fact]
        public void ClickAuthorizationButtonWithoutLoginAndPasswordReturnsError()
        {
            _app = Application.Launch(_path);
            _automation = new UIA3Automation();

            var window = _app.GetMainWindow(_automation);
            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("AuthButton")).AsButton();
            button?.Click();

            var errorWindow = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Ошибка"))).AsWindow();

            var screenshot = errorWindow.Capture();
            screenshot.Save("screenshot.png");


            Assert.True(errorWindow is not null);

            _app.Close();
        }

        [Theory]
        [InlineData("user","qwedfik0efiok")]
        [InlineData("admin","qdzxcv123!@#!")]
        [InlineData("moder11","opa")]
        [InlineData("qwertyu","1111")]
        [InlineData("up","123")]
        public void ClickAuthorizationButtonWithIncorrectLoginOrPasswordReturnsError(string login,string password)
        {
            _app = Application.Launch(_path);
            _automation = new UIA3Automation();

            var window = _app.GetMainWindow(_automation);

            var loginTextBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            loginTextBox.Text = login;

            var passwordBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();
            passwordBox.Text = password;

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("AuthButton")).AsButton();
            button?.Click();

            var errorWindow = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Ошибка"))).AsWindow();

            Assert.True(errorWindow is not null);

            var screenshot = errorWindow.Capture();
            screenshot.Save("screenshot.png");

            _app.Close();
        }


        [Theory]
        [InlineData("admin","123")]
        [InlineData("user", "1111")]
        [InlineData("moder11", "qwerty1")]
        public void ClickAuthorizationButtonWithCorrectLoginAndPasswordOpensInfoWindow(string login, string password)
        {
            _app = Application.Launch(_path);
            _automation = new UIA3Automation();

            var window = _app.GetMainWindow(_automation);

            var loginTextBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            loginTextBox.Text = login;

            var passwordBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();
            passwordBox.Text = password;

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("AuthButton")).AsButton();
            button?.Click();

            var infoWindow = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Информация"))).AsWindow();

            Assert.True(infoWindow is not null);


            var screenshot = infoWindow.Capture();
            screenshot.Save("screenshot.png");

            _app.Close();
        }

        [Theory]
        [InlineData("admin", "123", "Администратор")]
        [InlineData("user", "1111", "Пользователь")]
        [InlineData("moder11", "qwerty1", "Модератор")]
        public void InfoWindowShowsCorrectInformation(string login, string password, string roleName)
        {
            _app = Application.Launch(_path);
            _automation = new UIA3Automation();

            var window = _app.GetMainWindow(_automation);

            var loginTextBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            loginTextBox.Text = login;

            var passwordBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();
            passwordBox.Text = password;

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("AuthButton")).AsButton();
            button?.Click();

            var infoWindow = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Информация"))).AsWindow();

            var infoTextBlock = infoWindow?.FindFirstDescendant(cf => cf.ByAutomationId("InfoTextBlock")).AsLabel();

            var text = infoTextBlock.Text;

            Assert.True(text.Contains(roleName));

            var screenshot = infoWindow.Capture();
            screenshot.Save("screenshot.png");

            _app.Close();
        }

        [Fact]
        public void InfoWindowShowsCorrectInformationOnSecondLogin()
        {
            _app = Application.Launch(_path);
            _automation = new UIA3Automation();

            var window = _app.GetMainWindow(_automation);

            string firstLogin = "admin";
            string firstPassword = "123";
            string firstRole = "Администратор";

            string secondLogin = "user";
            string secondPassword = "1111";
            string secondRole = "Пользователь";

            var loginTextBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            loginTextBox.Text = firstLogin;

            var passwordBox = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();
            passwordBox.Text = firstPassword;

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("AuthButton")).AsButton();
            button?.Click();

            var infoWindow = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Информация"))).AsWindow();

            var infoTextBlock = infoWindow?.FindFirstDescendant(cf => cf.ByAutomationId("InfoTextBlock")).AsLabel();

            var firstText = infoTextBlock.Text;

            infoWindow.Close();

            loginTextBox.Text = secondLogin;
            passwordBox.Text = secondPassword;

            button?.Click();

            infoWindow = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Информация"))).AsWindow();

            infoTextBlock = infoWindow?.FindFirstDescendant(cf => cf.ByAutomationId("InfoTextBlock")).AsLabel();

            var secondText = infoTextBlock.Text;

            Assert.True(firstText.Contains(firstRole) && secondText.Contains(secondRole));

            var screenshot = infoWindow.Capture();
            screenshot.Save("screenshot.png");

            _app.Close();
        }
    }
}