using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class History
    {
        public string Phone { set; get; }
        public string Dishes { set; get; }
        public string Price { set; get; }
        public string Time { set; get; }

        public History(string dishes,string price,string time)
        {
            Dishes = dishes;
            Price = price;
            Time = time;
        }

        public History(string phone,string dishes, string price, string time)
        {
            Phone = phone;
            Dishes = dishes;
            Price = price;
            Time = time;
        }

    }
}
