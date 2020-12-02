using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Shop
{
    //Отчет о покупке
    public class CSReport
    {
        private int discount; //использованная скидка
        private double costWithDiscount; //цена с учетом скидки
        private bool isCardChanged; //поменялась ли карта

        //Конструктор
        public CSReport() { }

        //Скидка
        public int Discount
        {
            get
            {
                return discount;
            }

            set
            {
                Debug.Assert(value >= 0, "discount is negative");
                discount = value;
            }
        }

        //Стоимость с учетом скидки
        public double CostWithDiscount
        {
            get
            {
                return costWithDiscount;
            }

            set
            {
                Debug.Assert(value >= 0, "cost is negative");
                costWithDiscount = value;
            }
        }

        //Была ли карта изменена ?
        public bool IsCardChanged
        {
            get
            {
                return isCardChanged;
            }

            set
            {
                isCardChanged = value;
            }
        }
    }
}
