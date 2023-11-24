using Lesson_14.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models
{
    /// <summary>
    /// Депозитный счёт
    /// </summary>
    public class Deposit : Account, IAccount<Account>
    {
        #region Конструкторы
        public Deposit(object number, object balance) : base(number, balance)
        {
        }
        #endregion

        #region Методы
        public override void ReplenishBalance(decimal sum)
        {
            // За пополнение, бонус 50 у.е. для привлечения клиентов (чтобы видеть отличия)
            sum += 50;
            this.Balance = (decimal)this.Balance + sum;
        }
        #endregion
    }
}
