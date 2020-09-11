using Android.App;
using Android.OS;

using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Provider;
using System.IO;
using Android.Graphics;
using MomentCapturer.Classes;
using MomentCapturer.DataContext;
using MomentCapturer.Fragments;
using Android.Views;
using System.Linq;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using System.Threading;
using System.ComponentModel;
using Java.IO;
using Android.Net;
using Android.Content.PM;
using Java.Lang;
using Android.Support.V7.App;
using Android.Renderscripts;
using Android.Net.Sip;

namespace MomentCapturer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",ConfigurationChanges = ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true)]
    public class MainActivity : Activity
    {
        private EditText editUsername, editPasssword;

        private Button btnLogin, btnRegister;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.UserLogin);
           
            Data.CreateUserTable();

            editUsername = FindViewById<EditText>(Resource.Id.editUsername);

            editPasssword = FindViewById<EditText>(Resource.Id.editPassword);

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnRegister.Click += BtnRegister_Click;

            btnLogin.Click += BtnLogin_Click;

          
        }

      

        private void Layout_Touch(object sender, View.TouchEventArgs e)
        {
           
        }

        private void BtnRegister_Click(object sender, System.EventArgs e)
        {
            ShowRegisterFragment();
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            User currentUser = new User(editUsername.Text, editPasssword.Text);
            var user = Data.GetUsers()
                .FirstOrDefault(u => u.Username.Equals(currentUser.Username)
                && u.Password.Equals(currentUser.Password));

            if(user != null)
            {
                Intent intent = new Intent(this, typeof(MomentActivity));
                intent.PutExtra("userId", user.Id);
                StartActivity(intent);
            }
            else
            {
                ShowRegisterFragment();
            }
            
        }
        private void ShowRegisterFragment()
        {
            var trans = FragmentManager.BeginTransaction();
            var registerFragment = new RegisterFragment();
            registerFragment.Context = this;
            registerFragment.Show(trans, "Register");
        }

      

       
    }
}