using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabWork8
{
    /// <summary>
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        static User User { get; set; } = new User();
        public UserInfoWindow(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InfoTextBlock.Text = $"Добро пожаловать, {User.Login}, ваша роль - {User.Role}";
        }
    }
}
