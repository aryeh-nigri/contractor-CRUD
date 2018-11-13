using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF.Splash_Screen
{
    /// <summary>
    /// Interaction logic for MySplashScreen.xaml
    /// </summary>
    public partial class MySplashScreen : Window
    {
        public MySplashScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PLWPF.MainWindow.specsPage = new Specializations.UserControlSpecializations();
            PLWPF.MainWindow.employeesPage = new Employees.UserControlEmployees();
            PLWPF.MainWindow.employersPage = new Employers.UserControlEmployers();
            PLWPF.MainWindow.contractsPage = new Contracts.UserControlContracts();
            Thread.Sleep(3000);
            this.Close();
        }
    }
}
