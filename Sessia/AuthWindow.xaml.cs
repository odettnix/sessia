
using Sessia.DataSet1TableAdapters;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading;

namespace Sessia
{
    public partial class AuthWindow : Window
    {
        public static AuthWindow authWindow;

        public AuthWindow()
        {
            InitializeComponent();
            authWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.PIN != null && Properties.Settings.Default.PIN != "")
            {
                new MainWindow().Show();
                this.Close();
            }
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                AuthWindow.authWindow.DragMove();
        }

        

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            UsersTableAdapter usersTable = new UsersTableAdapter();
            DataSet1.UsersDataTable usersRows = new DataSet1.UsersDataTable();
            usersTable.Fill(usersRows);
            int a = 0;

            DataSet1.UsersRow user = usersRows.Where(userDB => userDB.Password.Equals(PassTB.Text)).FirstOrDefault();
            if (user != null)
            {
                new Window1().Show();
                this.Close();
                return;
            }
            else
            {
                a++;
            }
            if (a>3)
            {
                AuthBtn.IsEnabled = false;
                Thread.Sleep(10000);
                AuthBtn.IsEnabled = true;
                a=0;
            }
            MessageBox.Show("Неверный логин или пароль");
        }
    }
}