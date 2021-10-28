using Model;
using OnlineOrderingSystem.Controls;
using OnlineOrderingSystem.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// UserInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoPage : UserControl, INotifyPropertyChanged, INaviPage
    {
        private User _user=new User();

#region

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string UserName
        {
            set 
            {
                _user.UserName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
            get 
            { 
                return _user.UserName; 
            }
        }
        public string Phone
        {
            set
            {
                _user.Phone = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
            get 
            { 
                return _user.Phone; 
            }
        }
        public string Password
        {
            set
            {
                _user.Password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
            get
            {
                return _user.Password;
            }
        }
        public string Address
        {
            set
            {
                _user.Address = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Address"));
            }
            get
            {
                return _user.Address;
            }
        }

        public string Email
        {
            set
            {
                _user.Email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
            get
            {
                return _user.Email;
            }
        }
        public string Age
        {
            set
            {
                _user.Age = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Age"));
            }
            get
            {
                return _user.Age;
            }
        }
        public int Sex
        {
            set
            {
                _user.Sex = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Sex"));
            }
            get
            {
                return _user.Sex;
            }
        }
        public string Remark
        {
            set
            {
                _user.Remark = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Remark"));
            }
            get
            {
                return _user.Remark;
            }
        }
        #endregion
        public UserInfoPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetUser(string phone)
        {
            var res = Db.ExecuteQuery("SELECT * FROM UserInfo WHERE phone='" + phone + "'");
            if (res.Read())
            {
                Phone = phone;
                //_user = new User(phone, res.GetString(1), passsword, res.GetString(3));
                UserName = res.GetString(1);
                Password = res.GetString(3);
                Address = res.GetString(4);
                Email = res.GetString(5);
                Age = res.GetString(6);
                Sex = res.GetInt32(7);
                Remark = res.GetString(8);
            }
        }

        private void SaveUserInfo(object sender, RoutedEventArgs e)
        {
            var EncryptionPsd = Db.GenerateMD5(Password);
            var sql = "UPDATE UserInfo SET name='"+UserName+ "',encryptionPsd='" + EncryptionPsd+"',password='"+Password+"',email='"+Email+"',age='"+Age+"',sex='"+Sex+"',remark='"+Remark+"',address='"+Address+"' WHERE phone='"+Phone+"'";
            var res = Db.ExecuteNonQuery(sql);
            if(res)
            {
                MessageBox.Show("修改成功", "Success");
            }
            else
            {
                MessageBox.Show("修改失败", "Error");
            }
        }

        
    }
}
