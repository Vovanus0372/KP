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
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class TicketWindow : Window
    {
        MainWindow AF;

        bool IsEdit = false;
        Ticket EditTicket;
        private bool p1;
        private int p2;
        public TicketWindow(MainWindow AF)
        {
            this.AF = AF;
            InitializeComponent();

        }
        public TicketWindow(MainWindow AF, Ticket EditTicket)
        {
            IsEdit = true;
            this.AF = AF;
            this.EditTicket = EditTicket;
            InitializeComponent();
        }

        public TicketWindow(bool p1, int p2)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
        }

        private void ButtonAddEditTicket_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEdit)
            {
                if (textBlockAddEditCost.Text != string.Empty)
                {
                    using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                    {
                        WFAEntity.API.Ticket objectTicket = new WFAEntity.API.Ticket(
                        textBlockAddEditCost.Text,
                        textBlockAddEditAmount.Text,
                        textBlockAddEditStatus.Text,
                        (WFAEntity.API.Client)ComboBoxAddEditClient.SelectedItem,
                        (WFAEntity.API.MK_schedule)ComboBoxAddEditShedule.SelectedItem,
                        (WFAEntity.API.Other_services)ComboBoxAddEditServices.SelectedItem,
                        (WFAEntity.API.Skates_hire)ComboBoxAddEditSkates.SelectedItem
                        );
                        try
                        {
                            objectMyDBContext.Ticket.Add(objectTicket);
                            objectMyDBContext.SaveChanges();
                            MessageBox.Show("Билет добавлен");
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
                    WFAEntity.API.Employees objectTicket = new WFAEntity.API.Employees();
                    EditTicket.Cost = textBlockAddEditCost.Text;
                    EditTicket.Amount = textBlockAddEditAmount.Text;
                    EditTicket.Status = textBlockAddEditStatus.Text;
                    try
                    {
                        objectMyDBContext.Ticket.AddOrUpdate(EditTicket);
                        objectMyDBContext.SaveChanges();
                        MessageBox.Show("Билет Редактирован");
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
            using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
            {
                ComboBoxAddEditClient.ItemsSource = WFAEntity.API.DatabaseRequest.GetClients(objectMyDBContext);
                ComboBoxAddEditClient.Text = "{Binging Name}";
                ComboBoxAddEditShedule.ItemsSource = WFAEntity.API.DatabaseRequest.GetShedule(objectMyDBContext);
                ComboBoxAddEditShedule.Text = "{Binging Date}";
                ComboBoxAddEditServices.ItemsSource = WFAEntity.API.DatabaseRequest.GetServices(objectMyDBContext);
                ComboBoxAddEditServices.Text = "{Binging Name}";
                ComboBoxAddEditSkates.ItemsSource = WFAEntity.API.DatabaseRequest.GetSkates(objectMyDBContext);
                ComboBoxAddEditSkates.Text = "{Binging Size}";
            }
        }
    }
}
