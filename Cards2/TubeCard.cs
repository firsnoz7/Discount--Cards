using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Shop
{
    class TubeCard:Card
    {
        
        public TubeCard() : base()
        {
            discount = DiscountConst.TubeDiscount;
            name = "tube discount card";
        }

        //True, если карту можно заменить
        public override bool IsChanging(Shop.Customer cust)
        {
            Debug.Assert(cust != null, "owner is null");

            return cust.purch_sum >= DiscountConst.TransistorSum;
        }

        //замена карты
        public override Card ChangeCard(Shop.Customer owner = null)
        {
            return new TransistorCard();
        }
    }
}