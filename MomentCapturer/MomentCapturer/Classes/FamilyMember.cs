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
using MomentCapturer.Interfaces;
using SQLite;

namespace MomentCapturer.Classes
{
    public class FamilyMember : IClosePeople
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string Position { get; set; }

        public byte[] PictureBytes { get; set; }

        public FamilyMember()
        {
            this.DateOfCreation = DateTime.Now;
        }

        public FamilyMember(string name, string description,byte[] pictureBytes, string position)
        {
            this.DateOfCreation = DateTime.Now;

            this.Position = position;

            this.Name = name;

            this.Description = description;

            this.PictureBytes = pictureBytes;

        }
    }
}