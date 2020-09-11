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
    public class Moment
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public DateTime Date { get; set; }

        public Moment() { }

        public Moment(string title,string description,byte[] pictureBytes)
        {
            this.Title = title;

            this.Description = description;

            this.Picture = pictureBytes;

            this.Date = DateTime.Now;
        }

        public string ChangeTitle(string newTitle)
        {
            using(var conn = new SQLiteConnection(Data.FullMemoryPath))
            {
                this.Title = newTitle;
                conn.Update(this);
                return this.Title;
            }
            
        }

        public string ChangeDescription(string description)
        {
            using (var conn = new SQLiteConnection(Data.FullMemoryPath))
            {
                this.Description = description;
                conn.Update(this);
                return this.Description;
            }

        }

    }
}