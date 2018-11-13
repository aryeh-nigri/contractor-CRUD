using BE;
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
using System.Windows.Shapes;
using BL;
using System.Threading;

namespace PLWPF.Employees
{
    /// <summary>
    /// Interaction logic for BankAccountWindow.xaml
    /// </summary>
    public partial class BankAccountWindow : Window
    {
        public Enums.BankAccount bankAccount;

        private IBL blObject;

        private List<Enums.BankAccount> banks;

        public static List<long> bankNumbers { get; set; }
        public static List<string> bankNames { get; set; }
        public static List<int> bankAgencies { get; set; }
        public static List<string> bankAddresses { get; set; }
        public static List<string> bankCities { get; set; }
        
        public BankAccountWindow()
        {
            blObject = FactoryBL.getBL();

            banks = blObject.GetAllBankAgencies().ToList();

            bankNumbers = (from b in banks
                           select b.bankNumber).Distinct().ToList();
            bankAgencies = (from b in banks
                            select b.bankAgency).Distinct().ToList();
            bankCities = (from b in banks
                          select b.bankCity).Distinct().ToList();
            bankNames = (from b in banks
                         select b.bankName).Distinct().ToList();
            bankAddresses = (from b in banks
                             select b.bankAddress).Distinct().ToList();

            DataContext = this;

            //comboBoxBankAddresses.DataContext = this;
            //comboBoxBankAgencies.DataContext = this;
            //comboBoxBankCities.DataContext = this;
            //comboBoxBankNames.DataContext = this;
            //comboBoxBankNumbers.DataContext = this;

            //StartProperties();



            InitializeComponent();
        }

        //private void StartProperties()
        //{
        //    List<Enums.BankAccount> banks = new List<Enums.BankAccount>();
        //    try
        //    {
        //        banks = blObject.GetAllBankAgencies().ToList();
        //    }
        //    catch
        //    {
        //        //banks = null;
        //        banks.Clear();
        //        banks.Add(new Enums.BankAccount()
        //        {
        //            accountNumber = 0,
        //            bankAddress = "empty",
        //            bankAgency = 0,
        //            bankCity = "empty",
        //            bankName = "empty",
        //            bankNumber = 0
        //        });
        //    }

        //    bankNumbers = (from b in banks
        //                   select b.bankNumber).ToList();
        //    Console.WriteLine("ta indo");
        //    bankNames = (from b in banks
        //                 select b.bankName).ToList();
        //    Console.WriteLine("ta indo");
        //    bankAgencies = (from b in banks
        //                    select b.bankAgency).ToList();
        //    Console.WriteLine("ta indo");
        //    bankAddresses = (from b in banks
        //                     select b.bankAddress).ToList();
        //    Console.WriteLine("ta indo");
        //    bankCities = (from b in banks
        //                  select b.bankCity).ToList();
        //    Console.WriteLine("acabou!");
        //}

        private void btnAddBankAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bankAccount = new Enums.BankAccount
                {
                    bankAddress = Convert.ToString(comboBoxBankAddresses.SelectedItem),
                    bankAgency = Convert.ToInt32(comboBoxBankAgencies.SelectedItem),
                    bankCity = Convert.ToString(comboBoxBankCities.SelectedItem),
                    bankName = Convert.ToString(comboBoxBankNames.SelectedItem),
                    bankNumber = Convert.ToInt64(comboBoxBankNumbers.SelectedItem),
                    accountNumber = Convert.ToInt64(txtBankAccountNumber.Text)

                    //bankNumber = long.Parse(txtBankNumber.Text),
                    //bankName = txtBankName.Text,
                    //bankAgency = int.Parse(txtBankAgency.Text),
                    //bankAddress = txtBankAddress.Text,
                    //bankCity = txtBankCity.Text,
                    //accountNumber = long.Parse(txtAccountNumber.Text)
                };

                Console.WriteLine(bankAccount);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                ClearAllFields();
            }
        }


        /// <summary>
        /// Verifies and dont accept non-numbers as input
        /// </summary>
        private void OnlyNumberAllowed(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //public bool DidFilledAllFields
        //{
        //    get
        //    {
        //        return (txtAccountNumber.Text.Length != 0 &&
        //                txtBankName.Text.Length != 0 &&
        //                txtBankAgency.Text.Length != 0 &&
        //                txtBankAddress.Text.Length != 0 &&
        //                txtBankCity.Text.Length != 0 &&
        //                txtBankNumber.Text.Length != 0);
        //    }
        //}

        private void ClearAllFields()
        {
            //txtAccountNumber.Clear();
            //txtBankAddress.Clear();
            //txtBankAgency.Clear();
            //txtBankCity.Clear();
            //txtBankName.Clear();
            //txtBankNumber.Clear();

            txtBankAccountNumber.Clear();
            comboBoxBankAddresses.SelectedIndex = -1;
            comboBoxBankAgencies.SelectedIndex = -1;
            comboBoxBankCities.SelectedIndex = -1;
            comboBoxBankNames.SelectedIndex = -1;
            comboBoxBankNumbers.SelectedIndex = -1;
        }
        

    }
}
