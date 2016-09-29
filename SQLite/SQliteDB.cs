using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;

namespace SQl_Lite
{
    class SQliteDB
    {
        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "usres.db3");
        public static List<users> ls =new List<users>();
        public SQliteDB()
        {
            if(!File.Exists(path))
            {
                var db = new SQLiteConnection(path);
                db.CreateTable<users>();
            }
        }
        public void insert(string username,string email,string password)
        {
            var db = new SQLiteConnection(path);
            var newUser = new users();
            newUser.UserName = username;
            newUser.Email = email;
            newUser.Password = password;
            db.Insert(newUser);
        }
        public void delete(int id)
        {
            var db =new SQLiteConnection(path);
            var user = new users();
            user.ID = id;
            db.Delete(user);
        }
        public List<users> returnList()
        {
            var db = new SQLiteConnection(path);
            var table = db.Table<users>();
            foreach(var row in table)
            {
                ls.Add(row);
            }
            return ls;
        }
        public string selectAll()
        {
            var db = new SQLiteConnection(path);
            var table = db.Table<users>();
            string data = "";
            int i = 0;
            foreach( var row in table)
            {
                
                data += "row " + ++i +" id = " + row.ID +  " username =  " + row.UserName + ", email = " + row.Email + " password = " + row.Password + "\n";
            }
            return data;
        }
        public void update(int id,string username,string email,string password)
        {
            var db = new SQLiteConnection(path);
            var user = new users();
            user.ID = id;
            user.UserName = username;
            user.Email = email;
            user.Password = password;
            db.Update(user);
        }
        [Table("users")]
        public class users
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            [MaxLength(255)]
            public string UserName { get; set; }
            [MaxLength(255)]
            public string Email { get; set; }
            [MaxLength(255)]
            public string Password { get; set; }
        }
    }
}