using Lesson_14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Interface
{
    /// <summary>
    /// Интерфейс для работы со счетами клиента
    /// </summary>
    /// <typeparam name="T">Тип счёта</typeparam>
    public interface IAccount<out T> where T : Account
    {
        /// <summary>
        /// Номер счёта
        /// </summary>
        object Number { get; set; }
        /// <summary>
        /// Баланс счёта
        /// </summary>
        object Balance { get; set; }
        /// <summary>
        /// Тип счёта
        /// </summary>
        object MyType { get; set; }
        /// <summary>
        /// Перевод средств
        /// </summary>
        /// <param name="sum">Сумма перевода</param>
        public virtual void ReplenishBalance(decimal sum)
        {
            ;
        }
    }
}
