using Model;
using OnlineOrderingSystem.Controls;
using DataDal;
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
using Interfaces;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infra;

using Panuon.UI.Silver.Core;
using Panuon.UI.Silver;
using System.Diagnostics;
using System.Xml.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using NPOI.SS.UserModel;

namespace OnlineOrderingSystem.Pages
{
    /// <summary>
    /// OrderingPage.xaml 的交互逻辑
    /// </summary>
    public partial class OrderingPage : UserControl, INotifyPropertyChanged, INaviPage, ICallBack
    {
       
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private bool _isPlay;
        private ObservableCollection<Channel> _channelList;
        public ObservableCollection<Channel> ChannelList
        {
            get { return _channelList; }
            set { _channelList = value; PropertyChanged(this, new PropertyChangedEventArgs("ChannelList")); }
        }
        public OrderingPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += OrderingPage_Loaded;
        }

        private async void OrderingPage_Loaded(object sender, RoutedEventArgs e)
        {
            ChannelList = new ObservableCollection<Channel>();
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;
            DirectoryInfo libDirectory;
            if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.X86)
                libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "LibVlc", "x86"));
            else
                libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "LibVlc", "x64"));
            Logger.Default.Debug("CreatePlayer Start");           
                myControl.SourceProvider.CreatePlayer(libDirectory);//创建视频播放器
            Logger.Default.Debug("CreatePlayer End");
            Vol_Slider.Value = Vol_Slider.Maximum;
            LoadChannel();
        }

        private async void LoadChannel()
        {
            //var handler = PendingBox.Show("Please wait ...", "Processing", false, Application.Current.MainWindow);
            var thr = Task.Factory.StartNew(() => 
            {
                ObservableCollection<Channel> list = new ObservableCollection<Channel>();
                var res = Db.ExecuteQuery("SELECT * FROM PCMedia");
                while (res.Read())
                {
                    Channel channel = new Channel();
                    channel.Id = res.GetInt32(0);
                    channel.Name = res.GetString(1);
                    channel.Link = res.GetString(2);
                    list.Add(channel);
                }
                return list;
            });
            ChannelList = await thr;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            var name = Channel_Name.Text.Trim();
            var link = Channel_Link.Text.Trim();
            if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(link)))
            {
                string sql = string.Format($"INSERT INTO PCMedia(name, link) VALUES ('{name}','{link}')");
                if (DataOp(sql))
                {
                    ShowSuccessMessage();
                }
                var res = Db.ExecuteQuery("SELECT MAX(id) FROM PCMedia");
                Channel channel = new Channel();
                res.Read();
                channel.Id = res.GetInt32(0);
                channel.Name = name;
                channel.Link = link;
                ChannelList.Add(channel);
            }
        }

        private void Vol_play(object sender, RoutedEventArgs e)
        {
            if (_isPlay)
            {
                Button_VolOp.Content = "play";
                myControl.SourceProvider.MediaPlayer.Pause();
                _isPlay = false;
            }
            else
            {
                
                Button_VolOp.Content = "pause";
                myControl.SourceProvider.MediaPlayer.Play();
                _isPlay = true;
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            var op = sender as Button;
            if (op.Tag != null)
            {
                int id = (int)op.Tag;
                UpdateOrdering updateOrdering = new UpdateOrdering();
                updateOrdering.Init(this, id);
                updateOrdering.Show();
            }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var op = sender as Button;
            if (op.Tag != null)
            {
                int id = (int)op.Tag;
                string sql = "Delete From PCMedia where id = " + id;
                if (DataOp(sql))
                {
                    ShowSuccessMessage();
                }
                foreach (var item in ChannelList)
                {
                    if (item.Id == id)
                    {
                        ChannelList.Remove(item);
                        break;
                    }
                }
            }

        }
        private void Button_Play(object sender, RoutedEventArgs e)
        {
            var op = sender as Button;
            if (op.Tag != null)
            {
                string videourl = (string)op.Tag;                
                myControl.SourceProvider.MediaPlayer.Play(new Uri(videourl));
                _isPlay = true;
                Button_VolOp.Content = "pause";
                Button_VolOp.IsEnabled = true;
            }
        }

        private void Button_Import(object sender, RoutedEventArgs e)
        {
            string fName;
            Microsoft.Win32.OpenFileDialog dialog =
                new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = "C:";
            dialog.Filter = "xlsx|*.xlsx|xls|.xls";
            if (dialog.ShowDialog() == true)
            {
                fName = dialog.FileName;
                ExcelHelper excelHelper = new ExcelHelper(fName);
                var res = excelHelper.ExcelToDataTable("Sheet1", true);
                ImportHelper(res);
            }
        }

        private async void ImportHelper(List<Channel> channels)
        {
            List<string> sqls = new List<string>();
            foreach (var item in channels)
            {
                string sql = string.Format($"INSERT INTO PCMedia(name, link) VALUES ('{item.Name}','{item.Link}')");
                sqls.Add(sql);
            }
            Db.ImportExecute(sqls);
            LoadChannel();
        }

        private void Button_Export(object sender, RoutedEventArgs e)
        {
           
        }



        private bool DataOp(string sql)
        {
            return Db.ExecuteNonQuery(sql);
            
               //LoadChannel();
            
        }

        private void ShowSuccessMessage(string msg="")
        {
            MessageBoxXConfigurations messageBoxXConfigurations = new MessageBoxXConfigurations();
            messageBoxXConfigurations.MessageBoxIcon = MessageBoxIcon.Success;
            MessageBoxX.Show("操作成功！", "Success", Application.Current.MainWindow, MessageBoxButton.OK, messageBoxXConfigurations);
        }

        public void Refresh(Channel edit)
        {
            //LoadChannel();
            foreach (var item in ChannelList)
            {
                if (item.Id == edit.Id)
                {                   
                    item.Name = edit.Name;
                    item.Link = edit.Link;
                    break;
                }
            }
        }

        private void Vol_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var val = Vol_Slider.Value;
            myControl.SourceProvider.MediaPlayer.Audio.Volume = (int)val;
        }

        private void dgData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}
