using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MomentCapturer.DataContext;

namespace MomentCapturer.Fragments
{
    public class RegisterFragment : DialogFragment
    {
        private EditText username, password, confirmPassword;

        private Button btnRegister;

        private Context context;

        public Context Context
        {
            get { return this.context; }

            set { this.context = value; }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
          
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var view = inflater.Inflate(Resource.Layout.UserRegister, container, false);
            Dialog.Window.SetGravity(GravityFlags.Bottom | GravityFlags.FillHorizontal);
            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Black));
            //Create user table
            Data.CreateUserTable();

            //Username,password and confirm password
            username = view.FindViewById<EditText>(Resource.Id.username);
            password = view.FindViewById<EditText>(Resource.Id.password);
            confirmPassword = view.FindViewById<EditText>(Resource.Id.confirmPassword);

            //Button register
            btnRegister = view.FindViewById<Button>(Resource.Id.btnRegister);
            btnRegister.Click += BtnRegister_Click;
            return view;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var user = Data.GetUserByUsername(username.Text);

            bool arePasswordsEqual = password.Text.Equals(confirmPassword.Text); 

            if(user == null && arePasswordsEqual)
            {
                Data.AddUser(new Classes.User(username.Text, password.Text));
                Toast.MakeText(Context, "Добавен потребител: "+username.Text, ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(Context, "Съществува такъв потребител",ToastLength.Long).Show();
            }

            this.Dismiss();
        }
    }
}