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
        MainWindow AF;

        bool IsEdit = false;
        Skates_hire EditSkates;
        private bool p1;
        private int p2;
        public SkatesWindow(MainWindow AF)
        {
            this.AF = AF;
            InitializeComponent();

        }
        public SkatesWindow(MainWindow AF, Skates_hire EditSkates)
        {
            IsEdit = true;
            this.AF = AF;
            this.EditSkates = EditSkates;
            InitializeComponent();
        }

        public SkatesWindow(bool p1, int p2)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
        }
        //public Employees GetNeedEmployees(List<Employees> List)
        //{
        //    string[] Names = ComboBoxAddEditEmployess.Text.Split(' ');
        //    for (int i = 0; i < List.Count; i++)
        //    {
        //        if (List[i].Name == Names[0])
        //        {
        //            return List[i];
        //        }
        //    }
        //    return List[0];
        //}

        private void ButtonAddEditSkates_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEdit)
            {
                if (textBlockAddEditSize.Text != string.Empty)
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
                        /*objectSkates.ID_skates_hire = objectMyDBContext.Skates_hire.Count();
                        objectSkates.ID_skates_hire++;
                        objectSkates.Size = textBlockAddEditSize.Text;
                        objectSkates.Time = textBlockAddEditTime.Text;
                        objectSkates.Type = textBlockAddEditType.Text;
                        objectSkates.Count = textBlockAddEditCount.Text;
                        objectSkates.ID_employees=
                         (WFAEntity.API.Employees)comboBoxAddEditEmployees.SelectedItem;*/
                        try
                        {
                            objectMyDBContext.Skates_hire.Add(objectSkates);
                            objectMyDBContext.SaveChanges();
                            MessageBox.Show("Коньки добавлены");
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
                    WFAEntity.API.Skates_hire objectSkates = new WFAEntity.API.Skates_hire();
                    objectSkates.Size = textBlockAddEditSize.Text;
                    objectSkates.Time = textBlockAddEditTime.Text;
                    objectSkates.Count = textBlockAddEditType.Text;
                    objectSkates.Type = textBlockAddEditCount.Text;
                    try
                    {
                        objectMyDBContext.Skates_hire.AddOrUpdate(EditSkates);
                        objectMyDBContext.SaveChanges();
                        MessageBox.Show("Клиент Редактирован");
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
            /*var tmp = MyDBContext.DBContext.Employees.ToList();
            List<string> Strings = new List<string>();
            for (int i = 0; i < tmp.Count; i++)
            {
                Strings.Add(tmp[i].Name);
            }
                ComboBoxAddEditEmployess.ItemsSource = Strings;*/
            using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
            {
                ComboBoxAddEditEmployess.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                ComboBoxAddEditEmployess.Text = "{Binging Name}";
            }
        }
    }
}
