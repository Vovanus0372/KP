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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (WFAEntity.API.MyDBContext MyDBContext = new WFAEntity.API.MyDBContext())
            {
                var tmp = (
                    from tmpEmployees in MyDBContext.Employees.ToList<Employees>()
                    where tmpEmployees.Login.CompareTo(Log.Text) == 0 && tmpEmployees.Password.CompareTo(Pas.Text) == 0
                    select tmpEmployees
                    ).ToList();
                if (tmp.Count > 0)
                {
                    if (tmp[0].Login == "Администратор")
                    {
                        Forms.SheduleWindow g = new Forms.SheduleWindow(new MainWindow());
                        if (g.ShowDialog() == true)        
                            this.ShowAll();
                    }
                    else if (tmp[0].Login == "Кассир")
                    {
                        SkatesWindow g = new SkatesWindow(new MainWindow());
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                    else if (tmp[0].Login == "Посетитель")
                    {
                        MainWindow g = new MainWindow();
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибочка");
                }
            }
        }
        private void ShowAll()
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext = new WFAEntity.API.MyDBContext())
                {
                    //var list = WpfApplicationEntity.API.DatabaseRequest.GetMatType(objectMyDBContext);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Тип материалов", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
