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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WFAEntity.API;
using WpfApplicationEntity.Forms;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для CashierWindow.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        public CashierWindow()
        {
            InitializeComponent();
        }
        public void ShowAll()
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    SkatesGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithTicket(objectMyDBContext);
                    ServiceGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithServices(objectMyDBContext);
                    TicketsGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetClientWithTicket(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Клиент", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
       
       
        private void addSkatesButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.SkatesWindow g = new Forms.SkatesWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        private void deleteSkatesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (DataGridRow)SkatesGrid.ItemContainerGenerator.ContainerFromIndex(SkatesGrid.SelectedIndex);
                //var SelectedService = row.Item as ShowServiceStruct;
                var itm = (WFAEntity.API.DatabaseRequest.NewSkates_hire)SkatesGrid.SelectedItem;
                var tmp = (
        from tmpService in MyDBContext.DBContext.Skates_hire.ToList<Skates_hire>()
        where tmpService.ID_skates_hire.CompareTo(itm.ID_skates_hire) == 0
        select tmpService
              ).ToList();
                MyDBContext.DBContext.Skates_hire.Remove(tmp[0]);
                MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void editSkatesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (WFAEntity.API.DatabaseRequest.NewSkates_hire)SkatesGrid.SelectedItem;
                Skates_hire EditSkates = MyDBContext.DBContext.Skates_hire.Find(itm.ID_skates_hire);
                SkatesWindow skatesAddForm = new SkatesWindow(false, EditSkates.ID_skates_hire);
                if (skatesAddForm.ShowDialog() == true)
                    this.ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ServicesWindow g = new Forms.ServicesWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        private void deleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (DataGridRow)ServiceGrid.ItemContainerGenerator.ContainerFromIndex(ServiceGrid.SelectedIndex);
                //var SelectedService = row.Item as ShowServiceStruct;
                var itm = (WFAEntity.API.DatabaseRequest.NewOther_services)ServiceGrid.SelectedItem;
                var tmp = (
        from tmpService in MyDBContext.DBContext.Other_services.ToList<Other_services>()
        where tmpService.ID_other_services.CompareTo(itm.ID_other_services) == 0
        select tmpService
              ).ToList();
                MyDBContext.DBContext.Other_services.Remove(tmp[0]);
                MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void editServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (WFAEntity.API.DatabaseRequest.NewOther_services)ServiceGrid.SelectedItem;
                Other_services EditService = MyDBContext.DBContext.Other_services.Find(itm.ID_other_services);
                ServicesWindow ServiceAddForm = new ServicesWindow(false, EditService.ID_other_services);
                if (ServiceAddForm.ShowDialog() == true)
                    this.ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.TicketWindow g = new Forms.TicketWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        private void deleteTicketButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (DataGridRow)TicketsGrid.ItemContainerGenerator.ContainerFromIndex(TicketsGrid.SelectedIndex);
                //var SelectedService = row.Item as ShowServiceStruct;
                var itm = (WFAEntity.API.DatabaseRequest.NewTicket)TicketsGrid.SelectedItem;
                var tmp = (
        from tmpTicket in MyDBContext.DBContext.Ticket.ToList<Ticket>()
        where tmpTicket.ID_Ticket.CompareTo(itm.ID_Ticket) == 0
        select tmpTicket
              ).ToList();
                MyDBContext.DBContext.Ticket.Remove(tmp[0]);
                MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void editTicketButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (WFAEntity.API.DatabaseRequest.NewTicket)TicketsGrid.SelectedItem;
                Ticket EditTicket = MyDBContext.DBContext.Ticket.Find(itm.ID_Ticket);
                TicketWindow TicketAddForm = new TicketWindow(false, EditTicket.ID_Ticket);
                if (TicketAddForm.ShowDialog() == true)
                    this.ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
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
                            //WFAEntity.API.Employees objectUser = new WFAEntity.API.Employees();
                            //objectUser.Name = "user name";
                            //objectUser.Login = "user";
                            //objectUser.Password = "1111";
                            //objectMyDBContext.Employees.Add(objectUser);
                            objectMyDBContext.SaveChanges();


                        }
                        WFAEntity.API.DatabaseRequest.Fill();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Создание базы данных");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Подключение к базе данных");
            }

            this.ShowAll();
        }
    }
}
