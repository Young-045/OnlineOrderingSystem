using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineOrderingSystem.Controls
{
    public class DishMenuItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Dish Dish { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; PropertyChanged(this, new PropertyChangedEventArgs("IsSelected")); }
        }

        private bool isCol;

        public bool IsCol
        {
            get { return isCol; }
            set { isCol = value; PropertyChanged(this, new PropertyChangedEventArgs("IsCol")); }
        }




    }
}
