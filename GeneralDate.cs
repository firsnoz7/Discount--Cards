using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Shop
{
    // Класс единой даты для магазина и карт
    public class GeneralDate
    {
        public DateTime Date;

        static GeneralDate instance;

        // Конструктор
        protected GeneralDate()
        {
            Date = DateTime.Today;
        }

        /// Конструктор
        protected GeneralDate(DateTime date)
        {
            this.Date = date;
        }

        public static GeneralDate Instance()
        {
            if (instance == null)
                instance = new GeneralDate();

            return instance;
        }

        public static GeneralDate Instance(DateTime date)
        {
            if (instance == null)
                instance = new GeneralDate();
            instance.Date = date;
            return instance;
        }

        public static void AddDays(int day)
        {
            Debug.Assert(day > 0, "Count of days should be positive");
            Debug.Assert(day < 100000, "Count if days is very big");

            instance.Date = instance.Date.AddDays(day);
        }

        public static void AddMonths(int months)
        {
            Debug.Assert(months > 0, "Count of months must be positive");
            Debug.Assert(months < 20000, "Count of months is very big");

            instance.Date = instance.Date.AddMonths(months);
        }
    }
}
