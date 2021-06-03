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
    partial class SkatesWindow : Window
    {

        private readonly bool add_edit;
        private readonly int id;

        public SkatesWindow()
        {
            InitializeComponent();
        }
        public SkatesWindow(bool add_edit, int id = 0)
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
                    WFAEntity.API.Skates_hire objectStudent = WFAEntity.API.DatabaseRequest.GetSkatesById(objectMyDBContext, this.id);
                    textBlockAddEditSize.Text = objectStudent.Size;
                    textBlockAddEditTime.Text = objectStudent.Time;
                    textBlockAddEditCount.Text = objectStudent.Count;
                    textBlockAddEditType.Text = objectStudent.Type;

                    ButtonAddEditSkates.Content = "Изменить";
                }
            }
        }
        private bool IsDataCorrect()
        {
            return (textBlockAddEditSize.Text != string.Empty) ||
                (textBlockAddEditTime.Text != string.Empty) ||
                (textBlockAddEditCount.Text != string.Empty);
        }
        private void ButtonAddEditSkates_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsDataCorrect() == true)
            {
                    using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                    {
                        WFAEntity.API.Skates_hire objectSkates = new WFAEntity.API.Skates_hire(
                        textBlockAddEditSize.Text,
                        textBlockAddEditTime.Text,
                        textBlockAddEditCount.Text,
                        textBlockAddEditType.Text,
                         (WFAEntity.API.Employees)ComboBoxAddEditEmployess.SelectedItem
                            );
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Skates_hire.Add(objectSkates);
                        }
                        else
                        {
                            objectSkates.ID_skates_hire = WFAEntity.API.DatabaseRequest.GetSkatesById(objectMyDBContext, this.id).ID_skates_hire;
                            WFAEntity.API.Skates_hire objectStudentFromDataBase = new WFAEntity.API.Skates_hire();
                            objectStudentFromDataBase = WFAEntity.API.DatabaseRequest.GetSkatesById(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectStudentFromDataBase).CurrentValues.SetValues(objectSkates);
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
                ComboBoxAddEditEmployess.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                ComboBoxAddEditEmployess.Text = "{Binging Name}";
            }
        }
    }
}
