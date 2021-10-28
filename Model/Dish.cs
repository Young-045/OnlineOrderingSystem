using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Dish
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public string Score { get; set; }
        public string Price { get; set; }
    }
}
