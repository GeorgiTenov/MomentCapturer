using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MomentCapturer.Fragments
{
    public class FamilyDetailFragment : DialogFragment
    {
        private TextView date, name, description, pos;

        private ImageView img;

        private View view;

        private string dateText, titleText, descriptionText, position;

        private byte[] pictureBytes;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.collegue_details_view, container, false);



            date = view.FindViewById<TextView>(Resource.Id.detailDate);
            date.Text = "Дата: " + dateText;


            name = view.FindViewById<TextView>(Resource.Id.detailName);
            name.Text = "Име: " + titleText;

            pos = view.FindViewById<TextView>(Resource.Id.detailPosition);
            pos.Text = "Роднина: " + position;

            description = view.FindViewById<TextView>(Resource.Id.detailDescription);
            description.Text = "Описание: " + descriptionText;

            img = view.FindViewById<ImageView>(Resource.Id.detailImg);
            img.SetImageBitmap(BitmapFactory
                .DecodeByteArray(pictureBytes, 0, pictureBytes.Length));


            return view;
        }



        public void SetDetails(string date, string title, string description, byte[] pictureBytes, string position)
        {
            this.dateText = date;

            this.titleText = title;

            this.descriptionText = description;

            this.pictureBytes = pictureBytes;

            this.position = position;

        }
    }
}