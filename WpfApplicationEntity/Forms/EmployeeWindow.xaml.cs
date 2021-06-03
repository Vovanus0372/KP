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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : System.Windows.Window
    {
        public EmployeeWindow()
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
                    ClientGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetClients(objectMyDBContext);
                    EmployeesGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                    SheduleGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetEmployeesWithSchedule(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Клиент", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow g = new EmployeesWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }



        private void editEmployeesButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Employees)EmployeesGrid.SelectedItem;
                Employees EditEmployees = MyDBContext.DBContext.Employees.Find(itm.ID_employees);
                EmployeesWindow employeesAddForm = new EmployeesWindow(false, EditEmployees.ID_employees);
                if (employeesAddForm.ShowDialog() == true)
                    this.ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void addSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.SheduleWindow g = new Forms.SheduleWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        private void deleteSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void editSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (WFAEntity.API.DatabaseRequest.NewMK_schedule)SheduleGrid.SelectedItem;
                MK_schedule EditShedule = MyDBContext.DBContext.MK_schedule.Find(itm.ID_MK_schedule);
                SheduleWindow ServiceAddForm = new SheduleWindow(false, EditShedule.ID_MK_schedule);
                if (ServiceAddForm.ShowDialog() == true)
                    this.ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addClientButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ClientWindow g = new Forms.ClientWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Client)ClientGrid.SelectedItem;
                var tmp = (
        from tmpClient in MyDBContext.DBContext.Client.ToList<Client>()
        where tmpClient.ID_Client == itm.ID_Client
        select tmpClient
              ).ToList();
                MyDBContext.DBContext.Client.Remove(tmp[0]);
                MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Client)ClientGrid.SelectedItem;
                Client EditClient = MyDBContext.DBContext.Client.Find(itm.ID_Client);
                ClientWindow clientAddForm = new ClientWindow(false, EditClient.ID_Client);
                if (clientAddForm.ShowDialog() == true)
                    this.ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите строку", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void reportEmployeesButton_Click_1(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Фамилия";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Имя";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Отчество";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Должность";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Логин";
            workSheet.Cells[1, 6].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 6].Interior.ColorIndex = 17;
            workSheet.Cells[1, 6] = "Пароль";
            workSheet.Cells[1, 7].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 7].Interior.ColorIndex = 17;
            workSheet.Cells[1, 7] = "Дата рождения";
            workSheet.Cells[1, 8].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 8].Interior.ColorIndex = 17;
            workSheet.Cells[1, 8] = "Адрес";
            workSheet.Cells[1, 9].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 9].Interior.ColorIndex = 17;
            workSheet.Cells[1, 9] = "Телефон";       //от 1 до 11 это названия столбцов в отчёте

            int i = 2;
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    List<Employees> employes = WFAEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext).ToList(); //задаём для кого будем делать отчёт
                    foreach (Employees Employee in employes)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = Employee.Surname;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = Employee.Name;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = Employee.Patronymic;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = Employee.Position;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = Employee.Login;
                        workSheet.Cells[i, 6].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 6] = Employee.Password;
                        workSheet.Cells[i, 7].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 7] = Employee.Date;
                        workSheet.Cells[i, 8].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 8] = Employee.Address;
                        workSheet.Cells[i, 9].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 9] = Employee.Telephone;  //тут собственно прописаны значения которые записываются в отчёт и в какой столбец они записываются
   
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Сотрудники.xls";                                        //путь к отчёту и название
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

        private void reportClientButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Фамилия";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Имя";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Отчество";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Адрес";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Телефон";

            int i = 2;
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    List<Client> Client = WFAEntity.API.DatabaseRequest.GetClients(objectMyDBContext).ToList(); //задаём для кого будем делать отчёт
                    foreach (Client client in Client)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = client.Surname;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = client.Name;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = client.Patronymic;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = client.Address;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = client.Number;

                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Клиент.xls";                                        //путь к отчёту и название
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

        public void UpdateGridAfterSearch(List<Employees> Source)
        {
            EmployeesGrid.ItemsSource = Source;
            EmployeesGrid.Columns[0].Header = "ID сотрудника";
            EmployeesGrid.Columns[1].Header = "Имя";
            EmployeesGrid.Columns[2].Header = "Фамилия";
            EmployeesGrid.Columns[3].Header = "Отчество";
            EmployeesGrid.Columns[4].Header = "Дата рождения";
            EmployeesGrid.Columns[5].Header = "Должность";
            EmployeesGrid.Columns[6].Header = "Адрес";
            EmployeesGrid.Columns[7].Header = "Номер телефона";
            EmployeesGrid.Columns[8].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[9].Visibility = Visibility.Hidden;
        }

        public struct WorkerSearchInfo
        {
            public int ID_employees { get; set; }
            public string NamesInfo { get; set; }
            public WorkerSearchInfo(int ID_employees, string NamesInfo)
                : this()
            {
                this.ID_employees = ID_employees;
                this.NamesInfo = NamesInfo;
            }
        }
        public List<Employees> SearchWorkers(string SearchValue)
        {
            var DefaultWorkersList = MyDBContext.DBContext.Employees.ToList();
            List<WorkerSearchInfo> workerSearchInfos = new List<WorkerSearchInfo>();
            List<Employees> SearchedWorkers = new List<Employees>();
            for (int i = 0; i < DefaultWorkersList.Count; i++)
            {
                workerSearchInfos.Add(new WorkerSearchInfo(DefaultWorkersList[i].ID_employees, DefaultWorkersList[i].Surname + " " + DefaultWorkersList[i].Name + " " + DefaultWorkersList[i].Patronymic));
            }
            for (int i = 0; i < workerSearchInfos.Count; i++)
            {
                if (workerSearchInfos[i].NamesInfo.Contains(SearchValue))
                {
                    SearchedWorkers.Add(MyDBContext.DBContext.Employees.Find(workerSearchInfos[i].ID_employees));
                }
            }
            return SearchedWorkers;
        }
        private void SearchBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = String.Empty;
        }

        private void SearchBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "Поиск по ФИО";
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text.Length >= 3 && SearchBox.Text != "Поиск по ФИО")
            {
                List<Employees> SearchedWorkers = new List<Employees>();
                string SearchValue = SearchBox.Text;
                Thread SearchThread = new Thread(() => SearchedWorkers = SearchWorkers(SearchValue));
                SearchThread.Start();
                SearchThread.Join();
                UpdateGridAfterSearch(SearchedWorkers);
            }
            else
            {
                ShowAll();
            }
        }
    }
}
