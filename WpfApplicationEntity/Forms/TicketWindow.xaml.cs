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
    partial class TicketWindow : Window
    {

        private readonly bool add_edit;
        private readonly int id;

        public TicketWindow()
        {
            InitializeComponent();
        }
        public TicketWindow(bool add_edit, int id = 0)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;

            using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
            {
                if (this.add_edit == false)
                {
                    WFAEntity.API.Ticket objectTicket = WFAEntity.API.DatabaseRequest.GetTicketById(objectMyDBContext, this.id);
                    textBlockAddEditCost.Text = objectTicket.Cost;
                    textBlockAddEditAmount.Text = objectTicket.Amount;
                    textBlockAddEditStatus.Text = objectTicket.Status;
                    ButtonAddEditTicket.Content = "Изменить";
                }
            }
        }
        private bool IsDataCorrect()
        {
            return (textBlockAddEditCost.Text != string.Empty) ||
                (textBlockAddEditAmount.Text != string.Empty) ||
                (textBlockAddEditStatus.Text != string.Empty);
        }
        private void ButtonAddEditTicket_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsDataCorrect() == true)
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
                    if (this.add_edit == true)
                    {
                        objectMyDBContext.Ticket.Add(objectTicket);
                    }
                    else
                    {
                        objectTicket.ID_Ticket = WFAEntity.API.DatabaseRequest.GetTicketById(objectMyDBContext, this.id).ID_Ticket;
                        WFAEntity.API.Ticket objectStudentFromDataBase = new WFAEntity.API.Ticket();
                        objectStudentFromDataBase = WFAEntity.API.DatabaseRequest.GetTicketById(objectMyDBContext, this.id);
                        objectMyDBContext.Entry(objectStudentFromDataBase).CurrentValues.SetValues(objectTicket);
                        objectMyDBContext.SaveChanges();
                    }
                    objectMyDBContext.SaveChanges();
                    this.DialogResult = true;
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
