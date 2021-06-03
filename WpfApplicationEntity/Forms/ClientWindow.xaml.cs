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
    public partial class ClientWindow : Window
    {
        private bool add_edit;
        private int id;
        public ClientWindow()
        {
            InitializeComponent();
        }
        public ClientWindow(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                {
                    WFAEntity.API.Client objectClient = WFAEntity.API.DatabaseRequest.GetClientById(objectMyDBContext, this.id);
                    objectClient.Surname = objectClient.Surname;
                    objectClient.Name = objectClient.Name;
                    objectClient.Patronymic = objectClient.Patronymic;
                    objectClient.Address = objectClient.Address;
                    objectClient.Number = objectClient.Number;

                }
                ButtonAddEditClient.Content = "Изменить";
            }
        }
        private bool IsDataCorrcet()
        {
            if (textBlockAddEditSurname.Text != string.Empty || textBlockAddEditName.Text != string.Empty || textBlockAddEditPatronymic.Text != string.Empty || textBlockAddEditAdress.Text != string.Empty || textBlockAddEditTelephone.Text != string.Empty) ;
            return true;
            return false;

        }
        private void ButtonAddEditClient_Click(object sender, RoutedEventArgs e)
        {
            WFAEntity.API.Client objectClient = new WFAEntity.API.Client();
            if (this.add_edit == true)
            {
                objectClient.Surname = textBlockAddEditSurname.Text;
                objectClient.Name = textBlockAddEditName.Text;
                objectClient.Patronymic = textBlockAddEditPatronymic.Text;
                objectClient.Address = textBlockAddEditAdress.Text;
                objectClient.Number = textBlockAddEditTelephone.Text;
                try
                {
                    if (this.IsDataCorrcet() == true)
                    {
                        using (WFAEntity.API.MyDBContext objectMyDBContext =
                                new WFAEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Client.Add(objectClient);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Клиент добавлена");
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
            else
            {
                try
                {
                    if (this.IsDataCorrcet())
                    {
                        using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                        {
                            WFAEntity.API.Client client = new WFAEntity.API.Client();
                            client = WFAEntity.API.DatabaseRequest.GetClientById(objectMyDBContext, this.id);
                            client.Surname = textBlockAddEditSurname.Text;
                            client.Name = textBlockAddEditName.Text;
                            client.Patronymic = textBlockAddEditPatronymic.Text;
                            client.Address = textBlockAddEditAdress.Text;
                            client.Number = textBlockAddEditTelephone.Text;
                            objectMyDBContext.Entry(client).State = System.Data.Entity.EntityState.Modified;
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Клиент изменена");
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

        private void ButtonAddEditGroup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
