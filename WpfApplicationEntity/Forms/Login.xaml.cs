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
     partial class Login : Window
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
                    if (tmp[0].Position == "Администратор")
                    {
                        EmployeeWindow g = new EmployeeWindow();
                        if (g.ShowDialog() == true)        
                            this.ShowAll();
                    }
                    else if (tmp[0].Position == "Кассир")
                    {
                        CashierWindow g = new CashierWindow();
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                    else if (tmp[0].Position == "Посетитель")
                    {
                        MainWindow g = new MainWindow();
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
                else
                {
                    MessageBox.Show("Логин или пароль неправильны! Попробуйте еще раз");
                }
            }
        }
        private void ShowAll()
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext = new WFAEntity.API.MyDBContext())
                {
                   // var list = WFAEntity.API.DatabaseRequest.GetMatType(objectMyDBContext);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Тип материалов", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext = new WFAEntity.API.MyDBContext())
                {
                    try
                    {
                        if (objectMyDBContext.Database.Exists() == false)
                        {
                            WFAEntity.API.DatabaseRequest.Fill();
                            objectMyDBContext.Database.Create();
                            WFAEntity.API.Employees objectUser = new WFAEntity.API.Employees();
                            objectUser.Name = "user name";
                            objectMyDBContext.Employees.Add(objectUser);
                            objectMyDBContext.SaveChanges();
                        }
                        WFAEntity.API.DatabaseRequest.Fill();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("База успешно создана", "Создание базы данных");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Были внесены изменени в БД, удалите БД и повторите попытку", "Подключение к базе данных");
            }

            this.ShowAll();
        }
    }
}
