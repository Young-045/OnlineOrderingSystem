using NL.MidEnd.MenuView.WPF4.Controls;
using OnlineOrderingSystem.Controls;
using OnlineOrderingSystem.Pages;
using System;
using System.Collections.Generic;
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

namespace OnlineOrderingSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _phone;
        private INaviPage page;
        public MainWindow()
        {
            InitializeComponent();
            PART_UserInfo.IsChecked = true;
        }

        public void SetUser(string phone)
        {
            _phone = phone;
           
            if(page != null)
            {
                page.SetUser(phone);
            }
        }

        private void MenuSampleItem_Checked(object sender, RoutedEventArgs e)
        {
            var control = sender as MenuSampleItem;
            GetNewNaviPage(control.NaviPage);
            PART_Frame.Content = page;           
        }

        private void GetNewNaviPage(NaviPageEnum naviPage)
        {            
            switch (naviPage)
            {
                case NaviPageEnum.UserInfo:
                    page = new UserInfoPage();
                    break;
                case NaviPageEnum.Ordering:
                    page = new OrderingPage();
                    break;
                case NaviPageEnum.History:
                    page = new HistoryPage();
                    break;
                case NaviPageEnum.Collection:
                    page = new CollectionPage();
                    break;
                case NaviPageEnum.Evaluation:
                    page = new EvaluationPage();
                    break;
                default:
                    throw new Exception("Error Page");
            }
            page.SetUser(_phone);
        }


        
    }
}
