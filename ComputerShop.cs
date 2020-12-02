using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Shop.Cards2
{
    
    public class ComputerShop
    {
        private Dictionary<string, Customer> customers; //Покупатели
        private List<Card> cards; //Скидочные карты

        private Customer curCustomer; //Текущий покупатель
        private string customerKey; //идентификатор текущего покупателя

        private SortedSet<int> cheerfulDays; //"Веселые" дни

        private DateTime curDate;

        //Конструктор
        public ComputerShop()
        {
            customers = new Dictionary<string, Customer>();
            cards = new List<Card>();
            curCustomer = null;

            cards.Add(new Card());

            curDate = GeneralDate.Instance().Date;
            cheerfulDays = new SortedSet<int>();
            setCheerfulDays();
            customerKey = "";
        }

        //Установить "веселые" дни
        private void setCheerfulDays()
        {
            cheerfulDays.Clear();

            Random gen = new Random();

            int max_value = DateTime.DaysInMonth(curDate.Year, curDate.Month);
            for (int i = 0; i < 10; ++i)
            {
                int day = gen.Next(1, max_value);
                while (cheerfulDays.Contains(day))
                {
                    day = (day + 1) % (max_value);
                    if (day == 0)
                        ++day;
                }

                cheerfulDays.Add(day);
            }

            //Обновление всех выданных веселых карт
            foreach (Card card in cards)
                if (card is CheerfulCard)
                    (card as CheerfulCard).UpdateCheerfulDays(cheerfulDays);
        }

        
        /// Проверка наступления нового месяца
        public bool checkNewMonth()
        {
            DateTime lastDate = curDate;
            curDate = GeneralDate.Instance().Date;  

            if (curDate > lastDate && curDate.Month != lastDate.Month)
            {
                setCheerfulDays();
                return true;
            }

            return false;
        }

        //Команды А

        //Авторизация покупателя. True, если данный покупатель есть в системе
        public bool autorization(string cust_name)
        {
            if (customers.ContainsKey(cust_name) == false)
                return false;

            curCustomer = customers[cust_name];
            customerKey = cust_name;
            return true;
        }

        //Регистрация покупателя
        public int registration(string firstName, string lastName)
        {
            string customer = firstName + '_' + lastName;
            int code = 0;
            string name = customer + '_' + code.ToString();

            //Ликвидация коллизий
            while (customers.ContainsKey(name))
                name = customer + '_' + (++code).ToString();

            customers.Add(name, new Customer());

            //Авторизация
            autorization(name);

            return code;
        }

        /*Комманды B*/

        //Совершение покупки
        public CSReport makeSale(double cost)
        {
            Debug.Assert(cost > 0, "Cost should be positive");
            Debug.Assert(cost < 1000000000, "Cost is very big");

            checkNewMonth();//Проверка текущей даты

            CSReport report = new CSReport(); //Отчет о вызове функции

            curCustomer.purch_sum += cost; //Увеличение баланса покупателя

            //Поиск наибольшей скидки
            report.Discount = Math.Max(cards[curCustomer.mainCard].Discount,
                                    cards[curCustomer.cheerCard].Discount);

            //Стоимоcть со скидкой
            report.CostWithDiscount = cost - cost * (report.Discount * 0.01);

            //Попытка замены карты
            Card card = cards[curCustomer.mainCard];
            report.IsCardChanged = card.IsChanging(curCustomer);
            while (cards[curCustomer.mainCard].IsChanging(curCustomer))
                if (curCustomer.mainCard == 0)
                {
                    cards.Add(cards[curCustomer.mainCard].ChangeCard(curCustomer));
                    curCustomer.mainCard = cards.Count - 1;
                }
                else
                    cards[curCustomer.mainCard] = cards[curCustomer.mainCard].ChangeCard(curCustomer);

            //Попытка выдачи Квантовой карты
            if ((new Random()).Next(10) == 0)
            {
                report.IsCardChanged = true;
                if (curCustomer.mainCard == 0)
                {
                    cards.Add(new Shop.Cards2.QuantumCard());// 
                    curCustomer.mainCard = cards.Count - 1;
                }
                else
                    cards[curCustomer.mainCard] = new Shop.Cards2.QuantumCard();
            }

            return report;
        }

        //Приобретение веселой карты
        public bool getCheerfulCard()
        {
            if (curCustomer.cheerCard > 0)
                return false;

            cards.Add(new CheerfulCard(cheerfulDays));
            curCustomer.cheerCard = cards.Count - 1;

            return true;
        }

        //Аннулирование циклической карты
        public bool cancelCyclicCard()
        {
            if (!(cards[curCustomer.mainCard] is Shop.Cards2.CyclicCard))
                return false;

            (cards[curCustomer.mainCard] as Shop.Cards2.CyclicCard).CancelCard(curCustomer);
            return true;
        }
        

        //Завершение сеанса работы
        public void logOut()
        {
            curCustomer = null;
            customerKey = null;
        }
    
        // Возвращает текущего покупателя
        public Shop.Customer CurrentCustomer
        {
            get
            {
                return curCustomer;
            }
        }

        // Возвращает название карты текущего пользователя
        public string CurCustomerCardName
        {
            get
            {
                if (curCustomer == null)
                    return null;
                return cards[curCustomer.mainCard].FullName;
            }
        }

        // Возвращает скидку карты данного покупателя
        public int CurCustomerCardDiscount
        {
            get
            {
                if (curCustomer == null)
                    return 0;
                return cards[curCustomer.mainCard].Discount;
            }
        }

        // Число клиентов
        public int CustomersCount
        {
            get
            {
                return customers.Count;
            }
        }

        // Число выданных карт
        public int CardCount
        {
            get
            {
                return cards.Count - 1;
            }
        }

        // "Веселые дни"
        public SortedSet<int> CheerfulDays
        {
            get
            {
                return cheerfulDays;
            }
        }

        // Возвращает полное имя и код покупателя
        public string CustomerKey
        {
            get
            {
                return customerKey;
            }
        }

        // Возвращает накопительный баланс покупателя
        public double CustomerBalance
        {
            get
            {
                if (curCustomer == null)
                    return 0;
                return curCustomer.purch_sum;
            }
        }

        // Имеет ли текущий покупатель веселую карту.
        public bool HasCheerfulCard
        {
            get
            {
                if (curCustomer == null)
                    return false;
                return curCustomer.cheerCard > 0;
            }
        }
    }
}
