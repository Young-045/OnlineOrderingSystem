using NL.MidEnd.MenuView.WPF4.Controls;
using OnlineOrderingSystem.AdminPage;
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
using System.Windows.Shapes;

namespace OnlineOrderingSystem
{
    /// <summary>
    /// Admin.xaml 的交互逻辑
    /// </summary>
    public partial class Admin : Window
    {
        private INaviPage page;
        public Admin()
        {
            InitializeComponent();
            PART_UsersPage.IsChecked = true;
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
                case NaviPageEnum.Users:
                    page = new UsersPage();
                    break;
                case NaviPageEnum.AdminOrdering:
                    page = new AdminOrderingPage();
                    break;
                case NaviPageEnum.AllHistory:
                    page = new AllHistoryPage();
                    break;
                
                default:
                    throw new Exception("Error Page");
            }
            
        }


    }
}
