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
using System.Data.Entity.Migrations;
using WFAEntity.API;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        MainWindow AF;
        bool IsEdit = false;
        Employees EditEmployees;
        public EmployeesWindow(MainWindow AF)
        {
            this.AF = AF;
            InitializeComponent();
        }
        public EmployeesWindow(MainWindow AF, Employees EditEmployees)
        {
            IsEdit = true;
            this.AF = AF;
            this.EditEmployees = EditEmployees;
            InitializeComponent();

        }
        private void ButtonAddEditEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEdit)
            {
                if (textBlockAddEditSurname.Text != string.Empty)
                {
                    using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                    {
                        WFAEntity.API.Employees objectEmployees = new WFAEntity.API.Employees();
                        objectEmployees.ID_employees = objectMyDBContext.Employees.Count();
                        objectEmployees.ID_employees++;
                        objectEmployees.Surname = textBlockAddEditSurname.Text;
                        objectEmployees.Name = textBlockAddEditName.Text;
                        objectEmployees.Patronymic = textBlockAddEditPatronymic.Text;
                        objectEmployees.Address = textBlockAddEditAddress.Text;
                        objectEmployees.Telephone = textBlockAddEditTelephone.Text;
                        objectEmployees.Position = textBlockAddEditPosition.Text;
                        objectEmployees.Date = textBlockAddEditDate.Text;
                        objectEmployees.Password = textBlockAddEditPassword.Text;
                        objectEmployees.Login = textBlockAddEditLogin.Text;
                        try
                        {
                            objectMyDBContext.Employees.Add(objectEmployees);
                            objectMyDBContext.SaveChanges();
                            MessageBox.Show("Сотрудник добавлен");
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
                    WFAEntity.API.Employees objectEmployees = new WFAEntity.API.Employees();
                    EditEmployees.Surname = textBlockAddEditSurname.Text;
                    EditEmployees.Name = textBlockAddEditName.Text;
                    EditEmployees.Patronymic = textBlockAddEditPatronymic.Text;
                    EditEmployees.Address = textBlockAddEditAddress.Text;
                    EditEmployees.Telephone = textBlockAddEditTelephone.Text;
                    EditEmployees.Position = textBlockAddEditPosition.Text;
                    EditEmployees.Date = textBlockAddEditDate.Text;
                    EditEmployees.Login = textBlockAddEditLogin.Text;
                    EditEmployees.Password = textBlockAddEditPassword.Text;
                    try
                    {
                        objectMyDBContext.Employees.AddOrUpdate(EditEmployees);
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
    }
}
