using Model;
using OnlineOrderingSystem.DB;
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

namespace OnlineOrderingSystem.Controls
{
    /// <summary>
    /// UpdateOrdering.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateOrdering : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private UpdateOrderingEnum _updateOrderingType;

        private Dish _dish;
        public Dish Dish
        {
            get { return _dish; }
            set { _dish = value; PropertyChanged(this, new PropertyChangedEventArgs("Dish")); }
        }
        public UpdateOrdering()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void Init(UpdateOrderingEnum updateOrderingEnum, int id = 0)
        {
            if (updateOrderingEnum == UpdateOrderingEnum.Update)
            {
                LoadDish(id);
            }
            else
            {
                Dish = new Dish();
            }
            _updateOrderingType = updateOrderingEnum;
        }

        private void LoadDish(int id)
        {
            var sql = "select * from Dishes where id ='" + id + "'";
            XmlDataService ds = new XmlDataService();
            Dish = ds.GetDishes(sql).FirstOrDefault();

        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "png|*.png|jpg|*.jpg";
            if (dialog.ShowDialog() == true)
            {
                Dish.Img = dialog.FileName;
                //var test = new Dish();
                //test.Img= dialog.FileName;
                //Dish = test;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            var img = DealImgFile();
            if (_updateOrderingType == UpdateOrderingEnum.Update)
            {
                sql = "update Dishes set img='" + img + "',name='" + Dish.Name + "',category='" + Dish.Category + "',comment='" + Dish.Comment + "',score='" + Dish.Score + "',price='" + Dish.Price + "' where id='"+Dish.Id+"'";
            }
            else
            {               
                sql = string.Format($"INSERT INTO Dishes(img, name, category, comment, score, price) VALUES ('{img}','{Dish.Name}','{Dish.Category}','{Dish.Comment}', '{Dish.Score}', '{Dish.Price}')");
            }
            var res=Db.ExecuteNonQuery(sql);
            if(res)
            {
                MessageBox.Show("操作成功", "Success");
                this.Close();
            }
        }
        private string DealImgFile()
        {
            var imgFileType = Dish.Img.Split('.').LastOrDefault();
            var fileName = Guid.NewGuid().ToString() + "." + imgFileType;
            var targetPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Img\" + fileName);
            System.IO.File.Copy(Dish.Img, targetPath, true);
            return fileName;
        }


    }
}
