using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Shop.Cards2
{
    //Циклическая карта
    class CyclicCard : Card
    {
        public CyclicCard(Shop.Customer owner) : base()
        {
            Debug.Assert(owner != null, "owner is null");
            discount = DiscountConst.TubeDiscount;
            name = "cyclic discount card";
            owner.purch_sum = DiscountConst.TubeSum;
        }

        //Аннулирование циклической карты
        public void CancelCard(Shop.Customer owner)
        {
            Debug.Assert(owner != null, "owner is null");
            owner.purch_sum = DiscountConst.TubeSum;
            discount = DiscountConst.TubeDiscount;
        }

        //True, если карту нужно поменять
        public override bool IsChanging(Shop.Customer owner)
        {
            Debug.Assert(owner != null, "owner is null");

            return (discount == DiscountConst.TubeDiscount && owner.purch_sum >= DiscountConst.TransistorSum) ||
                (discount == DiscountConst.TransistorDiscount && owner.purch_sum >= DiscountConst.TransistorSum)
                    || (discount == DiscountConst.IntegralDiscount && owner.purch_sum >= DiscountConst.CyclicLimit);
        }

        //Замена карты
        public override Card ChangeCard(Customer owner)
        {
            Debug.Assert(owner != null, "owner is null");

            if (owner.purch_sum >= DiscountConst.CyclicLimit)
                CancelCard(owner);
            else if (owner.purch_sum >= DiscountConst.IntegralSum)
                discount = DiscountConst.IntegralSum;
            else
                discount = DiscountConst.TransistorDiscount;
           return this;
        }
    }
}
