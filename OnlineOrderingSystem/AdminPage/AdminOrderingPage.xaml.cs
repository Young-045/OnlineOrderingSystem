using Model;
using OnlineOrderingSystem.Controls;
using OnlineOrderingSystem.DB;
using System;
using System.Collections.Generic;
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
    /// AdminOrderingPage.xaml 的交互逻辑
    /// </summary>
    public partial class AdminOrderingPage : UserControl, INaviPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private List<Dish> _dishMenu;
        public List<Dish> DishMenu
        {
            get { return _dishMenu; }
            set { _dishMenu = value; PropertyChanged(this, new PropertyChangedEventArgs("DishMenu")); }
        }
        public AdminOrderingPage()
        {
            InitializeComponent();
            Loaded += AdminOrderingPage_Loaded;
            DataContext = this;
        }

        private void AdminOrderingPage_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            XmlDataService ds = new XmlDataService();
            DishMenu = ds.GetDishes();
        }

        public void SetUser(string phone)
        {

        }
        private void LoadAll(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var searchText = SearchText.Text;
            var sql = "select * from Dishes where name like'%" + searchText + "%'";
            XmlDataService ds = new XmlDataService();
            DishMenu = ds.GetDishes(sql);
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            UpdateOrdering updateOrdering = new UpdateOrdering();
            updateOrdering.Init(UpdateOrderingEnum.Add);
            updateOrdering.Show();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var obj = (sender as Button).DataContext.ToString();
            UpdateOrdering updateOrdering = new UpdateOrdering();
            updateOrdering.Init(UpdateOrderingEnum.Update,int.Parse(obj));
            updateOrdering.Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var obj = (sender as Button).DataContext.ToString();
            var sql = "delete  from Dishes where id= " + int.Parse(obj) + "";
            Db.ExecuteNonQuery(sql);
            Init();
        }
    }
}
