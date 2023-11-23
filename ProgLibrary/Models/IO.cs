using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models
{
    public static class IO
    {
        /// <summary>
        /// Файл куда записывается история изменений
        /// </summary>
        private const string path = "./history.txt";
        /// <summary>
        /// Метод вызывается делегатом - запись в файл истории изменений с датой
        /// </summary>
        /// <param name="message"></param>
        public static void Save2File(string message)
        {
            DateTime now = DateTime.Now;
            File.AppendAllText(path, $"{now}: {message}\r\n");
        }
    }
}
