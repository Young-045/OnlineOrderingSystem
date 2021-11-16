using DataDal;
using OnlineOrderingSystem.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Interfaces;
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
using Infra;


namespace OnlineOrderingSystem.Pages
{
    /// <summary>
    /// CollectionPage.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionPage : UserControl, INaviPage, INotifyPropertyChanged
    {
        //private string _phone;
        //private DataOperation _dataOperation;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //private ObservableCollection<DishMenuItem> _colDishMenu;
        //public ObservableCollection<DishMenuItem> ColDishMenu
        //{
        //    get { return _colDishMenu; }
        //    set { _colDishMenu = value; PropertyChanged(this, new PropertyChangedEventArgs("ColDishMenu")); }
        //}

        //public CollectionPage()
        //{
        //    InitializeComponent();
        //    DataContext = this;
        //}

        //public void InitData(string phone)
        //{
        //    _phone = phone;
        //    _dataOperation = new DataOperation();
        //    ColDishMenu = _dataOperation.LoadCol(phone);
        //}

        //private void Button_Col(object sender, RoutedEventArgs e)
        //{
        //    _dataOperation.SaveCol(_phone, ColDishMenu);
        //}

        


    }
}
