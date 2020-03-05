
//-----------------------------------------------------------------------


// <copyright file="MainWindow.xaml.cs" company="Siemens">


//     Copyright (c) Siemens. All rights reserved.
//     Author: Noah Eggenberger
//     Date: 05.09.2019
//     Description: Code Behind for MainWindow.xaml

// </copyright>


//-----------------------------------------------------------------------


using AddressManager.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace AddressManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constructor

        public MainWindow()
        {
            InitializeComponent();
            _addressList = new BindingList<Address>();
            Address_ListView.ItemsSource = _addressList;

            List<string> filterItemList = new List<string>()
            {
                "firstname",
                "lastname",
                "street",
                /* ToDo: Filter nach ZIP / PLZ */
                "city"
            };

            Filter_ComboBox.ItemsSource = filterItemList;
            Filter_ComboBox.SelectedItem = "Firstname";
        }

        #endregion

        #region fields

        private BindingList<Address> _filteredList;

        private BindingList<Address> _addressList;

        #endregion

        #region methods

        private void Add_Address_Button_Click(object sender, RoutedEventArgs e)
        {
            /* ToDo: Erstelle ein neues Address Objekt und füge es zur _addressList hinzu */
            Address_ListView.ItemsSource = _addressList;
            ClearForm();
        }

        private void Search_Address_Button_Cick(object sender, RoutedEventArgs e)
        {
            var searchTerm = Search_TextBox.Text;
            var filter = Filter_ComboBox.SelectedValue;
            
            switch(filter)
            {
                case "Firstname":
                    _filteredList = new BindingList<Address>(_addressList.Where(a => a.Firstname.Contains(searchTerm)).ToList());
                    break;
                case "Lastname":
                    _filteredList = new BindingList<Address>(_addressList.Where(a => a.Lastname.Contains(searchTerm)).ToList());
                    break;
                case "Street":
                    _filteredList = new BindingList<Address>(_addressList.Where(a => a.Street.Contains(searchTerm)).ToList());
                    break;
                /* ToDo: Filter für ZIP / PLZ hinzufügen*/
                case "City":
                    _filteredList = new BindingList<Address>(_addressList.Where(a => a.City.Contains(searchTerm)).ToList());
                    break;
                default:
                    MessageBox.Show("Der Filterwert '"  + filter + "' wurde nicht gefunden. Tipp: Case Sensitive");
                    break;
            }

            Address_ListView.ItemsSource = _filteredList;
        }

        private void Open_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Addresses files (*.adr)|*.adr";

            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream stream = File.OpenRead(openFileDialog.FileName + ".adr"))
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(BindingList<Address>));
                        _addresList = (BindingList<Address>)serializer.Deserialize(stream);
                        Address_ListView.ItemsSource = _addressList;
                    }
                    catch
                    {
                        MessageBox.Show("Could not open addresses!");
                    }
                }
            }
        }

        private void Save_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Addresses file (*.adr)|*.adr";

            if (saveFileDialog.ShowDialog() == true)
            {

                using (FileStream stream = File.Open(saveFileDialog.FileName, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(BindingList<Address>));


                    try
                    {
                        serializer.Serialize(stream, _addressList);
                    }
                    catch
                    {
                        MessageBox.Show("Could not save address!");
                        File.Delete(saveFileDialog.FileName);
                    }
                }
            }
        }

        private void Address_Text_Changed(object sender, RoutedEventArgs e)
        {
            Add_Address_Button.IsEnabled = !string.IsNullOrEmpty(Firstname_TextBox.Text) &&
                !string.IsNullOrEmpty(Lastname_TextBox.Text) &&
                !string.IsNullOrEmpty(Street_TextBox.Text) &&
                /* ToDo: Validierung Zip / PLZ TextBox */
                !string.IsNullOrEmpty(City_TextBox.Text);
        }

        private void ClearForm()
        {
            Firstname_TextBox.Text = "";
            Lastname_TextBox.Text = "";
            Street_TextBox.Text = "";
            /* ToDo: Zip / Plz TextBox leeren */
            City_TextBox.Text = "";
        }

        private void Search_TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string text = Search_TextBox.Text;

            if (string.IsNullOrEmpty(text))
                Address_ListView.ItemsSource = _addressList;
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
