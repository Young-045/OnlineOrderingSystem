using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Channel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public int Id { get; set; }
        private string _name;
        public string Name 
        {
            get { return _name; }
            set { _name = value; PropertyChanged(this, new PropertyChangedEventArgs("Name")); }
        }
        private string _link;
        public string Link 
        {
            get { return _link; }
            set { _link = value; PropertyChanged(this, new PropertyChangedEventArgs("Link")); }
        }

        public Channel()
        {

        }

        public Channel(int id,string name,string link)
        {
            this.Id = id;
            this.Name = name;
            this.Link = link;
        }

    }
}
