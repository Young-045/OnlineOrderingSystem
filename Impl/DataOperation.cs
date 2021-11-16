using DataDal;
using Infra;
using Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impl
{
    public class DataOperation : IDataOperation
    {
    //    public ObservableCollection<DishMenuItem> LoadCol(string phone)
    //    {
    //        string colString = "";
    //        var res = Db.ExecuteQuery("SELECT * FROM UserInfo where phone='" + phone + "'");
    //        while (res.Read())
    //        {
    //            colString = res.GetString(9);
    //        }
    //        var dishMenu = LoadDishMenu();
    //        var colDishMenu = new ObservableCollection<DishMenuItem>();        
    //        if (string.IsNullOrEmpty(colString)||colString.Equals(" "))
    //        {
    //            return colDishMenu;
    //        }
    //        var colAry = colString.Split('|');
    //        foreach (var item in dishMenu)
    //        {
    //            for (int i = 0; i < colAry.Length; i++)
    //            {
    //                if (item.Dish.Id == int.Parse(colAry[i]))
    //                {
    //                    colDishMenu.Add(item);
    //                    item.IsCol = true;
    //                }
    //            }
    //        }
    //        return colDishMenu;

    //    }

    //    public void LoadCol(string phone, ObservableCollection<DishMenuItem> dishMenu)
    //    {
    //        string colString = "";
    //        var res = Db.ExecuteQuery("SELECT * FROM UserInfo where phone='" + phone + "'");
    //        while (res.Read())
    //        {
    //            colString = res.GetString(9);
    //        }
    //        if (dishMenu == null)
    //        {
    //            LoadDishMenu();
    //        }
    //        if (string.IsNullOrEmpty(colString) || colString.Equals(" "))
    //        {
    //            return;
    //        }
    //        var colAry = colString.Split('|');
    //        foreach (var item in dishMenu)
    //        {
    //            for (int i = 0; i < colAry.Length; i++)
    //            {
    //                if (item.Dish.Id == int.Parse(colAry[i]))
    //                {
    //                    item.IsCol = true;
    //                }
    //            }

    //        }
    //    }

    //    public ObservableCollection<DishMenuItem> LoadDishMenu()
    //    {
    //        DataService ds = new DataService();
    //        var dishes = ds.GetDishes();
    //        var dishMenu = new ObservableCollection<DishMenuItem>();
    //        foreach (var dish in dishes)
    //        {
    //            DishMenuItem item = new DishMenuItem();
    //            item.Dish = dish;
    //            dishMenu.Add(item);
    //        }
    //        return dishMenu;
    //    }

    //    public ObservableCollection<History> LoadHistory(string phone = null)
    //    {
    //        var historyList = new ObservableCollection<History>();
    //        string sql;
    //        if(string.IsNullOrEmpty(phone))
    //        {
    //            sql = "SELECT * FROM UserOrder";
    //        }
    //        else
    //        {
    //            sql = "SELECT * FROM UserOrder WHERE phone='" + phone + "'";
    //        }
    //        var res = Db.ExecuteQuery(sql);            
    //        while (res.Read())
    //        {
    //            historyList.Add(new History(res.GetString(1), res.GetString(2), res.GetInt32(3).ToString(), res.GetString(4)));
    //        }
    //        return historyList;
    //    }

    //    public void SaveCol(string phone, ObservableCollection<DishMenuItem> dishMenu)
    //    {
    //        var col = dishMenu.Where(i => i.IsCol == true).Select(i => i.Dish.Id).ToList();
    //        var sb = new StringBuilder();
    //        foreach (var item in col)
    //        {
    //            sb.Append(item + "|");
    //        }
    //        if (sb.Length > 0)
    //        {
    //            sb.Remove(sb.Length - 1, 1);
    //            var sql = "UPDATE UserInfo SET Col='" + sb.ToString() + "' WHERE phone='" + phone + "'";
    //            var res = Db.ExecuteNonQuery(sql);
    //        }
    //        else
    //        {
    //            var sql = "UPDATE UserInfo SET Col=' ' WHERE phone='" + phone + "'";
    //            var res = Db.ExecuteNonQuery(sql);
    //        }
    //    }


    }
}
