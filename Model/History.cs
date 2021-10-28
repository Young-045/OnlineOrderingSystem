using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class History
    {
        public string Dishes { set; get; }
        public string Price { set; get; }
        public string Time { set; get; }

        public History(string dishes,string price,string time)
        {
            Dishes = dishes;
            Price = price;
            Time = time;
        }
    }
}
