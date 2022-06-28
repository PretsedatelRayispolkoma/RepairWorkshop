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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepairWorkshop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTB.Text == "" || PasswordPB.Password == "")
            {
                MessageBox.Show("Enter your username or password");
            }
            else
            {
                foreach (var authUser in MainWindow.db.User)
                {
                    if (authUser.Login == LoginTB.Text.Trim())
                    {
                        if (authUser.Password == PasswordPB.Password.Trim())
                        {
                            MainWindow.IDClient = authUser.EmployeeId;
                            MainWindow.IDRole = authUser.RoleId;
                            this.NavigationService.Navigate(new MainPage());
                        }
                    }
                }
            }
        }
    }
}
