using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models.ConvertJSON
{
    /// <summary>
    /// Класс для десериализации счетов
    /// </summary>
    public class AccountJSON
    {
        /// <summary>
        /// Номер счёта
        /// </summary>
        public object Number { get; set; }
        /// <summary>
        /// Остаток на счёте
        /// </summary>
        public object Balance { get; set; }
        /// <summary>
        /// Тип счёта
        /// </summary>
        public object MyType { get; set; }
    }
}
