using Lesson_14.Interface;
using Lesson_14.Models;
using Lesson_14.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson_14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        /// <summary>
        /// Экземпляр для работы с клиентами
        /// </summary>
        public static Clients? clients;
        public static ITransfer<SubAccount, SubAccount> transfer = new Transfer();
        public static string CurrentClientINN;
        public static object CurrentAccountNumber;

        /// <summary>
        /// Экземпляр класса всплывающего сообщения
        /// </summary>
        public static DisplayMessageClass displayMessage = new DisplayMessageClass() { Text = "Загрузка...", Visible = "Visible" };

        public MainWindow()
        {
            if (clients == null)
            {
                clients = new Clients();
                // добавлем 
                clients.Notify += DisplayMessage; // вывод сообщений в интерфейсе
                clients.Notify += IO.Save2File; // сохрание истории изменений
            }
            if (clients.Count > 0)
            {
                InitializeComponent();
                TextBlockMessage.DataContext = displayMessage;
                dgClients.ItemsSource = clients;
                dgAccounts.ItemsSource = clients.First().Accounts;
                CurrentClientINN = clients.First().INN;
            }
            clients.SaveChange();
        }

        /// <summary>
        /// Вызов страницы добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAddClient(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("pAddClient.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Удаления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuRemoveClient(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItems.Count > 0)
            {
                Client client = dgClients.SelectedItems[0] as Client;
                clients.Remove(client.INN);
                clients.SaveChange();
            }
            else
            {
                MessageBox.Show("Выберите клиента которого желаете удалить");
            }
        }

        /// <summary>
        /// Изменение данных клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgClients_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            CurrentClientINN = ((Client)e.Row.DataContext).INN;
            clients.Update(ref clients, (Client)e.Row.DataContext, e.Column.SortMemberPath, (e.EditingElement as TextBox).Text);
            clients.SaveChange();
        }

        /// <summary>
        /// Изменение клиента (активного)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Client client = (Client)e.AddedItems[0];
                CurrentClientINN = client.INN;
                dgAccounts.ItemsSource = client.Accounts;
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Добавление счёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAddAccount(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("pAddAccount.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Удаление счёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuRemoveAccount(object sender, RoutedEventArgs e)
        {
            Client client;
            if (dgClients.SelectedItems.Count > 0)
            {
                client = dgClients.SelectedItems[0] as Client;
            }
            else
            {
                MessageBox.Show("Выберите клиента счёт которого желаете удалить");
                return;
            }

            if (dgAccounts.SelectedItems.Count > 0)
            {
                clients.RemoveAccount<Account>(client.INN, dgAccounts.SelectedItems[0] as Account);
                clients.SaveChange();
            }
            else
            {
                MessageBox.Show("Выберите счёт для удаления");
            }
        }

        /// <summary>
        /// Вызов страницы пополнения баланса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuReplenishBalance(object sender, RoutedEventArgs e)
        {
            if (dgAccounts.SelectedItems.Count > 0)
            {
                CurrentAccountNumber = (dgAccounts.SelectedItems[0] as Account).Number;
                NavigationService.Navigate(new Uri("pReplenishBalance.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Выберите счёт для пополнения");
            }
        }

        /// <summary>
        /// Вызов страницы перевода средств
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuTransfer(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("pTransfer.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Кнопка справки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("14 модуль скилбокса - Алешин С.А.");
        }

        /// <summary>
        /// Вывод всплывающего сообщения
        /// </summary>
        /// <param name="message">Тест сообщения</param>
        public void DisplayMessage(string message)
        {
            displayMessage.Text = message;
        }
    }
}
