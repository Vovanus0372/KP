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
    /// Логика взаимодействия для SheduleWindow.xaml
    /// </summary>
    public partial class SheduleWindow : Window
    {
       MainWindow AF;

        bool IsEdit = false;
        MK_schedule EditShedule;
        public SheduleWindow(MainWindow AF)
        {
            this.AF = AF;
            InitializeComponent();

        }
        public SheduleWindow(MainWindow AF, MK_schedule EditShedule)
        {
            IsEdit = true;
            this.AF = AF;
            this.EditShedule = EditShedule;
            InitializeComponent();
        }

        private void ButtonAddEditShedule_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEdit)
            {
                if (textBlockAddEditDate.Text != string.Empty)
                {
                    using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                    {
                        WFAEntity.API.MK_schedule objectShedule = new WFAEntity.API.MK_schedule(
                        textBlockAddEditDate.Text,
                        textBlockAddEditPrice.Text,
                        textBlockAddEditStart.Text,
                        textBlockAddEditEnd.Text,
                        (WFAEntity.API.Employees)ComboBoxAddEditEmployees.SelectedItem,
                        (WFAEntity.API.Other_services)ComboBoxAddEditServices.SelectedItem
                        );
                        try
                        {
                            objectMyDBContext.MK_schedule.Add(objectShedule);
                            objectMyDBContext.SaveChanges();
                            MessageBox.Show("Расписание добавлено");
                            this.DialogResult = true;
                            AF.ShowAll();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка!");
                    this.DialogResult = false;
                }
            }
            else
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
                {
                    WFAEntity.API.MK_schedule objectShedule = new WFAEntity.API.MK_schedule();
                    objectShedule.Date = textBlockAddEditDate.Text;
                    objectShedule.Price = textBlockAddEditPrice.Text;
                    objectShedule.Start_time = textBlockAddEditStart.Text;
                    objectShedule.End_time = textBlockAddEditEnd.Text;
                    try
                    {
                        objectMyDBContext.MK_schedule.AddOrUpdate(EditShedule);
                        objectMyDBContext.SaveChanges();
                        MessageBox.Show("Расписание Редактировано");
                        this.DialogResult = true;
                        AF.ShowAll();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var tmp = MyDBContext.DBContext.Other_services.ToList();
            //List<string> Strings = new List<string>();
            //for (int i = 0; i < tmp.Count; i++)
            //{
            //    Strings.Add(tmp[0].Name + " " + tmp[0].The_cost);
            //}
            //ComboBoxAddEditServices.ItemsSource = Strings;

            //var tmp1 = MyDBContext.DBContext.Employees.ToList();
            //List<string> Strings1 = new List<string>();
            //for (int i = 0; i < tmp1.Count; i++)
            //{
            //    Strings1.Add(tmp1[0].Name + " " + tmp1[0].Surname);
            //}
            //ComboBoxAddEditEmployees.ItemsSource = Strings1;
            using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
            {
                ComboBoxAddEditEmployees.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                ComboBoxAddEditEmployees.Text = "{Binging Name}";
                ComboBoxAddEditServices.ItemsSource = WFAEntity.API.DatabaseRequest.GetServices(objectMyDBContext);
                ComboBoxAddEditServices.Text = "{Binging Name}";
            }
        }
    }
}
