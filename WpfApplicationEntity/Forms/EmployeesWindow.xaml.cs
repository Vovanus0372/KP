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

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для GroupAddEditWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        private bool add_edit;
        private int id;
        public EmployeesWindow()
        {
            InitializeComponent();
        }
        public EmployeesWindow(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                {
                    WFAEntity.API.Employees objectEmployees = WFAEntity.API.DatabaseRequest.GetEmployeesById(objectMyDBContext, this.id);
                    objectEmployees.Surname = objectEmployees.Surname;
                    objectEmployees.Name = objectEmployees.Name;
                    objectEmployees.Patronymic = objectEmployees.Patronymic;
                    objectEmployees.Address = objectEmployees.Address;
                    objectEmployees.Telephone = objectEmployees.Telephone;
                    objectEmployees.Position = objectEmployees.Position;
                    objectEmployees.Date = objectEmployees.Date;
                    objectEmployees.Password  = objectEmployees.Password;
                    objectEmployees.Login = objectEmployees.Login;

                }
                ButtonAddEditEmployees.Content = "Изменить";
            }
        }
        private bool IsDataCorrcet()
        {
            if (textBlockAddEditSurname.Text != string.Empty ||textBlockAddEditName.Text != string.Empty || textBlockAddEditPatronymic.Text != string.Empty || textBlockAddEditAddress.Text != string.Empty || textBlockAddEditTelephone.Text != string.Empty || textBlockAddEditPosition.Text != string.Empty || textBlockAddEditDate.Text != string.Empty || textBlockAddEditPassword.Text != string.Empty || textBlockAddEditLogin.Text != string.Empty);
                return true;
            return false;
             
        }
        private void ButtonAddEditEmployees_Click(object sender, RoutedEventArgs e)
        {
            WFAEntity.API.Employees objectEmployees = new WFAEntity.API.Employees();
            if (this.add_edit == true)
            {
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
                    if (this.IsDataCorrcet() == true)
                    {
                        using (WFAEntity.API.MyDBContext objectMyDBContext =
                                new WFAEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Employees.Add(objectEmployees);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Группа добавлена");
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Ввод данных", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Заполните все поля верно", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    if (this.IsDataCorrcet())
                    {
                        using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                        {
                            WFAEntity.API.Employees objectEmpl = new WFAEntity.API.Employees();
                            objectEmpl = WFAEntity.API.DatabaseRequest.GetEmployeesById(objectMyDBContext, this.id);
                            objectEmpl.Surname = textBlockAddEditSurname.Text;
                            objectEmpl.Name = textBlockAddEditName.Text;
                            objectEmpl.Patronymic = textBlockAddEditPatronymic.Text;
                            objectEmpl.Address = textBlockAddEditAddress.Text;
                            objectEmpl.Telephone = textBlockAddEditTelephone.Text;
                            objectEmpl.Position = textBlockAddEditPosition.Text;
                            objectEmpl.Date = textBlockAddEditDate.Text;
                            objectEmpl.Password = textBlockAddEditPassword.Text;
                            objectEmpl.Login = textBlockAddEditLogin.Text;
                            objectMyDBContext.Entry(objectEmpl).State = System.Data.Entity.EntityState.Modified;
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Группа изменена");
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Ввод данных", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
