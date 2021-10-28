using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public string UserName { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string Age { set; get; }
        public int Sex { set; get; }
        public string Remark { set; get; }
        public string Col { get; set; }

        public User()
        {

        }
        public User(string phone,string name,string password,string address,string email,string age,int sex,string remark,string col)
        {
            UserName = name;
            Phone = phone;
            Password = password;
            Address = address;
            Email = email;
            Age = age;
            Sex = sex;
            Remark = remark;
            Col = col;
        }
    }
}
