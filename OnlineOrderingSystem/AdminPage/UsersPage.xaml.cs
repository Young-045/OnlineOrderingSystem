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
    /// UsersPage.xaml 的交互逻辑
    /// </summary>
    public partial class UsersPage : UserControl, INaviPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private Dictionary<int, string> _dishes;
        private List<User> _userList;
        public List<User> UserList
        {
            get { return _userList; }
            set { _userList = value; PropertyChanged(this, new PropertyChangedEventArgs("UserList")); }
        }
        public UsersPage()
        {
            InitializeComponent();
            Loaded += UsersPage_Loaded;
            DataContext = this;
        }

        private void UsersPage_Loaded(object sender, RoutedEventArgs e)
        {
            _dishes = new Dictionary<int, string>();
            UserList = new List<User>();
            var sql = "select * from UserInfo";
            var res = Db.ExecuteQuery(sql);
            while (res.Read())
            {
                var phone= res.GetString(0);
                if (!phone.Equals("admin"))
                {
                    User user = new User();
                    user.Phone = res.GetString(0);
                    user.UserName = res.GetString(1);
                    user.Address = res.GetString(4);
                    user.Email = res.GetString(5);
                    user.Age = res.GetString(6);
                    user.Sex = res.GetInt32(7);
                    user.Remark = res.GetString(8);
                    user.Col = GetColName(res.GetString(9));

                    UserList.Add(user);
                }
                
            }
        }

        private string GetColName(string col)
        {
            if(string.IsNullOrEmpty(col))
            {
                return "";
            }
            var colNames = new StringBuilder();
            var sql = "select id,name from Dishes";
            var res = Db.ExecuteQuery(sql);
            while (res.Read())
            {
                if(!_dishes.ContainsKey(res.GetInt32(0)))
                {
                    _dishes.Add(res.GetInt32(0), res.GetString(1));
                }
                
            }
            var colAry = col.Split('|');
            foreach(var item in colAry)
            {
                int temp = int.Parse(item);
                var colName = _dishes[temp];
                colNames.Append(colName + ",");
            }
            colNames.Remove(colNames.Length - 1, 1);
            return colNames.ToString();
        }

        public void SetUser(string phone)
        {

        }
    }
}
