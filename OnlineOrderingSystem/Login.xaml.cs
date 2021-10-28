using OnlineOrderingSystem.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OnlineOrderingSystem
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private bool _isLogin;
        public bool IsLogin 
        { 
            set 
            { 
                _isLogin = value; 
                PropertyChanged(this, new PropertyChangedEventArgs("IsLogin")); 
            } 
            get { return _isLogin; } 
        }

        public Login()
        {
            InitializeComponent();
            DataContext = this;
            IsLogin = true;
        }

        private void UserLogin(object sender, RoutedEventArgs e)
        {
            var LPhone = LoginPhone.Text;
            var LPassword = LoginPassword.Password;
            var EncryptionPsd = Db.GenerateMD5(LPassword);
            var res=Db.ExecuteQuery("SELECT * FROM UserInfo WHERE phone='"+LPhone+ "' and encryptionPsd='" + EncryptionPsd+"'");
            if(res.Read())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.SetUser(LPhone);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("登录失败", "Error");
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if(IsLogin)
            {
                IsLogin = false;
            }
            else
            {
                var RPhone = RegPhone.Text;
                var RName = RegName.Text;
                var RAddress = RegAddress.Text;
                var RPassword = RegPassword.Password;
                var EncryptionPsd = Db.GenerateMD5(RPassword);
                string sql;
                //var res=Db.ExecuteQuery("SELECT * FROM database WHERE type='table' and name='UserInfo'");
                //if(!res.Read())
                //{
                    
                //}
                sql = "CREATE TABLE IF NOT EXISTS UserInfo(phone VARCHAR(11) PRIMARY KEY, name VARCHAR(20), password VARCHAR(40), address VARCHAR(100));";
                Db.ExecuteNonQuery(sql);
                sql = string.Format($"INSERT INTO UserInfo(phone, name, encryptionPsd, password, address) VALUES ('{RPhone}','{RName}','{EncryptionPsd}','{RPassword}', '{RAddress}')");
                var flag=Db.ExecuteNonQuery(sql);
                if (flag)
                {
                    MessageBox.Show("注册成功","Success");
                }
                else
                {
                    MessageBox.Show("注册失败","Error");
                }
            }
        }

        private void ReturnLogin(object sender, RoutedEventArgs e)
        {
            IsLogin = true;
        }


    }
}
