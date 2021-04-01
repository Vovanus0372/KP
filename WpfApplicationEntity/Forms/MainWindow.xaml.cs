﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WFAEntity.API;
using WpfApplicationEntity.Forms;

namespace WpfApplicationEntity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                            objectMyDBContext.Database.Create();
                            WFAEntity.API.Employees objectUser = new WFAEntity.API.Employees();
                            objectUser.Name = "user name";
                            objectUser.Login = "user";
                            objectUser.Password = "1111";
                            objectMyDBContext.Employees.Add(objectUser);
                            objectMyDBContext.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Создание базы данных");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Подключение к базе данных");
            }
            this.ShowAll();
        }
        #region Билеты
        private void addTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.TicketWindow g = new Forms.TicketWindow(this);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion

        
        public void ShowAll()
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext = 
                    new WFAEntity.API.MyDBContext())
                {
                    ClientGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetClients(objectMyDBContext);
                    EmployeesGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                    SkatesGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithTicket(objectMyDBContext);
                    SheduleGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithSchedule(objectMyDBContext);
                    TicketsGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetTicket(objectMyDBContext);
                    ServiceGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithServices(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Клиент", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Клиент
        private void addClientButton_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow ClientAddForm = new ClientWindow(this);
            ClientAddForm.ShowDialog();
        }
        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (Client)ClientGrid.SelectedItem;
            Client EditClient = MyDBContext.DBContext.Client.Find(itm.ID_client);
            ClientWindow workerAddForm = new ClientWindow(this, EditClient);
            workerAddForm.ShowDialog();
        }

        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (Client)ClientGrid.SelectedItem;
            var tmp = (
    from tmpClient in MyDBContext.DBContext.Client.ToList<Client>()
    where tmpClient.ID_client == itm.ID_client
    select tmpClient
          ).ToList();
            MyDBContext.DBContext.Client.Remove(tmp[0]);
            MyDBContext.DBContext.SaveChanges();
            ShowAll();
        }

        #endregion

        #region Сотрудники
        private void addEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow EmployeesAddForm = new EmployeesWindow(this);
            EmployeesAddForm.ShowDialog();
            
        }
        private void editEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (Employees)EmployeesGrid.SelectedItem;
            Employees EditEmployees = MyDBContext.DBContext.Employees.Find(itm.ID_employees);
            EmployeesWindow employeesAddForm = new EmployeesWindow(this, EditEmployees);
            employeesAddForm.ShowDialog();
        }
        private void deleteEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (Employees)EmployeesGrid.SelectedItem;
            var tmp = (
    from tmpEmployees in MyDBContext.DBContext.Employees.ToList<Employees>()
    where tmpEmployees.ID_employees == itm.ID_employees
    select tmpEmployees
          ).ToList();
            MyDBContext.DBContext.Employees.Remove(tmp[0]);
            MyDBContext.DBContext.SaveChanges();
            ShowAll();
        }
        #endregion

        #region Расписание
        private void addSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.SheduleWindow g = new Forms.SheduleWindow(this);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion

        #region Услуги
        private void addServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ServicesWindow g = new Forms.ServicesWindow(this);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion

        #region Коньки
        private void addSkatesButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.SkatesWindow g = new Forms.SkatesWindow(this);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion

        private void editSkatesButton_Click(object sender, RoutedEventArgs e)
        {
            if (SkatesGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < SkatesGrid.SelectedItems.Count; i++)
                {
                    WFAEntity.API.Skates_hire objectStudent = SkatesGrid.SelectedItems[i] as WFAEntity.API.Skates_hire;
                    if (objectStudent != null)
                    {
                        Forms.SkatesWindow g = new Forms.SkatesWindow(false, objectStudent.ID_skates_hire);
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }

       
    }
}