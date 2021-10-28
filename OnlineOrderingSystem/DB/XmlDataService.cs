using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace OnlineOrderingSystem.DB
{
    public class XmlDataService
    {
        public List<Dish> GetDishes()
        {
            List<Dish> dishList = new List<Dish>();
            string ImgFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Img");
            var res = Db.ExecuteQuery("SELECT * FROM Dishes");
            while (res.Read())
            {
                Dish dish = new Dish();
                dish.Id = res.GetInt32(0);
                dish.Img = System.IO.Path.Combine(ImgFile, res.GetString(1));
                dish.Name = res.GetString(2);
                dish.Category = res.GetString(3);
                dish.Comment = res.GetString(4);
                dish.Score = res.GetString(5);
                dish.Price = res.GetString(6);
                dishList.Add(dish);         
            }
            return dishList;
        }

        public List<Dish> GetDishes(string sql)
        {
            List<Dish> dishList = new List<Dish>();
            string ImgFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Img");
            var res = Db.ExecuteQuery(sql);
            while (res.Read())
            {
                Dish dish = new Dish();
                dish.Id = res.GetInt32(0);
                dish.Img = System.IO.Path.Combine(ImgFile, res.GetString(1));
                dish.Name = res.GetString(2);
                dish.Category = res.GetString(3);
                dish.Comment = res.GetString(4);
                dish.Score = res.GetString(5);
                dish.Price = res.GetString(6);
                dishList.Add(dish);
            }
            return dishList;
        }


    }
}
