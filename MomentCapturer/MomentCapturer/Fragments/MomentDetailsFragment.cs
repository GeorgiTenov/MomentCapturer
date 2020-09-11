using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MomentCapturer.Classes;
using Refractored.Controls;

namespace MomentCapturer.Fragments
{
    public class MomentDetailsFragment : Android.App.DialogFragment
    {
        private TextView date, title, description;

        private ImageView img;

        private View view;

        private string dateText, titleText, descriptionText;

        private byte[] pictureBytes;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.moment_details, container, false);

           
            
            date = view.FindViewById<TextView>(Resource.Id.detailDate);
            date.Text = "Дата: " + dateText;
           

            title = view.FindViewById<TextView>(Resource.Id.detailTitle);
            title.Text = "Заглавие: " + titleText;

            description = view.FindViewById<TextView>(Resource.Id.detailDescription);
            description.Text = "Описание: " + descriptionText;

            img = view.FindViewById<ImageView>(Resource.Id.detailImg);
            img.SetImageBitmap(BitmapFactory
                .DecodeByteArray(pictureBytes,0,pictureBytes.Length));
           
            return view;
        }

     

        public void SetDetails(string date,string title,string description,byte[] picture)
        {
            this.dateText = date;

            this.titleText = title;

            this.descriptionText = description;

            this.pictureBytes = picture;
        }
       
    }
}