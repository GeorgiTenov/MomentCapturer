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
    public class FamilyFragment : Fragment
    {
        private Button btnAdd, btnSearch;

        private EditText editSearch;

        private ListView list;

        private Context context;

        private int userId;

        private ClosePeopleAdapter<FamilyMember> adapter;

        private List<FamilyMember> members;

        public Context Context
        {
            get { return this.context; }

            set { this.context = value; }
        }

        public FamilyFragment(Context context)
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
            var view = inflater.Inflate(Resource.Layout.family_fragment_view, container, false);

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


            members = Data.GetFamilyMembersByUserId(userId)
               .OrderByDescending(c => c.DateOfCreation)
               .ToList();

            list = view.FindViewById<ListView>(Resource.Id.listView);

            adapter = new ClosePeopleAdapter<FamilyMember>(Context, members);

            list.Adapter = adapter;

            list.ItemLongClick += List_ItemLongClick;

            list.ItemClick += List_ItemClick;

            adapter.NotifyDataSetChanged();


            return view;
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int pos = e.Position;

            FamilyDetailFragment fragment = new FamilyDetailFragment();
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

                .SetMessage("Искате да изтриете този член?")

                .SetPositiveButton("Да", delegate
                {
                    Data.RemoveFamilyMember(adapter[pos]);
                    adapter.Remove(adapter[pos]);
                    adapter.NotifyDataSetChanged();
                })

                .SetNegativeButton("Не", delegate { })

                .Show();

        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            var searchText = editSearch.Text;
            List<FamilyMember> searchedMembers = new List<FamilyMember>();
            if (searchText != null && searchText != "")
            {
                searchedMembers = Data.GetFamilyMembersByUserId(userId)
                    .Where(c => c.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }


            if (searchedMembers != null)
            {

                adapter = new ClosePeopleAdapter<FamilyMember>(Context, searchedMembers);
                list.Adapter = adapter;

                Toast.MakeText(Context, "Намерени: " + searchedMembers.Count + " резултата", ToastLength.Long)
                    .Show();

            }
            if (searchText == null
                || searchText == ""
                || searchedMembers.Count <= 0)
            {
                Toast.MakeText(Context, "Не са намерени членове от семейството", ToastLength.Long)
                    .Show();
                var newMembers = Data.GetFamilyMembersByUserId(userId).ToList();
                adapter = new ClosePeopleAdapter<FamilyMember>(Context, newMembers);
                list.Adapter = adapter;
            }

            editSearch.Text = "";
        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            FamilyDialogFragment fragment = new FamilyDialogFragment();
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

                FamilyMember familyMember = new FamilyMember(e.Name, e.Description, e.PictureBytes, e.Position);
                familyMember.UserId = userId;
                Data.AddFamilyMember(familyMember);
                adapter.Add(familyMember);
                adapter.NotifyDataSetChanged();
            }
            else
            {
                Toast.MakeText(Context, "Трябва да попълните всички полета", ToastLength.Long).Show();
            }
        }

    }
}