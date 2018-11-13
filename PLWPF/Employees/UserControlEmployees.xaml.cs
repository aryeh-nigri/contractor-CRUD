using System;
using System.Collections.Generic;
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
using BL;
using BE;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Collections;
using System.Runtime.CompilerServices;

namespace PLWPF.Employees
{
    /// <summary>
    /// Interaction logic for UserControlEmployees.xaml
    /// </summary>
    public partial class UserControlEmployees : UserControl
    {
        #region FIELDS

        BankAccountWindow bankWindow;

        private static DateTime startDay = new DateTime(1980, 1, 1);
        private IBL blObject;
        //private ObservableCollection<Employee> employees;

        public List<int> SpecsID { get; set; }
        public List<string> specsName{ get;set; }

        #endregion

        #region CONSTRUCTOR

        public UserControlEmployees()
        {
            blObject = FactoryBL.getBL();

            try
            {
                bankWindow = new BankAccountWindow();
                DataContext = blObject.GetAllEmployeesById();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Starting Error");
            }
            InitializeComponent();

            datePickerBirthdayEmployee.DisplayDate = startDay;
            datePickerBirthdayEmployee.SelectedDate = startDay;
            
            RefreshDataGrid();
            
            comboBoxSpeciality.DataContext = this;
            btnAddEmployee.DataContext = this;
        }
        
        #endregion


        #region EVENTS

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            
        }

