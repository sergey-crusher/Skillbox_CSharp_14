using Lesson_14.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Lesson_14
{
    /// <summary>
    /// Логика взаимодействия для pAddClient.xaml
    /// </summary>
    public partial class pAddClient : Page
    {
        private List<string> FullName = new List<string>();

        public pAddClient()
        {
            InitializeComponent();

            // Наполнение коллекции возможными ФИО
            var file = File.ReadAllLines("./people.txt");
            foreach(var line in file)
            {
                FullName.Add(line);
            }
        }

        private void ButtonGeneration(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            TextBoxFullName.Text = FullName[random.Next(0, FullName.Count - 1)];
            TextBoxINN.Text = random.NextInt64(100_000_000_000, 999_999_999_999).ToString();
            TextBoxPhone.Text = "7" + random.NextInt64(1_000_000_000, 9_999_999_999).ToString();
        }

        private void ButtonCreate(object sender, RoutedEventArgs e)
        {
            string FullName = TextBoxFullName.Text;
            string INN = TextBoxINN.Text;
            string Phone = TextBoxPhone.Text;

            if (String.IsNullOrEmpty(FullName) || String.IsNullOrEmpty(INN) || String.IsNullOrEmpty(Phone))
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else
            {
                PublicVariables.clients.Add(FullName, INN, Phone);
                PublicVariables.CurrentClientINN = INN;
            }
        }
    }
}
