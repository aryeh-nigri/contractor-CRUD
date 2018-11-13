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
    /// Interaction logic for UserControlAdvancedSearchContracts.xaml
    /// </summary>
    public partial class UserControlAdvancedSearchContracts : UserControl
    {

        private IBL blObject;

        public List<int> employersID { get; set; }
        public List<int> employeesID { get; set; }
    

        public UserControlAdvancedSearchContracts()
        {
            blObject = FactoryBL.getBL();

            DataContext = blObject.GetAllContracts();

            employersID = (from e in blObject.GetAllEmployersById()
                           select e.Id).ToList();

            employeesID = (from w in blObject.GetAllEmployeesById()
                           select w.Id).ToList();

            InitializeComponent();

            dataGridContractsByCondition.ItemsSource = null;
            //RefreshDataGrid();

            wrapPanelButtons.DataContext = this;
            comboBoxEmployersID.DataContext = this;
            comboBoxEmployeesID.DataContext = this;
        }


        #region PROPERTIES
        public bool BtnEmployer
        {
            get
            {
                return (comboBoxEmployersID.SelectedIndex != -1);
            }
        }
        public bool BtnEmployee
        {
            get
            {
                return (comboBoxEmployeesID.SelectedIndex != -1);
            }
        }
        public bool BtnStartDate
        {
            get
            {
                return (datePickerStartDate.SelectedDate != null);
            }
        }
        public bool BtnEndDate
        {
            get
            {
                return (datePickerEndDate.SelectedDate != null);
            }
        }

        #endregion


        /// <summary>
        /// Verifies and dont accept non-numbers as input
        /// </summary>
        private void OnlyNumberAllowed(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = e.Text.Contains(" ");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ClearAllFields()
        {
            comboBoxEmployersID.SelectedIndex = -1;
            comboBoxEmployeesID.SelectedIndex = -1;
            datePickerStartDate.SelectedDate = null;
            datePickerEndDate.SelectedDate = null;
            checkBoxContractSigned.IsChecked = false;
            checkBoxInterview.IsChecked = false;

            dockPanelID.Visibility = Visibility.Collapsed;
            labelIdContract.Content = "";
            labelMode.Content = "Search for specific contracts by filling the fields below:";
        }

        private void RefreshDataGrid(IEnumerable<BE.Contract> data)
        {
            dataGridContractsByCondition.ItemsSource = null;

            dataGridContractsByCondition.DataContext = data;
            dataGridContractsByCondition.ItemsSource = data;
        }


        #region BUTTONS
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetAllContracts();
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        private void btnSearchByEmployerID_Click(object sender, RoutedEventArgs e)
        {
            int position = -1;
            try
            {
                var data = blObject.GetContracts(c =>
                                  c.EmployerId == Convert.ToInt32(comboBoxEmployersID.SelectedItem));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
                position = comboBoxEmployersID.SelectedIndex;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
                comboBoxEmployersID.SelectedIndex = position;
            }
        }

        private void btnSearchByEmployeeID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetContracts(c =>
                    c.EmployeeId == Convert.ToInt32(comboBoxEmployeesID.SelectedItem));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        private void btnSearchByStartedBefore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetContracts(c =>
                    c.StartDate <= Convert.ToDateTime(datePickerStartDate.SelectedDate));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        private void btnSearchByStartedAfter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetContracts(c =>
                    c.StartDate >= Convert.ToDateTime(datePickerStartDate.SelectedDate));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        private void btnSearchByEndBefore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetContracts(c =>
                    c.EndDate <= Convert.ToDateTime(datePickerEndDate.SelectedDate));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        private void btnSearchByEndAfter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetContracts(c =>
                    c.EndDate >= Convert.ToDateTime(datePickerEndDate.SelectedDate));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        private void btnSearchByInterview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetContracts(c =>
                    c.DidInterviewHasbeenConducted == Convert.ToBoolean(checkBoxInterview.IsChecked));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        private void btnSearchBySigned_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = blObject.GetContracts(c =>
                    c.DidContractGotSigned == Convert.ToBoolean(checkBoxContractSigned.IsChecked));
                //dataGridContractsByCondition.DataContext = data;
                RefreshDataGrid(data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
                //RefreshDataGrid();
            }
        }

        #endregion

        private void dataGridContractsByCondition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllFields();
            RefreshDataGrid(null);
            e.Handled = true;
        }

        private void dataGridContractsByCondition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridContractsByCondition.SelectedIndex == -1)
                e.Handled = true;

            Contract ctc = dataGridContractsByCondition.SelectedItem as Contract;

            if (ctc != null)
            {
                labelIdContract.Content = ctc.Id;
                dockPanelID.Visibility = Visibility.Visible;

                comboBoxEmployersID.SelectedItem = ctc.EmployerId as object;
                comboBoxEmployeesID.SelectedItem = ctc.EmployeeId as object;
                datePickerStartDate.SelectedDate = ctc.StartDate;
                datePickerEndDate.SelectedDate = ctc.EndDate;
                checkBoxContractSigned.IsChecked = ctc.DidContractGotSigned;
                checkBoxInterview.IsChecked = ctc.DidInterviewHasbeenConducted;

                labelMode.Content = "Editing mode is ON";
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.UpdateContract(int.Parse(labelIdContract.Content.ToString()),
                                        new Contract()
                                        {
                                            Id = Convert.ToInt32(labelIdContract.Content.ToString()),
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
                RefreshDataGrid(null);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.RemoveContract(int.Parse(labelIdContract.Content.ToString()));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Removing Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ClearAllFields();
                RefreshDataGrid(null);
            }
        }
    }
}
