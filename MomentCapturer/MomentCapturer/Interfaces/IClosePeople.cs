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

namespace MomentCapturer.Interfaces
{
    public interface IClosePeople
    {
         int Id { get; set; }

         int UserId { get; set; }

         string Name { get; set; }

         string Description { get; set; }

         DateTime DateOfCreation { get; set; }

        string Position { get; set; }

        byte[] PictureBytes { get; set; }
    }
}