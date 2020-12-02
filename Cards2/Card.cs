using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Shop
{
     class Card   
     {
         protected int discount;
         protected string name;

         public Card()
         {
             discount=0;
             name="";

         }
     
       //Необходимость замены карты
         virtual public bool IsChanging(Shop.Customer cust)
         {
             Debug.Assert(cust != null, "owner is empty");
             return cust.purch_sum > DiscountConst.TubeDiscount;
         }


         //замена карты
         virtual public Card ChangeCard(Shop.Customer cust = null)
         {
             return new TubeCard();
         }

         
         virtual public int Discount{get{return discount;}}
       

         
         public string FullName { get { return name; } }
         
     }
 }
