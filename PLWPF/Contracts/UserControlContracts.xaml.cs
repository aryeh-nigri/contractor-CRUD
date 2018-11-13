using BE;
using BL;
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

namespace PLWPF.Contracts
{
    /// <summary>
    /// Interaction logic for UserControlContracts.xaml
    /// </summary>
    public partial class UserControlContracts : UserControl
    {
        private IBL blObject;
        private UserControlAdvancedSearchContracts advancedPage = null;

        public List<int> employersID { get; set; }
        public List<int> employeesID { get; set; }

        public UserControlContracts()
        {
            blObject = FactoryBL.getBL();

            InitializeComponent();

            try
            {
                DataContext = blObject.ContractsById();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }

            comboBoxEmployersID.DataContext = this;
            comboBoxEmployeesID.DataContext = this;

            datePickerStartDate.SelectedDate = DateTime.Now;
            datePickerStartDate.DisplayDate = DateTime.Now;
            datePickerEndDate.SelectedDate = DateTime.Now;
            datePickerEndDate.DisplayDate = DateTime.Now;

            RefreshDataGrid();
        }


        #region EVENTS

        private void btnAddContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var contract = new Contract()
                {
                    Id = Convert.ToInt32(txtIDContract.Text),
                    EmployerId = Convert.ToInt32(comboBoxEmployersID.SelectedItem),
                    EmployeeId = Convert.ToInt32(comboBoxEmployeesID.SelectedItem),
                    StartDate = Convert.ToDateTime(datePickerStartDate.SelectedDate),
                    EndDate = Convert.ToDateTime(datePickerEndDate.SelectedDate),
                    DidContractGotSigned = Convert.ToBoolean(checkBoxContractSigned.IsChecked),
                    DidInterviewHasbeenConducted = Convert.ToBoolean(checkBoxInterview.IsChecked)
                };

                blObject.AddContract(contract);
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

        private void btnRemoveContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.RemoveContract(int.Parse(labelIDContract.Content.ToString()));
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

        private void btnUpdateContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.UpdateContract(int.Parse(txtIDContract.Text),
                                        new Contract()
                                        {
                                            Id = Convert.ToInt32(txtIDContract.Text),
                                            EmployerId = Convert.ToInt32(comboBoxEmployersID.SelectedItem),
                                            EmployeeId = Convert.ToInt32(comboBoxEmployeesID.SelectedItem),
                                            StartDate = Convert.ToDateTime(datePickerStartDate.SelectedDate),
                                            EndDate = Convert.ToDateTime(datePickerEndDate.SelectedDate),
                                            DidContractGotSigned = Convert.ToBoolean(checkBoxContractSigned.IsChecked),
                                            DidInterviewHasbeenConducted = Convert.ToBoolean(checkBoxInterview.IsChecked)
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

        private void dataGridContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridContracts.SelectedIndex == -1)
                e.Handled = true;
            
            Contract ctc = dataGridContracts.SelectedItem as Contract;

            if (ctc != null)
            {
                labelIDContract.Content = ctc.Id;
                dockPanelID.Visibility = Visibility.Visible;
                dockPanelInputID.Visibility = Visibility.Collapsed;

                txtIDContract.Text = ctc.Id.ToString();
                comboBoxEmployersID.SelectedItem = ctc.EmployerId as object;
                comboBoxEmployeesID.SelectedItem = ctc.EmployeeId as object;
                datePickerStartDate.SelectedDate = ctc.StartDate;
                datePickerEndDate.SelectedDate = ctc.EndDate;
                checkBoxContractSigned.IsChecked = ctc.DidContractGotSigned;
                checkBoxInterview.IsChecked = ctc.DidInterviewHasbeenConducted;

                labelMode.Content = "Editing mode is ON";
                btnAddContract.IsEnabled = false;
            }
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
            dockPanelID.Visibility = Visibility.Collapsed;
            dockPanelInputID.Visibility = Visibility.Visible;

            txtIDContract.Clear();
            comboBoxEmployersID.SelectedIndex = -1;
            comboBoxEmployeesID.SelectedIndex = -1;
            datePickerStartDate.SelectedDate = DateTime.Now;
            datePickerEndDate.SelectedDate = DateTime.Now;
            checkBoxContractSigned.IsChecked = false;
            checkBoxInterview.IsChecked = false;

            labelMode.Content = "Fill all the fields to add a Contract";
            btnAddContract.IsEnabled = true;
        }


        /// <summary>
        /// Verify if all inputs fields are filled
        /// </summary>
        private bool DidFilledAllFields
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Refresh the values of our current grid
        /// </summary>
        private void RefreshDataGrid()
        {
            dataGridContracts.ItemsSource = null;

            try
            {
                employersID = (from e in blObject.GetAllEmployersById()
                               select e.Id).ToList();

                employeesID = (from w in blObject.GetAllEmployeesById()
                               select w.Id).ToList();

                DataContext = blObject.GetAllContractsById();
                dataGridContracts.ItemsSource = blObject.GetAllContractsById();

                //if (comboBoxOrderingBy.SelectedIndex == -1)
                //{
                //    DataContext = blObject.GetAllContractsById();
                //    dataGridContracts.ItemsSource = blObject.GetAllContractsById();
                //}
                //if (comboBoxOrderingBy.SelectedIndex == 0)//Domain
                //{
                //    DataContext = blObject.ContractsBySpecialization(true);
                //    dataGridContracts.ItemsSource = blObject.ContractsBySpecialization(true);
                //}
                //if (comboBoxOrderingBy.SelectedIndex == 1)//Address
                //{
                //    DataContext = blObject.ContractsByAddress(true);
                //    dataGridContracts.ItemsSource = blObject.ContractsByAddress(true);
                //}
                //if (comboBoxOrderingBy.SelectedIndex == 2)//Duration
                //{
                //    DataContext = (blObject.ContractsByDurationOfContract(true)).ToList();
                //    dataGridContracts.ItemsSource = (blObject.ContractsByDurationOfContract(true)).ToList();
                //}
                //if (comboBoxOrderingBy.SelectedIndex == 3)
                //{
                //    DataContext = blObject.GetAllContractsById();
                //    dataGridContracts.ItemsSource = blObject.GetAllContractsById();
                //}

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Refreshing Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }





        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }

        private void btnAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            advancedPage = new UserControlAdvancedSearchContracts();
            (this.Parent as ContentControl).Content = advancedPage;
        }

        private void comboBoxOrderingBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void dataGridContracts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllFields();
            RefreshDataGrid();
            e.Handled = true;
        }
    }
}
