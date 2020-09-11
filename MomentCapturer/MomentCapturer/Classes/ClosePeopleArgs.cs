using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MomentCapturer.Classes
{
   public class ClosePeopleArgs : EventArgs
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] PictureBytes { get; set; }

        public string Position { get; set; }


        public ClosePeopleArgs(string name, string description,byte[] pictureBytes,string position) : base()
        {
            this.Name = name;

            this.Description = description;

            this.PictureBytes = pictureBytes;

            this.Position = position;
        }
    }
}