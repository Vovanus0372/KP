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
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class ServicesWindow : Window
    {
        MainWindow AF;

        bool IsEdit = false;
        Other_services EditServices;
        private bool p1;
        private int p2;
        public ServicesWindow(MainWindow AF)
        {
            this.AF = AF;
            InitializeComponent();

        }
        public ServicesWindow(MainWindow AF, Other_services EditServices)
        {
            IsEdit = true;
            this.AF = AF;
            this.EditServices = EditServices;
            InitializeComponent();
        }
        private void ButtonAddEditServices_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEdit)
            {
                if (textBlockAddEditName.Text != string.Empty)
                {
                    WFAEntity.API.Other_services objectServices = new WFAEntity.API.Other_services(
                          textBlockAddEditName.Text,
                          textBlockAddEditThe_cost.Text,
                          (WFAEntity.API.Employees)ComboBoxAddEditName.SelectedItem
                      );

                    try
                    {
                        using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Other_services.Add(objectServices);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Услуга добавлена");
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка!");
                    this.DialogResult = false;
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
