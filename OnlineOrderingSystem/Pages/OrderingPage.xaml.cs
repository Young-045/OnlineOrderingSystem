using Model;
using OnlineOrderingSystem.Controls;
using OnlineOrderingSystem.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
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
    /// OrderingPage.xaml 的交互逻辑
    /// </summary>
    public partial class OrderingPage : UserControl, INotifyPropertyChanged, INaviPage
    {
        private string _phone;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value; PropertyChanged(this, new PropertyChangedEventArgs("Count")); }
        }

        private ObservableCollection<DishMenuItem> _selectedDishMenu;
        public ObservableCollection<DishMenuItem> SelectedDishMenu
        {
            get { return _selectedDishMenu; }
            set { _selectedDishMenu = value; PropertyChanged(this, new PropertyChangedEventArgs("SelectedDishMenu")); }
        }

        private List<DishMenuItem> _dishMenu;
        public List<DishMenuItem> DishMenu
        {
            get { return _dishMenu; }
            set { _dishMenu = value; PropertyChanged(this, new PropertyChangedEventArgs("DishMenu")); }
        }
        public OrderingPage()
        {
            InitializeComponent();
            //Loaded += OnLoaded;
            
            
            DataContext = this;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //LoadDishMenu();
            //LoadCol();
        }

        private void LoadDishMenu()
        {

            XmlDataService ds = new XmlDataService();
            var dishes = ds.GetDishes();
            this.DishMenu = new List<DishMenuItem>();
            SelectedDishMenu = new ObservableCollection<DishMenuItem>();
            foreach (var dish in dishes)
            {
                DishMenuItem item = new DishMenuItem();
                item.Dish = dish;
                this.DishMenu.Add(item);
            }
        }

        private void LoadCol()
        {
            string colString="";
            var res = Db.ExecuteQuery("SELECT * FROM UserInfo where phone='"+_phone+"'");
            while (res.Read())
            {
                colString = res.GetString(9);
            }
            if (DishMenu == null)
            {
                LoadDishMenu();
            }
            if (colString.Equals(" "))
            {
                return;
            }
            var colAry = colString.Split('|');
                    
            foreach(var item in DishMenu)
            {
                for (int i = 0; i < colAry.Length; i++)
                {
                    if (item.Dish.Id == int.Parse(colAry[i]))
                    {
                        item.IsCol = true;
                    }
                }
                
            }
        }
       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var order = new StringBuilder();
            foreach(var item in SelectedDishMenu)
            {
                order.Append(item.Dish.Name + "+");
            }
            order.Remove(order.Length - 1, 1);
            var time = DateTime.Now.ToString();
            
            var sql = "CREATE TABLE IF NOT EXISTS UserOrder(id INTEGER PRIMARY KEY, phone VARCHAR(11), orderDish VARCHAR(300), price INTEGER, time VARCHAR(50))";
            Db.ExecuteNonQuery(sql);
            sql = string.Format($"INSERT INTO UserOrder(phone, orderDish, price, time) VALUES ('{_phone}','{order.ToString()}','{Count}', '{time}')");
            var flag = Db.ExecuteNonQuery(sql);
            if (flag)
            {
                MessageBox.Show("订餐成功！", "Success");
            }
            else
            {
                MessageBox.Show("订餐失败！", "Error");
            }
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectedDishMenu.Clear();
            Count = 0;
            foreach (var item in DishMenu)
            {
                if (item.IsSelected)
                {
                    Count += int.Parse(item.Dish.Price.Substring(0, item.Dish.Price.Length - 1));
                    SelectedDishMenu.Add(item);
                }
            }
        }

        public void SetUser(string phone)
        {
            _phone = phone;
            LoadCol();
        }

        private void Button_Col(object sender, RoutedEventArgs e)
        {
            
            SaveCol();
        }

       

        private void SaveCol()
        {
            var col= DishMenu.Where(i => i.IsCol == true).Select(i => i.Dish.Id).ToList();
            var sb = new StringBuilder();
            foreach(var item in col)
            {
                sb.Append(item + "|");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                var sql = "UPDATE UserInfo SET Col='" + sb.ToString() + "' WHERE phone='" + _phone + "'";
                var res = Db.ExecuteNonQuery(sql);
            }
            else
            {
                var sql = "UPDATE UserInfo SET Col=' ' WHERE phone='" + _phone + "'";
                var res = Db.ExecuteNonQuery(sql);
            }
            
        }

    }
}
