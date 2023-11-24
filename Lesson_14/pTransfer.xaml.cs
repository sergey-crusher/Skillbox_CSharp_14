using Lesson_14.Interface;
using Lesson_14.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для pTransfer.xaml
    /// </summary>
    public partial class pTransfer : Page
    {
        public Clients clients => PublicVariables.clients;

        public pTransfer()
        {
            InitializeComponent();
        }

        private void ButtonTransfer(object sender, RoutedEventArgs e)
        {
            if (ComboBoxAcc1.SelectedValue.ToString() != ComboBoxAcc2.SelectedValue.ToString() ||
                ComboBoxCl1.SelectedItem != ComboBoxCl2.SelectedItem)
            {
                PublicVariables.clients.Transfer(
                    (Account)clients.First(x => x == (Client)ComboBoxCl1.SelectedItem).Accounts.First(x => x.Number.ToString() == ComboBoxAcc1.Text),
                    (Account)clients.First(x => x == (Client)ComboBoxCl2.SelectedItem).Accounts.First(x => x.Number.ToString() == ComboBoxAcc2.Text),
                    TextBoxSum.Text
                    );
            }
            else
            {
                MessageBox.Show("Выберите разные счета");
            }
        }

        private void ComboBoxCl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxAcc1.ItemsSource = GetAccounts((Client)ComboBoxCl1.SelectedItem);
        }

        private void ComboBoxCl2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxAcc2.ItemsSource = GetAccounts((Client)ComboBoxCl2.SelectedItem);
        }

        private ObservableCollection<string> GetAccounts(Client client)
        {
            try
            {
                return new ObservableCollection<string>(
                    clients
                        .First(x => x == client).Accounts
                            .Select(x => x.Number.ToString())
                );
            }
            catch
            {
                return new ObservableCollection<string>();
            }
        }
    }
}
