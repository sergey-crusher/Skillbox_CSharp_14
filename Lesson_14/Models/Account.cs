using Lesson_14.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models
{
    /// <summary>
    /// Родительский класс для работы со счетами клиентов
    /// </summary>
    public class Account : ViewModelBase, IAccount<Account>
    {
        #region Свойства
        /// <summary>
        /// Номер счёта
        /// </summary>
        public object Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number != value)
                {
                    number = value;
                    RaisePropertyChanged("Number");
                }
            }
        }
        private object number;

        /// <summary>
        /// Остаток на счёте
        /// </summary>
        public object Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (balance != value)
                {
                    balance = value;
                    RaisePropertyChanged("Balance");
                }
            }
        }
        private object balance;

        public object MyType
        {
            get
            {
                return myType.ToString() == "Deposit" ? "Депозитный" : "Недепозитный";
            }
            set
            {
                if (myType != value)
                {
                    RaisePropertyChanged("MyType");
                }
            }
        }
        private object myType => this.GetType().Name;
        #endregion

        #region Конструкторы
        public Account() { }
        public Account(object number, object balance)
        {
            Number = number;
            Balance = new Balance<object>(balance).Value;
        }
        #endregion

        #region Методы
        public virtual void ReplenishBalance(decimal sum)
        {
            ;
        }
        #endregion
    }
}