        private void btnBankAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bankWindow.Closing += BankWindow_Closing;
                bankWindow.ShowDialog();
                
            }
            catch(Exception error)
            {
                MessageBox.Show("Banks list still loading!\nDetails: " + error.Message);
            }

        }

        private void BankWindow_Closing(object sender, CancelEventArgs e)
        {
            if(sender is BankAccountWindow)
            {
                e.Cancel = true;
                bankWindow.Hide();
            }
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = new Employee()
                {
                    Id = Convert.ToInt32(txtIdEmployee.Text),
                    FirstName = txtFirstNameEmployee.Text,
                    LastName = txtLastNameEmployee.Text,
                    Telephone = Convert.ToInt64(txtTelephoneEmployee.Text),
                    Address = txtAddressEmployee.Text,
                    Birthday = Convert.ToDateTime(datePickerBirthdayEmployee.SelectedDate),
                    //Formation=(Enums.Degree)Enum.Parse(typeof(Enums.Degree),comboBoxFormationEmployee.SelectedItem.ToString()),
                    Formation = (Enums.Degree)comboBoxFormationEmployee.SelectedIndex,
                    IsMilitaryGraduate = Convert.ToBoolean(checkBoxMilitaryEmployee.IsChecked),
                    SpecialtyId = Convert.ToInt32(comboBoxSpeciality.SelectedItem),
                    Account = bankWindow.bankAccount
                };

                blObject.AddEmployee(employee);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Adding Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                bankWindow.Close();
                ClearAllFields();
                RefreshDataGrid();
            }
        }

        private void btnRemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var wantToRemove = MessageBox.Show("Are you sure you want to remove Employee?\nDetails:\n" +
                                                   blObject.FindEmployeeById(int.Parse(labelIDEmployee.Content.ToString())).ToString(),
                                                   "Remove Employee", 
                                                   MessageBoxButton.YesNoCancel, 
                                                   MessageBoxImage.Exclamation);

                if (wantToRemove == MessageBoxResult.Yes)
                    blObject.RemoveEmployee(int.Parse(labelIDEmployee.Content.ToString()));
            }
            catch(Exception error)
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
                var wantToUpdate = MessageBox.Show("Are you sure you want to update Employee?\nDetails:\n" +
                                                   blObject.FindEmployeeById(int.Parse(labelIDEmployee.Content.ToString())).ToString(),
                                                   "Update Employee",
                                                   MessageBoxButton.YesNoCancel,
                                                   MessageBoxImage.Exclamation);

                if (wantToUpdate == MessageBoxResult.Yes)
                {
                    blObject.UpdateEmployee(int.Parse(labelIDEmployee.Content.ToString()),
                                            new Employee()
                                            {
                                                Id = Convert.ToInt32(txtIdEmployee.Text),
                                                FirstName = txtFirstNameEmployee.Text,
                                                LastName = txtLastNameEmployee.Text,
                                                Telephone = Convert.ToInt64(txtTelephoneEmployee.Text),
                                                Address = txtAddressEmployee.Text,
                                                Birthday = Convert.ToDateTime(datePickerBirthdayEmployee.SelectedDate),
                                                Formation = (Enums.Degree)comboBoxFormationEmployee.SelectedIndex,
                                                IsMilitaryGraduate = Convert.ToBoolean(checkBoxMilitaryEmployee.IsChecked),
                                                SpecialtyId = Convert.ToInt32(comboBoxSpeciality.SelectedItem),
                                                Account = bankWindow.bankAccount
                                            });
                }
            }
            catch(Exception error)
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
            Employee emp = dataGridEmployees.SelectedItem as Employee;

            if (emp != null)
            {
                labelMode.Content = "EDITING MODE IS ON";
                labelIDEmployee.Content = emp.Id;
                dockPanelIdEmployee.Visibility = Visibility.Visible;
                dockPanelInputID.Visibility = Visibility.Collapsed;

                txtIdEmployee.Text = emp.Id.ToString();
                txtFirstNameEmployee.Text = emp.FirstName;
                txtLastNameEmployee.Text = emp.LastName;
                txtTelephoneEmployee.Text = emp.Telephone.ToString();
                txtAddressEmployee.Text = emp.Address;
                datePickerBirthdayEmployee.SelectedDate = emp.Birthday;
                comboBoxFormationEmployee.SelectedIndex = (int)emp.Formation;
                //comboBoxFormationEmployee.SelectedItem = emp.Formation.ToString();
                checkBoxMilitaryEmployee.IsChecked = emp.IsMilitaryGraduate;
                //comboBoxSpeciality.SelectedIndex = emp.SpecialtyId;
                comboBoxSpeciality.SelectedValue = emp.SpecialtyId;

                try
                {
                    //bankWindow.txtBankNumber.Text = emp.Account.bankNumber.ToString();
                    //bankWindow.txtBankName.Text = emp.Account.bankName;
                    //bankWindow.txtBankAgency.Text = emp.Account.bankAgency.ToString();
                    //bankWindow.txtBankAddress.Text = emp.Account.bankAddress;
                    //bankWindow.txtBankCity.Text = emp.Account.bankCity;
                    //bankWindow.txtAccountNumber.Text = emp.Account.accountNumber.ToString();

                    bankWindow.comboBoxBankAddresses.SelectedItem = emp.Account.bankAddress as object;
                    bankWindow.comboBoxBankAgencies.SelectedItem = emp.Account.bankAgency as object;
                    bankWindow.comboBoxBankCities.SelectedItem = emp.Account.bankCity as object;
                    bankWindow.comboBoxBankNames.SelectedItem = emp.Account.bankName as object;
                    bankWindow.comboBoxBankNumbers.SelectedItem = emp.Account.bankNumber as object;
                    bankWindow.txtBankAccountNumber.Text = emp.Account.accountNumber.ToString();
                }
                catch
                {
                    MessageBox.Show("BankWindow wasnt created!");
                }

                btnAddEmployee.IsEnabled = false;
            }
        }

        private void dataGridEmployees_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllFields();
            RefreshDataGrid();
            e.Handled = true;
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
            txtIdEmployee.Clear();
            txtFirstNameEmployee.Clear();
            txtLastNameEmployee.Clear();
            txtTelephoneEmployee.Clear();
            txtAddressEmployee.Clear();
            datePickerBirthdayEmployee.DisplayDate = startDay;
            datePickerBirthdayEmployee.SelectedDate = startDay;
            comboBoxFormationEmployee.SelectedIndex = -1;
            checkBoxMilitaryEmployee.IsChecked = false;
            comboBoxSpeciality.SelectedIndex = -1;

            if (bankWindow != null)
            {
                //bankWindow.txtBankNumber.Clear();
                //bankWindow.txtBankName.Clear();
                //bankWindow.txtBankAgency.Clear();
                //bankWindow.txtBankAddress.Clear();
                //bankWindow.txtBankCity.Clear();
                //bankWindow.txtAccountNumber.Clear();

                bankWindow.comboBoxBankAddresses.SelectedIndex = -1;
                bankWindow.comboBoxBankAgencies.SelectedIndex = -1;
                bankWindow.comboBoxBankCities.SelectedIndex = -1;
                bankWindow.comboBoxBankNames.SelectedIndex = -1;
                bankWindow.comboBoxBankNumbers.SelectedIndex = -1;
                bankWindow.txtBankAccountNumber.Clear();

            
            }

            dockPanelIdEmployee.Visibility = Visibility.Collapsed;
            dockPanelInputID.Visibility = Visibility.Visible;
            labelMode.Content = "Fill all the fields below to add an Employee";
            btnAddEmployee.IsEnabled = true;
        }


        /// <summary>
        /// Verify if all inputs fields are filled
        /// </summary>
        public bool DidFilledAllFields
        {
            get
            {
                return (txtIdEmployee.Text != "" &&
                        txtFirstNameEmployee.Text != "" &&
                        txtLastNameEmployee.Text != "" &&
                        txtTelephoneEmployee.Text != "" &&
                        txtAddressEmployee.Text != "" &&
                        comboBoxFormationEmployee.SelectedIndex != -1 &&
                        comboBoxSpeciality.SelectedIndex != -1);
            }
        }

        /// <summary>
        /// Refresh the values of our current grid
        /// </summary>
        private void RefreshDataGrid()
        {
            dataGridEmployees.ItemsSource = null;

            try
            {
                dataGridEmployees.ItemsSource = blObject.GetAllEmployeesById();
                
                SpecsID = (from s in blObject.GetAllSpecializationsById()
                           select s.Id).ToList();

                specsName = (from s in blObject.GetAllSpecializations()
                             select s.Name).ToList();
                
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Refreshing Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        

        #endregion


    }
}