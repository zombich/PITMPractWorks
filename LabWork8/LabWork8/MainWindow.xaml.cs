using System.Windows;
using System.Windows.Controls;

namespace LabWork8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<User> users = new List<User>()
        {
            new User()
            {
                Login = "admin",
                Password = "123",
                Role = "Администратор"
            },
            new User()
            {
                Login = "user",
                Password = "1111",
                Role = "Пользователь"
            },
            new User()
            {
                Login = "moder11",
                Password = "qwerty1",
                Role = "Модератор"
            }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in users)
                if (user.Login == LoginTextBox.Text && PasswordBox.Password == user.Password)
                {
                    UserInfoWindow userInfoWindow = new UserInfoWindow(user);
                    Hide();
                    userInfoWindow.ShowDialog();
                    Show();
                    return;
                }
            MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}