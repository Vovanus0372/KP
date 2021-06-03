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
    partial class SheduleWindow : Window
    {

        private readonly bool add_edit;
        private readonly int id;

        public SheduleWindow()
        {
            InitializeComponent();
        }
        public SheduleWindow(bool add_edit, int id = 0)
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
                    WFAEntity.API.MK_schedule objectShedule = WFAEntity.API.DatabaseRequest.GetSheduleById(objectMyDBContext, this.id);
                    textBlockAddEditDate.Text = objectShedule.Date;
                    textBlockAddEditPrice.Text = objectShedule.Price;
                    textBlockAddEditStart.Text = objectShedule.Start_time;
                    textBlockAddEditEnd.Text = objectShedule.End_time;

                    ButtonAddEditShedule.Content = "Изменить";
                }
            }
        }
        private bool IsDataCorrect()
        {
            return (textBlockAddEditDate.Text != string.Empty) ||
                (textBlockAddEditPrice.Text != string.Empty) ||
                (textBlockAddEditStart.Text != string.Empty) ||
                (textBlockAddEditEnd.Text != string.Empty);
        }
        private void ButtonAddEditShedule_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsDataCorrect() == true)
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
                    if (this.add_edit == true)
                    {
                        objectMyDBContext.MK_schedule.Add(objectShedule);
                    }
                    else
                    {
                        objectShedule.ID_MK_schedule = WFAEntity.API.DatabaseRequest.GetSheduleById(objectMyDBContext, this.id).ID_MK_schedule;
                        WFAEntity.API.MK_schedule objectStudentFromDataBase = new WFAEntity.API.MK_schedule();
                        objectStudentFromDataBase = WFAEntity.API.DatabaseRequest.GetSheduleById(objectMyDBContext, this.id);
                        objectMyDBContext.Entry(objectStudentFromDataBase).CurrentValues.SetValues(objectShedule);
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
                ComboBoxAddEditServices.ItemsSource = WFAEntity.API.DatabaseRequest.GetServices(objectMyDBContext);
                ComboBoxAddEditServices.Text = "{Binging Name}";
                ComboBoxAddEditEmployees.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                ComboBoxAddEditEmployees.Text = "{Binging Name}";
            }
        }
    }
}
