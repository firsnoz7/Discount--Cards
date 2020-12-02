using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class IntegralCard: Card
    {
        public IntegralCard() : base()
        {
            discount = DiscountConst.IntegralDiscount;
            name = "integral discount card";
        }

        //True, если карту нужно заменить
        public override bool IsChanging(Shop.Customer cust)
        {
            return false;
        }

        //Замена карты на следующую
        public override Card ChangeCard(Shop.Customer owner = null)
        {
            return this;
        }
    }
}