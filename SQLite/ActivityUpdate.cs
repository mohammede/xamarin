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
using System.Threading.Tasks;

namespace SQl_Lite
{
    [Activity(Label ="ActivityUpdate")]
    class ActivityUpdate : Activity
    {
        static SQliteDB dbConnection = new SQliteDB();
        static List<SQliteDB.users> data = SQliteDB.ls;
        static List<TableItem> tableItems = new List<TableItem>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityUpdate);
            var listView = FindViewById<ListView>(Resource.Id.listView1);
            data.Clear();
            tableItems.Clear();
            data = dbConnection.returnList();
            foreach (var row in data)
            {
                var newItem = new TableItem(row.ID, row.UserName, row.Email, row.Password);
                tableItems.Add(newItem);
            }
            listView.Adapter = new HomeScreenAdapter(this, tableItems);
            listView.ItemClick += ListView_ItemClick;  
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var calDialog = new AlertDialog.Builder(this);
            calDialog.SetTitle("Warning");
            calDialog.SetMessage("Are You Sure To Update This Item");
            calDialog.SetNegativeButton("Cancel", delegate { });
            calDialog.SetPositiveButton("Yes", delegate {
                GoToActivity(typeof(ActivityUpdate2));
            });
            calDialog.Show();
        }
        private void GoToActivity(Type activity)
        {
            StartActivity(activity);
        }
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
                view.FindViewById<TextView>(Resource.Id.textView3).Text = item.Email;
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