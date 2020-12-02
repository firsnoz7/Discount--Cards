using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Shop
{
    class CheerfulCard:Card
    {
        private SortedSet<int> cheerfulDays; //дни (неизвестные покупателю), когда в магазине действует скидка

        public CheerfulCard(SortedSet<int> cheerful_days): base()
        {
            Debug.Assert(cheerful_days != null, "cheerful days are null");

            discount = DiscountConst.CheerfullDiscount;
            name = "cheerful discount card";
            this.cheerfulDays = new SortedSet<int>(cheerfulDays);
        }

        //True, если карту нужно заменить
        public override bool IsChanging(Shop.Customer owner)
        {
            return false;
        }

        //Замена карты
        public override Card ChangeCard(Customer cust = null)
        {
            return this;
        }

        //Параметр "Скидка"
        public override int Discount
        {
            get   
            {
                if (cheerfulDays.Contains(GeneralDate.Instance().Date.Day))
                    return discount;
                else
                    return 0;
            }
        }

        //Обновление "веселых" дней
        public void UpdateCheerfulDays(SortedSet<int> cheerfulDays)
        {
            Debug.Assert(cheerfulDays != null, "cheerfulDays are null");
         this.cheerfulDays = new SortedSet<int>(cheerfulDays);
        }
    }
}
