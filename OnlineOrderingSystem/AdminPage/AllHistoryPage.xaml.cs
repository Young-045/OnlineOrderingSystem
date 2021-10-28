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

namespace OnlineOrderingSystem.AdminPage
{
    /// <summary>
    /// AllHistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class AllHistoryPage : UserControl, INaviPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ObservableCollection<History> _historyList;
        public ObservableCollection<History> HistoryList
        {
            get { return _historyList; }
            set { _historyList = value; PropertyChanged(this, new PropertyChangedEventArgs("HistoryList")); }
        }
        public AllHistoryPage()
        {
            InitializeComponent();
            Loaded += AllHistoryPage_Loaded;
            DataContext = this;
        }

        private void AllHistoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHistory();
        }

        private void LoadHistory()
        {
            HistoryList = new ObservableCollection<History>();
            var res = Db.ExecuteQuery("SELECT * FROM UserOrder");
            while (res.Read())
            {
                HistoryList.Add(new History(res.GetString(1),res.GetString(2), res.GetInt32(3).ToString(), res.GetString(4)));
            }
        }

        public void SetUser(string phone)
        {

        }
    }
}
