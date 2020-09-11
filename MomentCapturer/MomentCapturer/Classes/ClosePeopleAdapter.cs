using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MomentCapturer.Interfaces;
using Refractored.Controls;

namespace MomentCapturer.Classes
{
    class ClosePeopleAdapter<T> : BaseAdapter<T> where T : IClosePeople
    {
        private List<T> _people;

        private Context _context;

        private TextView title, description, date,pos;

        private CircleImageView img;

        private int position;

        public Context Context
        {
            get { return this._context; }
        }
        public ClosePeopleAdapter() { }

        public ClosePeopleAdapter(Context context, List<T> people)
        {
            this._context = context;

            this._people = people;
        }

        public override T this[int position]
        {
            get
            {
                return _people[position];
            }
        }


        public override int Count
        {
            get
            {
                return _people.Count;
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
                    convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.collegeus_adapter_view, null, false);
            }


            this.position = position;
            date = convertView.FindViewById<TextView>(Resource.Id.date);
            date.Text = "Дата: " + _people[position].DateOfCreation.ToString();

            title = convertView.FindViewById<TextView>(Resource.Id.name);
            title.Text = "Name: " + _people[position].Name;

            pos = convertView.FindViewById<TextView>(Resource.Id.position);

            if (typeof(T) == typeof(Collegue))
            {
                pos.Text = "Длъжност: " + _people[position].Position;
            }else if(typeof(T) == typeof(FamilyMember))
            {
                pos.Text = "Роднина: " + _people[position].Position;
            }
            else
            {
                pos.Text = "Запознанство: " + _people[position].Position;
            }

            description = convertView.FindViewById<TextView>(Resource.Id.description);
            description.Text = "Описание: " + _people[position].Description;

            img = convertView.FindViewById<CircleImageView>(Resource.Id.capturedImg);
            if(_people[position].PictureBytes != null)
            {
                img.SetImageBitmap(BitmapFactory.DecodeByteArray(_people[position].PictureBytes, 0, _people[position].PictureBytes.Length));
            }
            

            return convertView;
        }


        public void Add(T person)
        {
            this._people.Add(person);

        }

        public void Remove(T person)
        {

            this._people.Remove(person);

        }
     }
 }