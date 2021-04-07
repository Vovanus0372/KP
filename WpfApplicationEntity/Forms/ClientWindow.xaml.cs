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
    partial class ClientWindow : Window
    {
        MainWindow AF;
       
        bool IsEdit = false;
        Client EditClient;
        public ClientWindow(MainWindow AF)
        {
            this.AF = AF;
            InitializeComponent();
            
        }
        public ClientWindow(MainWindow AF, Client EditClient)
        {
            IsEdit = true;
            this.AF = AF;
            this.EditClient = EditClient;
            InitializeComponent();
       }
        private void ButtonAddEditGroup_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEdit)
            {
                if (textBlockAddEditSurname.Text != string.Empty)
                {
                    using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                    {
                        WFAEntity.API.Client objectClient = new WFAEntity.API.Client();
                        objectClient.Surname = textBlockAddEditSurname.Text;
                        objectClient.Name = textBlockAddEditName.Text;
                        objectClient.Patronymic = textBlockAddEditPatronymic.Text;
                        objectClient.Address = textBlockAddEditAdress.Text;
                        objectClient.Number = textBlockAddEditTelephone.Text;
                        try
                        {
                            objectMyDBContext.Client.Add(objectClient);
                            objectMyDBContext.SaveChanges();
                            MessageBox.Show("Клиент добавлен");
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
                    WFAEntity.API.Client objectClient = new WFAEntity.API.Client();
                    EditClient.Surname = textBlockAddEditSurname.Text;
                    EditClient.Name = textBlockAddEditName.Text;
                    EditClient.Patronymic = textBlockAddEditPatronymic.Text;
                    EditClient.Address = textBlockAddEditAdress.Text;
                    EditClient.Number = textBlockAddEditTelephone.Text;
                    try
                    {
                        objectMyDBContext.Client.AddOrUpdate(EditClient);
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
