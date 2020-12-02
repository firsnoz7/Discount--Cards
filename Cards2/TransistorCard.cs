using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Shop
{
    class TransistorCard: Card
    {
        public TransistorCard() : base()
        {
            discount = DiscountConst.TransistorDiscount;
            name = "transistor discount card";
        }

        //True, если карту нужно заменить
        public override bool IsChanging(Shop.Customer owner)
        {
            Debug.Assert(owner != null, "owner is null");
            return owner.purch_sum >= DiscountConst.IntegralSum;
        }

        //Замена карты на следующую
        public override Card ChangeCard(Shop.Customer cust = null)
        {
            return new IntegralCard();
        }
    }
}