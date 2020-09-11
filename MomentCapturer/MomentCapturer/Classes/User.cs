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
using SQLite;
using MomentCapturer.DataContext;

namespace MomentCapturer.Classes
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public User() 
        {
            this.CreationDate = DateTime.Now;
        }

        public User(string username,string password)
        {
            this.Username = username;

            this.Password = password;

            this.CreationDate = DateTime.Now;
        }

        public string ChangeUsername(string newUsername)
        {
            using (var conn = new SQLiteConnection(Data.FullUserPath))
            {
                this.Username = newUsername;
                conn.Update(this);
                return this.Username;
            }

        }

        public string ChangePassword(string newPassword)
        {
            using (var conn = new SQLiteConnection(Data.FullUserPath))
            {
                this.Password = newPassword;
                conn.Update(this);
                return this.Password;
            }

        }
    }
}