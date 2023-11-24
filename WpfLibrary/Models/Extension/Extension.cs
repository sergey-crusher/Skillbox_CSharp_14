using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models.Extension
{
    /// <summary>
    /// Класс расширений
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Парсинг decimal с ошибкой для отрицательных значений
        /// </summary>
        /// <param name="value">Принимаемое значение</param>
        /// <param name="result">Выходное значение</param>
        /// <returns>Статус парсинга, если null, то ошибки отсутствуют</returns>
        public static string? EParse(this string value, out decimal result)
        {
            string? status = null;
            decimal res = 0;
            try
            {
                res = decimal.Parse(value);
                if (res <= 0.0m)
                {
                    throw new Exception("Значение должны быть положительным числом");
                }
            }
            catch (FormatException)
            {
                status = "Сумма имела неверный формат";
            }
            catch (OverflowException)
            {
                status = $"Укажите пожалуйста значение меньше {decimal.MaxValue}";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            
            result = res;

            return status;
        }

        /// <summary>
        /// Проверка корректности номера счёта
        /// </summary>
        /// <param name="value">Номер счёта</param>
        /// <returns>Статус счёта (null - ошибки отсутствуют)</returns>
        public static string? AccountCorrectness(this string value)
        {
            if (!value.All(char.IsDigit))
            {
                return "Используйте только цифры при вводе номера счёта";
            }

            if (value.Length != 20)
            {
                return $"Количество цифр в номере счёта должно быть равным 20. Вы ввели {value.Length}";
            }
            return null;
        }
    }
}
