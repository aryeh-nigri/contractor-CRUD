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
using BE;
using BL;
using PLWPF.Contracts;
using PLWPF.Employees;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;
using PLWPF.Employers;
using PLWPF.Specializations;
using System.Threading;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region FIELDS

        static public UserControlEmployees employeesPage = null;
        static public UserControlEmployers employersPage = null;
        static public UserControlSpecializations specsPage = null;
        static public UserControlContracts contractsPage = null;

        private DispatcherTimer timer = new DispatcherTimer();

        #endregion


        #region CONSTRUCTOR

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();
            
            labelTimeNow.Content = DateTime.Now.ToLongTimeString();
            labelDateNow.Content = DateTime.Now.ToLongDateString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            labelTimeNow.Content = DateTime.Now.ToLongTimeString();
            labelDateNow.Content = DateTime.Now.ToLongDateString();
        }

        #endregion

        private void StartSplashScrenn()
        {
            PLWPF.Splash_Screen.MySplashScreen splashScreen = new PLWPF.Splash_Screen.MySplashScreen();
            splashScreen.ShowDialog();
        }

        #region EVENTS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employeesPage = new UserControlEmployees();
                contentControlMainPages.Content = employeesPage;
            }
            catch
            {
                MessageBox.Show("Couldn't load Employees Page", "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEmployers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employersPage = new UserControlEmployers();
                contentControlMainPages.Content = employersPage;
            }
            catch
            {
                MessageBox.Show("Couldn't load Employers Page", "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSpecs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                specsPage = new UserControlSpecializations();
                contentControlMainPages.Content = specsPage;
            }
            catch
            {
                MessageBox.Show("Couldn't load Specializations Page", "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnContracts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                contractsPage = new UserControlContracts();
                contentControlMainPages.Content = contractsPage;
            }
            catch
            {
                MessageBox.Show("Couldn't load Contracts Page", "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion


    }
}
