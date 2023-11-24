using Lesson_14.Interface;
using Lesson_14.Models.ConvertJSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Lesson_14.Models.Extension;

namespace Lesson_14.Models
{
    /// <summary>
    /// Коллекция клиентов
    /// </summary>
    public class Clients : ObservableCollection<Client>
    {
        /// <summary>
        /// Делегат
        /// </summary>
        /// <param name="message"></param>
        public delegate void AccountHandler(string message);
        /// <summary>
        /// Уведомление о событии
        /// </summary>
        public event AccountHandler? Notify;
        /// <summary>
        /// Инициализация и наполнение клиентами из базы
        /// </summary>
        public Clients() 
        {
            Get();
        }
        /// <summary>
        /// Получение данных клиентов
        /// </summary>
        private void Get()
        {
            // Считываем все данные клиентов (десериализуем их)
            var load = JsonConvert.DeserializeObject<ObservableCollection<ClientJSON>>(
                File.ReadAllText("../../../../clients_db.json"));
            // Добавляем клиентов из "базы"
            if (load.Count > 0)
            {
                foreach (var item in load)
                {
                    this.Add(new Client(item.FullName, item.INN, item.Phone, item.Accounts));
                    //this.Last().Post += this.Last().Tape;
                }
            }
            else
            {
                MessageBox.Show("Клиенты в БД отсутствуют");
            }
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="FullName">ФИО</param>
        /// <param name="INN">ИНН</param>
        /// <param name="Phone">Телефон</param>
        public void Add(string FullName, string INN, string Phone)
        {
            if (!FindClient(INN, false))
            {
                this.Add(new Client(FullName, INN, Phone));
                Notify?.Invoke($"Клиент {FullName} успешно добавлен");
            }
            else
            {
                MessageBox.Show($"Клиент с таким ИНН уже существует");
            }
        }

        /// <summary>
        /// Обновление данных клиента
        /// </summary>
        /// <param name="clients">Список клиентов</param>
        /// <param name="client">Клиент</param>
        /// <param name="field">Поле</param>
        /// <param name="value">Новое значение</param>
        public void Update(
            ref Clients clients,
            Client client, 
            string field,
            object value)
        {
            foreach (var item in typeof(Client).GetProperties())
            {
                if (item.Name == field)
                {
                    client.GetType()
                        .GetProperty(field)
                        .SetValue(client, value);
                    Notify?.Invoke($"Вы изменили у клиента {client.FullName} поле {field}");
                    break;
                }
            }
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="INN">ИНН</param>
        public void Remove(string INN)
        {
            if (FindClient(INN))
            {
                Client client = this.First(x => x.INN == INN);
                this.Remove(client);
                Notify?.Invoke($"Вы удалили клиента {client.FullName}");
            }
        }

        /// <summary>
        /// Добавление счёта, клиенту
        /// </summary>
        /// <typeparam name="T">Тип счёта</typeparam>
        /// <param name="INN">ИНН</param>
        /// <param name="account">Данные счёта</param>
        public void AddAccount<T>(string INN, T account) where T : Account
        {
            string? status = account.Number.ToString().AccountCorrectness();
            if (status == null)
            {
                if (FindClient(INN))
                {
                    Client client = this.First(x => x.INN == INN);
                    client.AddAccount<T>(account);
                    Notify?.Invoke($"Вы добавили счёт клиенту {client.FullName}");
                }
            }
            else
            {
                Notify?.Invoke(status);
            }
        }

        /// <summary>
        /// Удаление счёта клиента
        /// </summary>
        /// <typeparam name="T">Тип счёта</typeparam>
        /// <param name="INN">ИНН клиента</param>
        /// <param name="account">Счёт</param>
        public void RemoveAccount<T>(string INN, T account) where T : Account
        {
            if (FindClient(INN))
            {
                Client client = this.First(x => x.INN == INN);
                client.RemoveAccount<T>(account);
                Notify?.Invoke($"Вы удалили счёт клиента {client.FullName}");
            }
        }

        /// <summary>
        /// Пополнение счёта
        /// </summary>
        /// <param name="sum">Сумма пополнения</param>
        public void ReplenishBalance(string str_sum)
        {
            if (FindClient(PublicVariables.CurrentClientINN))
            {
                Client client = this.First(x => x.INN == PublicVariables.CurrentClientINN);
                Account account = (Account)client.Accounts.First(x => x.Number == PublicVariables.CurrentAccountNumber);
                decimal sum;
                string? status = str_sum.EParse(out sum);
                if (status == null)
                {
                    account.ReplenishBalance(sum);
                    Notify?.Invoke($"Вы пополнили счёт {PublicVariables.CurrentAccountNumber} на сумму {sum}");
                }
                else
                {
                    Notify?.Invoke($"Вы указали сумму {str_sum}. Что вызвало ошибку: {status}");
                }
            }
        }

        /// <summary>
        /// Перевод между счетами
        /// </summary>
        /// <param name="acc_out">Счёт с которого переводится</param>
        /// <param name="acc_in">Счёт на который переводится</param>
        /// <param name="sum">Сумма перевода</param>
        public void Transfer(Account acc_out, Account acc_in, string str_sum)
        {
            decimal sum;
            string? status = str_sum.EParse(out sum);
            if (status == null)
            {
                PublicVariables.transfer.Post(acc_out, acc_in, sum);
                Notify?.Invoke($"Перевод со счёта {acc_out.Number} на {acc_in.Number} на сумму {sum}");
            }
            else
            {
                Notify?.Invoke($"Вы указали сумму {str_sum}. Что вызвало ошибку: {status}");
            }
        }

        /// <summary>
        /// Поиск клиента по ИНН
        /// </summary>
        /// <param name="INN">ИНН</param>
        /// <returns>true - если клиент существует</returns>
        private bool FindClient(string INN, bool MessageShow = true)
        {
            if (this.Count(x => x.INN == INN) > 0)
            {
                return true;
            }
            else
            {
                if (MessageShow)
                {
                    MessageBox.Show($"Клиент с ИНН: \"{INN}\" не найден!");
                }
                return false;
            }
        }

        /// <summary>
        /// Сохрание изменений
        /// </summary>
        public void SaveChange()
        {
            string serialize = JsonConvert.SerializeObject(this);
            File.WriteAllText("../../../../clients_db.json", serialize);
        }
    }
}