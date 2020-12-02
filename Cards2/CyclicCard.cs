using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Shop
{
    //Циклическая карта
    class CyclicCard : Card
    {
        //Конструктор
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

            owner.balance = DiscountConst.TubeLimit;
            discount = DiscountConst.TubeDiscount;
        }

        //True, если карту нужно поменять
        public override bool IsNeedChange(PDA_Project_1.Costumer owner)
        {
            Debug.Assert(owner != null, "owner is null");

            return (discount == DiscountLimitsConst.TubeDiscount && owner.balance >= DiscountLimitsConst.TransistorLimit) ||
                (discount == DiscountLimitsConst.TransistorDiscount && owner.balance >= DiscountLimitsConst.IntegralLimit)
                    || (discount == DiscountLimitsConst.IntegralDiscount && owner.balance >= DiscountLimitsConst.CyclicLimit);
        }
        // Мдааа, давай, исправляй, я подожду ок
        //Замена карты
        public override Card ChangeCard(Costumer owner)
        {
            Debug.Assert(owner != null, "owner is null");

            if (owner.balance >= DiscountLimitsConst.CyclicLimit)
                CancelCard(owner);
            else if (owner.balance >= DiscountLimitsConst.IntegralLimit)
                discount = DiscountLimitsConst.IntegralLimit;
            else
                discount = DiscountLimitsConst.TransistorDiscount;

            return this;
        }
    }
}
