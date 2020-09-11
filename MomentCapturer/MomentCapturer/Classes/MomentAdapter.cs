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
using MomentCapturer.DataContext;
using Refractored.Controls;

namespace MomentCapturer.Classes
{
    public class MomentAdapter : BaseAdapter<Moment>
    {
        private List<Moment> _moments;

        private Context _context;

        private CircleImageView img;
        
        private TextView title, description,date;

        private int position;

        public Context Context
        {
            get { return this._context; }
        }
        public MomentAdapter() { }

        public MomentAdapter(Context context,List<Moment>moments)
        {
            this._context = context;

            this._moments = moments;
        }

        public override Moment this[int position]
        {
            get
            {
                return _moments[position];
            }
        }
            

        public override int Count
        {
            get
            {
                return _moments.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

                if (convertView == null)
                {
                    convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.MomentView, null, false);

                }


                this.position = position;
                date = convertView.FindViewById<TextView>(Resource.Id.date);
                date.Text = "Дата: " + _moments[position].Date.ToString();

                title = convertView.FindViewById<TextView>(Resource.Id.title);
                title.Text = "Заглавие: " + _moments[position].Title;

                description = convertView.FindViewById<TextView>(Resource.Id.description);
                description.Text = "Описание: " + _moments[position].Description;

                img = convertView.FindViewById<CircleImageView>(Resource.Id.img);
                img.SetImageBitmap(BitmapFactory.DecodeByteArray(_moments[position].Picture, 0, _moments[position].Picture.Length));

            return convertView;
        }
      
     
        public void Add(Moment moment)
        {
            this._moments.Add(moment);
           
        }
        
        public void Remove(Moment moment)
        {
            
            this._moments.Remove(moment);
           
        }

      
    }
}