using Lesson_14.Interface;
using Lesson_14.Models.ConvertJSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lesson_14.Models
{
    /// <summary>
    /// Класс для работы с клиентами
    /// </summary>
    public class Client : ViewModelBase
    {
        #region Свойства 
        /// <summary>
        /// Ф.И.О.
        /// </summary>
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    RaisePropertyChanged("FullName");
                }
            }
        }
        private string fullName;
        /// <summary>
        /// ИНН
        /// </summary>
        public string INN
        {
            get
            {
                return inn;
            }
            set
            {
                if (inn != value)
                {
                    inn = value;
                    RaisePropertyChanged("INN");
                }
            }
        }
        private string inn;
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (phone != value)
                {
                    phone = value;
                    RaisePropertyChanged("Phone");
                }
            }
        }
        private string phone;
        /// <summary>
        /// Список счетов клиента
        /// </summary>
        //[JsonConverter(typeof(ConcreteConverter<ObservableCollection<Account>>))]
        public ObservableCollection<IAccount<Account>> Accounts { get; set; }
        #endregion

        #region Конструкторы
        public Client(string FullName, string INN, string Phone)
        {
            this.FullName = FullName;
            this.INN = INN;
            this.Phone = Phone;
        }

        public Client(string FullName, string INN, string Phone, ObservableCollection<AccountJSON> accounts)
        {
            this.FullName = FullName;
            this.INN = INN;
            this.Phone = Phone;
            this.Accounts = new ObservableCollection<IAccount<Account>>();

            if (accounts != null)
            {
                foreach (var acc in accounts)
                {
                    if (acc.MyType.ToString() == "Депозитный")
                    {
                        this.Accounts.Add(new Deposit(acc.Number, acc.Balance));
                    }
                    else
                    {
                        this.Accounts.Add(new NonDeposit(acc.Number, acc.Balance));
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Добавление счёта
        /// </summary>
        /// <typeparam name="T">Тип счёта</typeparam>
        /// <param name="account">Данные счёта</param>
        public void AddAccount<T>(T account) where T : Account
        { 
            if (this.Accounts == null)
            {
                this.Accounts = new ObservableCollection<IAccount<Account>>();
            }
            this.Accounts.Add((IAccount<Account>)account);
        }

        /// <summary>
        /// Удаление счёта
        /// </summary>
        /// <typeparam name="T">Тим счёта</typeparam>
        /// <param name="account">Данные счёта</param>
        public void RemoveAccount<T>(T account) where T : Account
        {
            this.Accounts.Remove(account);
        }
    }
}
