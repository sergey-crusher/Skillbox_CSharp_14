using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14.Models
{
    public class Balance<T>
    {
        public decimal Value;
        public Balance(T value) 
        { 
            decimal.TryParse(value.ToString(), out Value);
        }
    }
}
