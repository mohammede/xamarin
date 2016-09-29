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
    [Activity(Label ="ActivityAdd")]
    class ActivityAdd:Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityAdd);
            var getUserName = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            var getEmail = FindViewById<EditText>(Resource.Id.editText1);
            var getPassword = FindViewById<EditText>(Resource.Id.editText2);
            var submit = FindViewById<Button>(Resource.Id.button1);
            var close = FindViewById<Button>(Resource.Id.button2);
            var connctionDB = new SQliteDB();
            submit.Click += delegate 
            {
                connctionDB.insert(getUserName.Text, getEmail.Text, getPassword.Text);
                Finish();
            };
            close.Click += delegate
            {
                Finish();
            };
        }
    }
}