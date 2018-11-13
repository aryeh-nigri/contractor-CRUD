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

namespace PLWPF.Specializations
{
    /// <summary>
    /// Interaction logic for UserControlSpecializations.xaml
    /// </summary>
    public partial class UserControlSpecializations : UserControl
    {
        //COPIADO

        #region FIELDS

        private IBL blObject;

        #endregion


        #region CONSTRUCTOR

        public UserControlSpecializations()
        {
            InitializeComponent();
            blObject = FactoryBL.getBL();

            try
            {
                DataContext = blObject.GetAllSpecializationsById();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }

            RefreshDataGrid();
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

        private void btnAddSpec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var spec = new Specialization()
                {
                    Id = Convert.ToInt32(txtIDSpec.Text),
                    Name = txtNameSpec.Text,
                    School = txtSchoolSpec.Text,
                    MinRate = Convert.ToInt32(txtMinRateSpec.Text),
                    MaxRate = Convert.ToInt32(txtMaxRateSpec.Text),
                    //Discipline = (Enums.Discipline)Enum.Parse(typeof(Enums.Discipline), comboBoxDiscipline.SelectedItem.ToString())
                    Discipline = (Enums.Discipline)comboBoxDiscipline.SelectedIndex
                };

                blObject.AddSpecialization(spec);
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

        private void btnRemoveSpec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.RemoveSpecialization(int.Parse(labelIDSpec.Content.ToString()));
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

        private void btnUpdateSpec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.UpdateSpecialization(int.Parse(labelIDSpec.Content.ToString()),
                                              new Specialization()
                                              {
                                                  Id = Convert.ToInt32(txtIDSpec.Text),
                                                  Name = txtNameSpec.Text,
                                                  School = txtSchoolSpec.Text,
                                                  MinRate = Convert.ToInt32(txtMinRateSpec.Text),
                                                  MaxRate = Convert.ToInt32(txtMaxRateSpec.Text),
                                                  Discipline = (Enums.Discipline)comboBoxDiscipline.SelectedIndex
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

        private void dataGridSpecs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Specialization spec = dataGridSpecs.SelectedItem as Specialization;

            if (spec != null)
            {
                labelIDSpec.Content = spec.Id.ToString();
                dockPanelID.Visibility = Visibility.Visible;

                txtIDSpec.Text = spec.Id.ToString();
                txtNameSpec.Text = spec.Name;
                txtSchoolSpec.Text = spec.School;
                txtMinRateSpec.Text = spec.MinRate.ToString();
                txtMaxRateSpec.Text = spec.MaxRate.ToString();
                comboBoxDiscipline.SelectedIndex = (int)spec.Discipline;

                labelMode.Content = "Editing Mode is ON";
                dockPanelInputID.Visibility = Visibility.Collapsed;
                btnAddSpec.IsEnabled = false;
            }
            else
            {
                ClearAllFields();
                e.Handled = true;
            }
        }

        private void dataGridSpecs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllFields();
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
            txtIDSpec.Clear();
            txtNameSpec.Clear();
            txtSchoolSpec.Clear();
            txtMinRateSpec.Clear();
            txtMaxRateSpec.Clear();
            comboBoxDiscipline.SelectedIndex = -1;

            dockPanelID.Visibility = Visibility.Collapsed;
            dockPanelInputID.Visibility = Visibility.Visible;
            labelMode.Content = "Fill all the fields to add a Specialization";
            btnAddSpec.IsEnabled = true;
        }


        /// <summary>
        /// Verify if all inputs fields are filled
        /// </summary>
        private bool DidFilledAllFields
        {
            get
            {
                //return (txtIdEmployee.Text.Length != 0 &&
                //        txtFirstNameEmployee.Text.Length != 0 &&
                //        txtLastNameEmployee.Text.Length != 0 &&
                //        txtTelephoneEmployee.Text.Length != 0 &&
                //        txtAddressEmployee.Text.Length != 0 &&
                //        comboBoxFormationEmployee.SelectedIndex != -1);
                return false;
            }
        }

        /// <summary>
        /// Refresh the values of our current grid
        /// </summary>
        private void RefreshDataGrid()
        {
            dataGridSpecs.ItemsSource = null;
            try
            {
                dataGridSpecs.ItemsSource = blObject.GetAllSpecializationsById();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        #endregion

    }
}
