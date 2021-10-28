using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Dish: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string _img;
        public int Id { get; set; }
        public string Img 
        {
            get { return _img; }
            set { _img = value; PropertyChanged(this, new PropertyChangedEventArgs("Img")); }
        }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public string Score { get; set; }
        public string Price { get; set; }
    }
}
