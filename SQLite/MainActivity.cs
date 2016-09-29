using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using System;

namespace SQl_Lite
{
    [Activity(Label = "SQl_Lite", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static SQliteDB dbConnection = new SQliteDB();
        static List<SQliteDB.users> data = SQliteDB.ls;
        static List<TableItem> tableItems = new List<TableItem>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            var add = FindViewById<Button>(Resource.Id.button1);
            var delete = FindViewById<Button>(Resource.Id.button2);
            var update = FindViewById<Button>(Resource.Id.button3);
            var select = FindViewById<Button>(Resource.Id.button4);
            add.Click += delegate
            {
                GoToActivity(typeof(ActivityAdd));
            };
            delete.Click += delegate
            {
                GoToActivity(typeof(ActivityDelete));
            };
            select.Click += delegate
            {
                //txt.Text = dbConnection.selectAll();
            };
            update.Click += delegate
            {
                GoToActivity(typeof(ActivityUpdate));
            };
        }
        protected override void OnResume()
        {
            base.OnResume();
            tableItems.Clear();
            data.Clear();
            var listView = FindViewById<ListView>(Resource.Id.listView1);
            data = dbConnection.returnList();
            foreach (var row in data)
            {
                var newItem = new TableItem(row.ID, row.UserName, row.Email, row.Password);
                tableItems.Add(newItem);
            }
            listView.Adapter = new HomeScreenAdapter(this, tableItems);
        }
        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    var listView = FindViewById<ListView>(Resource.Id.listView1);
        //    tableItems.Clear();
        //    data.Clear();
        //    listView.Adapter = new HomeScreenAdapter(this, tableItems);
        //}
        //methode create for start activity
        public void GoToActivity(Type myActivity)
        {
            StartActivity(myActivity);
        }
        // class for view items on list view
        public class HomeScreenAdapter : BaseAdapter<TableItem>
        {
            
            List<TableItem> items;
            Activity context;
            public HomeScreenAdapter(Activity context, List<TableItem> items)
                : base()
            {
                this.context = context;
                this.items = items;
            }
            public override long GetItemId(int position)
            {
                return position;
            }
            public override TableItem this[int position]
            {
                get { return items[position]; }
            }
            public override int Count
            {
                get { return items.Count; }
            }
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var item = items[position];
                View view = convertView;
                if (view == null) // no view to re-use, create new
                    view = context.LayoutInflater.Inflate(Resource.Layout.ItemView, null);
                view.FindViewById<TextView>(Resource.Id.textView1).Text = item.ID.ToString();
                view.FindViewById<TextView>(Resource.Id.textView2).Text = item.UserName;
                view.FindViewById<TextView>(Resource.Id.textView3).Text = item.Email ;
                view.FindViewById<TextView>(Resource.Id.textView4).Text = item.Password;
                return view;
            }
        }
        public class TableItem
        {
            public int ID;
            public string UserName;
            public string Email;
            public string Password;
            public TableItem(int id,string username,string email,string password)
            {
                this.ID = id;
                this.UserName = username;
                this.Email = email;
                this.Password = password;
            }
        }
    }
}

