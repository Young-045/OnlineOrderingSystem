using Model;
using OnlineOrderingSystem.Controls;
using OnlineOrderingSystem.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlineOrderingSystem.Pages
{
    /// <summary>
    /// HistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryPage : UserControl, INotifyPropertyChanged, INaviPage
    {
        private string _phone;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ObservableCollection<History> _historyList;
        public ObservableCollection<History> HistoryList
        {
            get { return _historyList; }
            set { _historyList = value; PropertyChanged(this, new PropertyChangedEventArgs("HistoryList")); }
        }

        public HistoryPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void LoadHistory()
        {
            HistoryList = new ObservableCollection<History>();
            var res = Db.ExecuteQuery("SELECT * FROM UserOrder WHERE phone='" + _phone + "'");
            while (res.Read())
            {
                HistoryList.Add(new History(res.GetString(2),res.GetInt32(3).ToString(),res.GetString(4)));
            }
        }

        public void SetUser(string phone)
        {
            _phone = phone;
            LoadHistory();
        }
    }
}
