using Infra;
using Interfaces;
using Model;
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
        //private string _phone;
        private INaviPage page;
        public MainWindow()
        {
            InitializeComponent();
            PART_Ordering.IsChecked = true;
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
                
                case NaviPageEnum.Ordering:
                    page = new OrderingPage();
                    break;
                
                    break;
                case NaviPageEnum.Collection:
                    //page = new CollectionPage();
                    break;
               
                default:
                    throw new Exception("Error Page");
            }
           
        }


        
    }
}
