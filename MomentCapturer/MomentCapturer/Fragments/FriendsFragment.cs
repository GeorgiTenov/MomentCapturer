using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MomentCapturer.Classes;
using MomentCapturer.DataContext;

namespace MomentCapturer.Fragments
{
    public class FriendsFragment : Fragment
    {
        private Button btnAdd, btnSearch;

        private EditText editSearch;

        private ListView list;

        private Context context;

        private int userId;

        private ClosePeopleAdapter<Friend> adapter;

        private List<Friend> friends;

        public Context Context
        {
            get { return this.context; }

            set { this.context = value; }
        }

        public FriendsFragment(Context context)
        {
            this.context = context;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.friends_fragment_view, container, false);

            btnSearch = view.FindViewById<Button>(Resource.Id.btnSearch);

            btnSearch.Click += BtnSearch_Click;

            editSearch = view.FindViewById<EditText>(Resource.Id.editSearch);

            btnAdd = view.FindViewById<Button>(Resource.Id.btnAdd);

            btnAdd.Click += BtnAdd_Click;

            if (this.Activity.Intent.Extras != null)
            {
                if (this.Activity.Intent.Extras.Get("userId") != null)
                {
                    userId = (int)this.Activity.Intent.Extras.Get("userId");
                }
            }


            friends = Data.GetFriendsByUserId(userId)
               .OrderByDescending(c => c.DateOfCreation)
               .ToList();

            list = view.FindViewById<ListView>(Resource.Id.listView);

            adapter = new ClosePeopleAdapter<Friend>(Context, friends);

            list.Adapter = adapter;

            list.ItemLongClick += List_ItemLongClick;

            list.ItemClick += List_ItemClick;

            adapter.NotifyDataSetChanged();


            return view;
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int pos = e.Position;

            FriendDetailFragment fragment = new FriendDetailFragment();
            var trans = FragmentManager.BeginTransaction();
            fragment.SetDetails(adapter[pos].DateOfCreation.ToString(),
                                adapter[pos].Name,
                                adapter[pos].Description,
                                adapter[pos].PictureBytes,
                                adapter[pos].Position);


            fragment.Show(trans, "Details");
        }

        private void List_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            int pos = e.Position;
            new Android.Support.V7.App.AlertDialog.Builder(Context)
                .SetTitle("Сигурни ли сте")

                .SetMessage("Искате да изтриете този приятел?")

                .SetPositiveButton("Да", delegate
                {
                    Data.RemoveFriend(adapter[pos]);
                    adapter.Remove(adapter[pos]);
                    adapter.NotifyDataSetChanged();
                })

                .SetNegativeButton("Не", delegate { })

                .Show();

        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            var searchText = editSearch.Text;
            List<Friend> searchedFriends = new List<Friend>();
            if (searchText != null && searchText != "")
            {
                searchedFriends = Data.GetFriendsByUserId(userId)
                    .Where(c => c.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }


            if (searchedFriends != null)
            {

                adapter = new ClosePeopleAdapter<Friend>(Context, searchedFriends);
                list.Adapter = adapter;

                Toast.MakeText(Context, "Намерени: " + searchedFriends.Count + " резултата", ToastLength.Long)
                    .Show();

            }
            if (searchText == null
                || searchText == ""
                || searchedFriends.Count <= 0)
            {
                Toast.MakeText(Context, "Не са намерени приятели", ToastLength.Long)
                    .Show();
                var newFriends = Data.GetFriendsByUserId(userId).ToList();
                adapter = new ClosePeopleAdapter<Friend>(Context, newFriends);
                list.Adapter = adapter;
            }

            editSearch.Text = "";
        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            FriendDialogFragment fragment = new FriendDialogFragment();
            var trans = FragmentManager.BeginTransaction();
            fragment.Show(trans, "Dialog Frag");
            fragment.OnCollegueCreate += Fragment_OnCollegueCreate;

        }

        private void Fragment_OnCollegueCreate(object sender, ClosePeopleArgs e)
        {
            if (!e.Name.Equals("")
                 && !e.Description.Equals("")
                 && e.PictureBytes != null
                 && !e.Position.Equals(""))
            {

                Friend friend = new Friend(e.Name, e.Description, e.PictureBytes, e.Position);
                friend.UserId = userId;
                Data.AddFriend(friend);
                adapter.Add(friend);
                adapter.NotifyDataSetChanged();
            }
            else
            {
                Toast.MakeText(Context, "Трябва да попълните всички полета", ToastLength.Long).Show();
            }
        }

    }
}