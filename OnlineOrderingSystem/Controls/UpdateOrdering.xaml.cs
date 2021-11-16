using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using Panuon.UI.Silver.Core;
using Panuon.UI.Silver;
using Interfaces;
using DataDal;

namespace OnlineOrderingSystem.Controls
{
    /// <summary>
    /// UpdateOrdering.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateOrdering : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ICallBack _callBack;
        private Channel _channel;
        public Channel Channel
        {
            get { return _channel; }
            set { _channel = value; PropertyChanged(this, new PropertyChangedEventArgs("Channel")); }
        }
        public UpdateOrdering()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void Init(ICallBack callBack, int id)
        {
            var res = Db.ExecuteQuery("SELECT * FROM PCMedia where id="+id);
            if (res.Read())
            {
                Channel = new Channel();
                Channel.Id = res.GetInt32(0);
                Channel.Name = res.GetString(1);
                Channel.Link = res.GetString(2);
            }
            _callBack = callBack;
        }

         

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!JudgeInput())
            {
                return;
            }            
            string sql = "update PCMedia set name='" + Channel.Name.Trim() + "',link='" + Channel.Link.Trim() + "'where id='" + Channel.Id + "'";           
            var res = Db.ExecuteNonQuery(sql);
            if (res)
            {
                this.Close();
                MessageBoxXConfigurations messageBoxXConfigurations = new MessageBoxXConfigurations();
                messageBoxXConfigurations.MessageBoxIcon = MessageBoxIcon.Success;
                MessageBoxX.Show("操作成功！", "Success", Application.Current.MainWindow, MessageBoxButton.OK, messageBoxXConfigurations);
                _callBack.Refresh(Channel);                
            }
        }
       

        private bool JudgeInput()
        {
            var name = Channel.Name.Trim();
            var link = Channel.Link.Trim();            
            MessageBoxXConfigurations messageBoxXConfigurations = new MessageBoxXConfigurations();
            messageBoxXConfigurations.MessageBoxIcon = MessageBoxIcon.Error;
            if (string.IsNullOrEmpty(name))
            {
                MessageBoxX.Show("名称不能为空！", "Error", Application.Current.MainWindow, MessageBoxButton.OK, messageBoxXConfigurations);
                return false;
            }
            if (string.IsNullOrEmpty(link))
            {
                MessageBoxX.Show("链接不能为空！", "Error", Application.Current.MainWindow, MessageBoxButton.OK, messageBoxXConfigurations);
                return false;
            }
            
            return true;
        }


    }
}
