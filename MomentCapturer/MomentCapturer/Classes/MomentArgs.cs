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

namespace MomentCapturer.Classes
{
    public class MomentArgs : EventArgs
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public MomentArgs(string title,string description,byte[] pictureBytes) : base()
        {
            this.Title = title;

            this.Description = description;

            this.Picture = pictureBytes;
        }
    }
}