using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using WFAEntity.API;
using WpfApplicationEntity.Forms;
using System.Data.Entity.Migrations;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для SkatesWindow.xaml
    /// </summary>
    partial class ServicesWindow : Window
    {

        private readonly bool add_edit;
        private readonly int id;

        public ServicesWindow()
        {
            InitializeComponent();
        }
        public ServicesWindow(bool add_edit, int id = 0)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;

            using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
            {
                if (this.add_edit == false)
                {
                    WFAEntity.API.Other_services objectServices = WFAEntity.API.DatabaseRequest.GetServicesById(objectMyDBContext, this.id);
                    textBlockAddEditName.Text = objectServices.Name;
                    textBlockAddEditThe_cost.Text = objectServices.The_cost;

                    ButtonAddEditServices.Content = "Изменить";
                }
            }
        }
        private bool IsDataCorrect()
        {
            return (textBlockAddEditName.Text != string.Empty) ||
                (textBlockAddEditThe_cost.Text != string.Empty);
        }
        private void ButtonAddEditServices_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsDataCorrect() == true)
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
                {
                    WFAEntity.API.Other_services objectServices = new WFAEntity.API.Other_services(
                    textBlockAddEditName.Text,
                    textBlockAddEditThe_cost.Text,
                     (WFAEntity.API.Employees)ComboBoxAddEditName.SelectedItem
                        );
                    if (this.add_edit == true)
                    {
                        objectMyDBContext.Other_services.Add(objectServices);
                    }
                    else
                    {
                        objectServices.ID_other_services = WFAEntity.API.DatabaseRequest.GetServicesById(objectMyDBContext, this.id).ID_other_services;
                        WFAEntity.API.Other_services objectStudentFromDataBase = new WFAEntity.API.Other_services();
                        objectStudentFromDataBase = WFAEntity.API.DatabaseRequest.GetServicesById(objectMyDBContext, this.id);
                        objectMyDBContext.Entry(objectStudentFromDataBase).CurrentValues.SetValues(objectServices);
                        objectMyDBContext.SaveChanges();
                    }
                    objectMyDBContext.SaveChanges();
                    this.DialogResult = true;
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
            {
                ComboBoxAddEditName.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                ComboBoxAddEditName.Text = "{Binging Name}";
            }
        }
    }
}
