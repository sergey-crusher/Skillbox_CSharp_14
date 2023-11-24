using Lesson_14.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models.ConvertJSON
{
    /// <summary>
    /// Класс для десериализации клиентов
    /// </summary>
    public class ClientJSON
    {
        #region Свойства 
        /// <summary>
        /// Ф.И.О.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// ИНН
        /// </summary>
        public string INN { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Список счетов клиента
        /// </summary>
        public ObservableCollection<AccountJSON> Accounts { get; set; }
        #endregion
    }
}
