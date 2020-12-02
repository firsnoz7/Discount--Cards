using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Shop.Cards2
{
    //Квантовая карта
    class QuantumCard : Card
    {
        DateTime changeDate; //Дата замены карты

        //Конструктор
        public QuantumCard() : base()
        {
            discount = DiscountConst.QuantumDiscount;
            name = "quantum discount card";
            changeDate = GeneralDate.Instance().Date.AddDays(DiscountConst.QuantumDaysLimit);
        }

        //Конструктор с датой
        public QuantumCard(DateTime createDate) : base()
        {
            Debug.Assert(createDate != null, "Create date is null");

            discount = DiscountConst.QuantumDiscount;
            name = "quantum discount card";
            this.changeDate = createDate.AddDays(DiscountConst.QuantumDaysLimit);
        }

        //True, если нужно заменить
        public override bool IsChanging(Shop.Customer owner)
        {
            return GeneralDate.Instance().Date >= changeDate;
        }

        //Замена карты
        public override Card ChangeCard(Shop.Customer owner)
        {
            Debug.Assert(owner != null, "owner is null");

            return new CyclicCard(owner);
        }
    }
}
