using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PLWPF.Employers
{
    /// <summary>
    /// Interaction logic for UserControlEmployers.xaml
    /// </summary>
    public partial class UserControlEmployers : UserControl
    {
        #region FIELDS

        private static DateTime startDay = new DateTime(1980, 1, 1);
        private IBL blObject;
        //private ObservableCollection<Employee> employees;

        #endregion

        #region CONSTRUCTOR

        public UserControlEmployers()
        {
            InitializeComponent();

            datePickerStartDate.DisplayDate = startDay;
            datePickerStartDate.SelectedDate = startDay;

            blObject = FactoryBL.getBL();

            try
            {
                DataContext = blObject.GetAllEmployersById();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }

            RefreshDataGrid();
        }

        #endregion

        #region EVENTS
        
        private void btnAddEmployer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employer = new Employer()
                {
                    Id = Convert.ToInt32(txtIdEmployer.Text),
                    IsIndividual = Convert.ToBoolean(checkBoxIndividual.IsChecked),
                    CompanyName = txtCompanyName.Text,
                    FirstName = txtFirstNameEmployer.Text,
                    LastName = txtLastNameEmployer.Text,
                    Telephone = Convert.ToInt64(txtTelephoneEmployer.Text),
                    Address = txtAddressEmployer.Text,
                    //Domain = (Enums.Domain)Enum.Parse(typeof(Enums.Domain), comboBoxDomainEmployer.SelectedItem.ToString()),
                    Domain = (Enums.Discipline)comboBoxDomainEmployer.SelectedIndex,
                    DateOfEstablishment = Convert.ToDateTime(datePickerStartDate.SelectedDate)
                };

                blObject.AddEmployer(employer);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Adding Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ClearAllFields();
                RefreshDataGrid();
            }
        }

        private void btnRemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.RemoveEmployer(int.Parse(labelIDEmployee.Content.ToString()));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Removing Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ClearAllFields();
                RefreshDataGrid();
            }
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.UpdateEmployer(int.Parse(labelIDEmployee.Content.ToString()),
                                        new Employer()
                                        {
                                            Id = Convert.ToInt32(txtIdEmployer.Text),
                                            IsIndividual = Convert.ToBoolean(checkBoxIndividual.IsChecked),
                                            CompanyName = txtCompanyName.Text,
                                            FirstName = txtFirstNameEmployer.Text,
                                            LastName = txtLastNameEmployer.Text,
                                            Telephone = Convert.ToInt64(txtTelephoneEmployer.Text),
                                            Address = txtAddressEmployer.Text,
                                            Domain = (Enums.Discipline)comboBoxDomainEmployer.SelectedIndex,
                                            DateOfEstablishment = Convert.ToDateTime(datePickerStartDate.SelectedDate)
                                        });

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Updating Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ClearAllFields();
                RefreshDataGrid();
            }
        }

        private void dataGridEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employer emp = dataGridEmployers.SelectedItem as Employer;

            if (emp != null)
            {
                labelMode.Content = "EDITING MODE IS ON";
                labelIDEmployee.Content = emp.Id;
                dockPanelID.Visibility = Visibility.Visible;
                dockPanelInputID.Visibility = Visibility.Collapsed;

                txtIdEmployer.Text = emp.Id.ToString();
                txtCompanyName.Text = emp.CompanyName;
                txtFirstNameEmployer.Text = emp.FirstName;
                txtLastNameEmployer.Text = emp.LastName;
                txtTelephoneEmployer.Text = emp.Telephone.ToString();
                txtAddressEmployer.Text = emp.Address;
                datePickerStartDate.SelectedDate = emp.DateOfEstablishment;
                checkBoxIndividual.IsChecked = emp.IsIndividual;
                comboBoxDomainEmployer.SelectedIndex = (int)emp.Domain;
                //comboBoxDomainEmployer.SelectedItem = emp.Domain.ToString() as object;

                btnAddEmployer.IsEnabled = false;
            }
            else
            {
                ClearAllFields();
                e.Handled = true;
            }
        }

        private void dataGridEmployers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dataGridEmployers.SelectedIndex = -1;
            ClearAllFields();
        }
        #endregion




        #region FUNCTIONS

        /// <summary>
        /// Verifies and dont accept non-numbers as input
        /// </summary>
        private void OnlyNumberAllowed(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Erase all the filled fields
        /// </summary>
        private void ClearAllFields()
        {
            txtIdEmployer.Clear();
            txtCompanyName.Clear();
            txtFirstNameEmployer.Clear();
            txtLastNameEmployer.Clear();
            txtTelephoneEmployer.Clear();
            txtAddressEmployer.Clear();
            datePickerStartDate.DisplayDate = startDay;
            datePickerStartDate.SelectedDate = startDay;
            comboBoxDomainEmployer.SelectedIndex = -1;
            checkBoxIndividual.IsChecked = false;

            dockPanelID.Visibility = Visibility.Collapsed;
            dockPanelInputID.Visibility = Visibility.Visible;
            labelMode.Content = "Fill all fields to add an Employer";
            btnAddEmployer.IsEnabled = true;
        }


        /// <summary>
        /// Verify if all inputs fields are filled
        /// </summary>
        private bool DidFilledAllFields
        {
            get
            {
                return (txtIdEmployer.Text.Length != 0 &&
                        txtFirstNameEmployer.Text.Length != 0 &&
                        txtLastNameEmployer.Text.Length != 0 &&
                        txtTelephoneEmployer.Text.Length != 0 &&
                        txtAddressEmployer.Text.Length != 0 &&
                        comboBoxDomainEmployer.SelectedIndex != -1);
            }
        }

        /// <summary>
        /// Refresh the values of our current grid
        /// </summary>
        private void RefreshDataGrid()
        {
            dataGridEmployers.ItemsSource = null;
            try
            {
                dataGridEmployers.ItemsSource = blObject.GetAllEmployersById();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Refreshing Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        
    }
}
