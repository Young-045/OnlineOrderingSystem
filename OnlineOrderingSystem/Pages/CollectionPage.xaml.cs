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
    /// CollectionPage.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionPage : UserControl, INaviPage, INotifyPropertyChanged
    {
        private string _phone;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        private List<DishMenuItem> _dishMenu;
        public List<DishMenuItem> DishMenu
        {
            get { return _dishMenu; }
            set { _dishMenu = value; PropertyChanged(this, new PropertyChangedEventArgs("DishMenu")); }
        }

        private List<DishMenuItem> _colDishMenu;
        public List<DishMenuItem> ColDishMenu
        {
            get { return _colDishMenu; }
            set { _colDishMenu = value; PropertyChanged(this, new PropertyChangedEventArgs("ColDishMenu")); }
        }

        public CollectionPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetUser(string phone)
        {
            _phone = phone;
            LoadCol();
        }

        private void LoadDishMenu()
        {
            XmlDataService ds = new XmlDataService();
            var dishes = ds.GetDishes();
            this.DishMenu = new List<DishMenuItem>();
            foreach (var dish in dishes)
            {
                DishMenuItem item = new DishMenuItem();
                item.Dish = dish;
                this.DishMenu.Add(item);
            }
        }

        private void LoadCol()
        {
            string colString = "";
            var res = Db.ExecuteQuery("SELECT * FROM UserInfo where phone='" + _phone + "'");
            while (res.Read())
            {
                colString = res.GetString(9);
            }
            if (DishMenu == null)
            {
                LoadDishMenu();
                ColDishMenu = new List<DishMenuItem>();
            }
            if (colString.Equals(" "))
            {
                return;
            }
            var colAry = colString.Split('|');
            
            foreach (var item in DishMenu)
            {
                for (int i = 0; i < colAry.Length; i++)
                {
                    if (item.Dish.Id == int.Parse(colAry[i]))
                    {
                        ColDishMenu.Add(item);
                        item.IsCol = true;
                    }
                }

            }
        }


        private void Button_Col(object sender, RoutedEventArgs e)
        {

            SaveCol();
        }



        private void SaveCol()
        {
            var col = DishMenu.Where(i => i.IsCol == true).Select(i => i.Dish.Id).ToList();
            var sb = new StringBuilder();
            foreach (var item in col)
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
            //LoadCol();
            


        }



    }
}
