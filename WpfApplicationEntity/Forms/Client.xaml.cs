using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
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
    public partial class MainWindow : System.Windows.Window
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
        #region Билеты
        private void addTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.TicketWindow g = new Forms.TicketWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void editTicketButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (WFAEntity.API.DatabaseRequest.NewTicket)TicketsGrid.SelectedItem;
            Ticket EditTicket = MyDBContext.DBContext.Ticket.Find(itm.ID_Ticket);
            TicketWindow TicketAddForm = new TicketWindow(false, EditTicket.ID_Ticket);
            if (TicketAddForm.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteTicketButton_Click(object sender, RoutedEventArgs e)
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
        #endregion

        
        public void ShowAll()
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext = 
                    new WFAEntity.API.MyDBContext())
                {
                    //ClientGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetClients(objectMyDBContext);
                    //EmployeesGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                    SkatesGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithTicket(objectMyDBContext);
                    SheduleGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithSchedule(objectMyDBContext);
                    TicketsGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetClientWithTicket(objectMyDBContext);
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
            ClientWindow ClientAddForm = new ClientWindow();
            ClientAddForm.ShowDialog();
        }
        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            //var itm = (Client)ClientGrid.SelectedItem;
            //Client EditClient = MyDBContext.DBContext.Client.Find(itm.ID_Client);
            //ClientWindow workerAddForm = new ClientWindow(false, EditClient);
            //workerAddForm.ShowDialog();
        }

        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
    //        var itm = (Client)ClientGrid.SelectedItem;
    //        var tmp = (
    //from tmpClient in MyDBContext.DBContext.Client.ToList<Client>()
    //where tmpClient.ID_Client == itm.ID_Client
    //select tmpClient
    //      ).ToList();
    //        MyDBContext.DBContext.Client.Remove(tmp[0]);
    //        MyDBContext.DBContext.SaveChanges();
    //        ShowAll();
        }

        #endregion

        #region Сотрудники
        private void addEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
          //  EmployeesWindow EmployeesAddForm = new EmployeesWindow(true);
            //EmployeesAddForm.ShowDialog();
            
        }
        private void editEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            //var itm = (Employees)EmployeesGrid.SelectedItem;
            //Employees EditEmployees = MyDBContext.DBContext.Employees.Find(itm.ID_employees);
           // EmployeesWindow employeesAddForm = new EmployeesWindow(false, EditEmployees.ID_employees);
           // if (employeesAddForm.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
    //        var itm = (Employees)EmployeesGrid.SelectedItem;
    //        var tmp = (
    //from tmpEmployees in MyDBContext.DBContext.Employees.ToList<Employees>()
    //where tmpEmployees.ID_employees == itm.ID_employees
    //select tmpEmployees
    //      ).ToList();
    //        MyDBContext.DBContext.Employees.Remove(tmp[0]);
    //        MyDBContext.DBContext.SaveChanges();
    //        ShowAll();
        }
        #endregion

        #region Расписание
        private void addSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.SheduleWindow g = new Forms.SheduleWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void editSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (WFAEntity.API.DatabaseRequest.NewMK_schedule)SheduleGrid.SelectedItem;
            MK_schedule EditShedule = MyDBContext.DBContext.MK_schedule.Find(itm.ID_MK_schedule);
            SheduleWindow ServiceAddForm = new SheduleWindow(false, EditShedule.ID_MK_schedule);
            if (ServiceAddForm.ShowDialog() == true)
                this.ShowAll();
        }

        private void deleteSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)SheduleGrid.ItemContainerGenerator.ContainerFromIndex(SheduleGrid.SelectedIndex);
            //var SelectedService = row.Item as ShowServiceStruct;
            var itm = (WFAEntity.API.DatabaseRequest.NewMK_schedule)SheduleGrid.SelectedItem;
            var tmp = (
    from tmpShedule in MyDBContext.DBContext.MK_schedule.ToList<MK_schedule>()
    where tmpShedule.ID_MK_schedule.CompareTo(itm.ID_MK_schedule) == 0
    select tmpShedule
          ).ToList();
            MyDBContext.DBContext.MK_schedule.Remove(tmp[0]);
            MyDBContext.DBContext.SaveChanges();
            ShowAll();
        }
        #endregion

        #region Услуги
        private void addServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ServicesWindow g = new Forms.ServicesWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void editServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (WFAEntity.API.DatabaseRequest.NewOther_services)ServiceGrid.SelectedItem;
            Other_services EditService = MyDBContext.DBContext.Other_services.Find(itm.ID_other_services);
            ServicesWindow ServiceAddForm = new ServicesWindow(false, EditService.ID_other_services);
            if (ServiceAddForm.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteServiceButton_Click(object sender, RoutedEventArgs e)
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
        #endregion

        #region Коньки
        private void addSkatesButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.SkatesWindow g = new Forms.SkatesWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void editSkatesButton_Click(object sender, RoutedEventArgs e)
        {
            var itm = (WFAEntity.API.DatabaseRequest.NewSkates_hire)SkatesGrid.SelectedItem;
            Skates_hire EditSkates = MyDBContext.DBContext.Skates_hire.Find(itm.ID_skates_hire);
            SkatesWindow skatesAddForm = new SkatesWindow(false, EditSkates.ID_skates_hire);
            if (skatesAddForm.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteSkatesButton_Click(object sender, RoutedEventArgs e)
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
        #endregion    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Дата";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Цена";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Время начала";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Время конца";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Другие услуги";
            workSheet.Cells[1, 6].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 6].Interior.ColorIndex = 17;
            workSheet.Cells[1, 6] = "Сотрудник";

            int i = 2;
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    List<WFAEntity.API.DatabaseRequest.NewMK_schedule> Shedule = WFAEntity.API.DatabaseRequest.GetEmployeesWithSchedule(objectMyDBContext).ToList(); //задаём для кого будем делать отчёт
                    foreach (WFAEntity.API.DatabaseRequest.NewMK_schedule shedule in Shedule)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = shedule.Date;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = shedule.Price;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = shedule.Start_time;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = shedule.End_time;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = shedule.Other_services;
                        workSheet.Cells[i, 6].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 6] = shedule.Employees;

                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\График массового катания.xls";                                        //путь к отчёту и название
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                    MessageBox.Show("Отчет успешно создан", "Отчет", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Стоимость";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Сотрудник";

            int i = 2;
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    List<WFAEntity.API.DatabaseRequest.NewOther_services> Services = WFAEntity.API.DatabaseRequest.GetEmployeesWithServices(objectMyDBContext).ToList(); //задаём для кого будем делать отчёт
                    foreach (WFAEntity.API.DatabaseRequest.NewOther_services services in Services)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = services.Name;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = services.The_cost;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = services.Employees;

                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Другие услуги.xls";                                        //путь к отчёту и название
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                    MessageBox.Show("Отчет успешно создан", "Отчет", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Размер";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Время";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Тип";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Количество";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Сотрудник";

            int i = 2;
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    List<WFAEntity.API.DatabaseRequest.NewSkates_hire> Skates = WFAEntity.API.DatabaseRequest.GetEmployeesWithTicket(objectMyDBContext).ToList(); //задаём для кого будем делать отчёт
                    foreach (WFAEntity.API.DatabaseRequest.NewSkates_hire skates in Skates)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = skates.Size;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = skates.Time;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = skates.Type;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = skates.Count;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = skates.Employees;

                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Коньки на прокат.xls";                                        //путь к отчёту и название
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                    MessageBox.Show("Отчет успешно создан", "Отчет", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void reportTicketButton(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Стоимость";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Количество";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Статус";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Клиент";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "График МК";
            workSheet.Cells[1, 6].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 6].Interior.ColorIndex = 17;
            workSheet.Cells[1, 6] = "Услуга";
            workSheet.Cells[1, 7].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 7].Interior.ColorIndex = 17;
            workSheet.Cells[1, 7] = "Коньки";

            int i = 2;
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    List<WFAEntity.API.DatabaseRequest.NewTicket> Ticket = WFAEntity.API.DatabaseRequest.GetClientWithTicket(objectMyDBContext).ToList(); //задаём для кого будем делать отчёт
                    foreach (WFAEntity.API.DatabaseRequest.NewTicket ticket in Ticket)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = ticket.Cost;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = ticket.Amount;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = ticket.Status;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = ticket.Client;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = ticket.MK_schedule;
                        workSheet.Cells[i, 6].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 6] = ticket.Other_services;
                        workSheet.Cells[i, 7].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 7] = ticket.Skates_hire;

                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Билеты.xls";                                        //путь к отчёту и название
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                    MessageBox.Show("Отчет успешно создан", "Отчет", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       
        private void SearchTicketButton(object sender, RoutedEventArgs e)
        {

        }



       

       

       

      

       
     


    }
}
     