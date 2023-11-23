using Lesson_14.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models
{
    /// <summary>
    /// Недепозитный счёт
    /// </summary>
    public class NonDeposit : Account, IAccount<Account>
    {
        #region Конструкторы
        public NonDeposit(object number, object balance) : base(number, balance)
        {
        }
        #endregion

        #region Методы
        public override void ReplenishBalance(decimal sum)
        {
            this.Balance = (decimal)this.Balance + sum;
        }
        #endregion
    }
}
