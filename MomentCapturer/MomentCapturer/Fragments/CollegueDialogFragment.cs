using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MomentCapturer.Classes;
using Refractored.Controls;

namespace MomentCapturer.Fragments
{
    public class CollegueDialogFragment : DialogFragment
    {
        private EditText _editName, _editDescription,_position;
        private Button _saveBtn,_captureBtn;
        private CircleImageView _img;

        
        public event EventHandler<ClosePeopleArgs> OnCollegueCreate;
        private byte[] _pictureBytes;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var view = inflater.Inflate(Resource.Layout.add_collegue_fragment, container, false);
            _editName = view.FindViewById<EditText>(Resource.Id.editName);
            _editDescription = view.FindViewById<EditText>(Resource.Id.editDescription);
            _saveBtn = view.FindViewById<Button>(Resource.Id.saveBtn);
            _captureBtn = view.FindViewById<Button>(Resource.Id.captureBtn);
            _position = view.FindViewById<EditText>(Resource.Id.editPosition);

            //Add
            _saveBtn.Click += _btnAdd_Click;

            _img = view.FindViewById<CircleImageView>(Resource.Id.capturedImg);

            //capture
            _captureBtn.Click += BtnCapture_Click;

           
            return view;


        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            if (e != null)
            {
                this.OnCollegueCreate.Invoke(this, new ClosePeopleArgs(_editName.Text,
                                                           _editDescription.Text,
                                                           _pictureBytes,
                                                           _position.Text));
            }

            this.Dismiss();
        }

        private void BtnCapture_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //base.OnActivityResult(requestCode, resultCode, data);
            if (data != null)
            {
                Bitmap bitmap = data.Extras.Get("data") as Bitmap;
                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    _pictureBytes = stream.ToArray();
                    _img.SetImageBitmap(bitmap);
                }

            }
        }


    }
}