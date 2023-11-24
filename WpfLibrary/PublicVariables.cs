using Lesson_14.Interface;
using Lesson_14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_14
{
    public static class PublicVariables
    {
        public static Clients? clients;
        public static ITransfer<SubAccount, SubAccount> transfer = new Transfer();
        public static string CurrentClientINN;
        public static object CurrentAccountNumber;
    }
}
