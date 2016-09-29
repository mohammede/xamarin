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

namespace SQl_Lite
{
    [Activity (Label = "ActivityUpdate2")]
    class ActivityUpdate2 : Activity
    {
        static SQliteDB dbConnection = new SQliteDB();
        static List<SQliteDB.users> data = SQliteDB.ls;
        static List<TableItem> tableItems = new List<TableItem>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var getUserName = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            var getEmail = FindViewById<EditText>(Resource.Id.editText1);
            var getPassword = FindViewById<EditText>(Resource.Id.editText2);
            var submit = FindViewById<Button>(Resource.Id.button1);
            var close = FindViewById<Button>(Resource.Id.button2);

            submit.Click += delegate 
            {
                
            };

        }
        
        public class TableItem
        {
            public int ID;
            public string UserName;
            public string Email;
            public string Password;
            public TableItem(int id, string username, string email, string password)
            {
                this.ID = id;
                this.UserName = username;
                this.Email = email;
                this.Password = password;
            }
        }
    }
}