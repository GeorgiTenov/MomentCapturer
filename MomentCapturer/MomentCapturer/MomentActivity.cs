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
using MomentCapturer.Classes;
using MomentCapturer.DataContext;
using MomentCapturer.Fragments;
using Android.Support.V7.App;
using Android.Content.PM;
using static Android.Views.GestureDetector;

namespace MomentCapturer
{
    [Activity(Label = "MomentActivity",ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MomentActivity : Activity
    {
        private Button btnAdd, btnSearch;

        private ListView list;

        private MomentAdapter adapter;

        private List<Moment> moments;

        private EditText editSearch;

        private int userId;

        private Toolbar toolbar;

        private List<View> views;

        private List<Fragment> fragments;

        private Fragment familyFragment, collegeusFragment, friendsFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.SetDisplayShowTitleEnabled(false);
            toolbar.MenuItemClick += Toolbar_MenuItemClick;

            views = new List<View>();

            fragments = new List<Fragment>();

            Data.CreateMemoryTable();

            Data.CreateUserTable();

            Data.CreateCollegueTable();

            Data.CreateFamilyTable();

            Data.CreateFriendTable();

            btnSearch = FindViewById<Button>(Resource.Id.btnSearch);

            btnSearch.Click += BtnSearch_Click;

            editSearch = FindViewById<EditText>(Resource.Id.editSearch);

            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);

            btnAdd.Click += BtnAdd_Click;

            list = FindViewById<ListView>(Resource.Id.listView);
            if(Intent.Extras != null)
            {
                if (Intent.Extras.Get("userId") != null)
                {
                    userId = (int)Intent.Extras.Get("userId");
                }
            }

            //Add views in list
            views.Add(btnSearch);
            views.Add(editSearch);
            views.Add(btnAdd);
            views.Add(list);
            

            moments = Data.GetMomentsByUserId(userId)
                .OrderByDescending(m => m.Date)
                .ToList();

            adapter = new MomentAdapter(this, moments);

            list.Adapter = adapter;

            list.ItemLongClick += List_ItemLongClick;

            list.ItemClick += List_ItemClick;
           
            adapter.NotifyDataSetChanged();

            
        }

        private void Toolbar_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            int id = e.Item.ItemId;

            ShowToolbarFragments(id);
        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            var searchText = editSearch.Text;
            List<Moment> searchedMoments = new List<Moment>();
            if (searchText != null && searchText != "")
            {
                searchedMoments = Data.GetMomentsByUserId(userId)
                    .Where(m => m.Title.ToLower().Contains(searchText.ToLower())).ToList();
            }
            

            if (searchedMoments != null)
            {

                adapter = new MomentAdapter(this, searchedMoments);
                list.Adapter = adapter;

                Toast.MakeText(this, "Намерени: " + searchedMoments.Count + " резултата", ToastLength.Long)
                    .Show();

            }
            if (searchText == null
                || searchText == ""
                || searchedMoments.Count <= 0)
            {
                Toast.MakeText(this, "Не са намерени моменти", ToastLength.Long)
                    .Show();
                var newMoments = Data.GetMomentsByUserId(userId).ToList();
                adapter = new MomentAdapter(this, newMoments);
                list.Adapter = adapter;
            }

            editSearch.Text = "";
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int pos = e.Position;

            MomentDetailsFragment fragment = new MomentDetailsFragment();
            var trans = FragmentManager.BeginTransaction();
            fragment.SetDetails(adapter[pos].Date.ToString(),
                                adapter[pos].Title,
                                adapter[pos].Description,
                                adapter[pos].Picture);

            fragment.Show(trans, "Details");
        }


        private void List_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            int pos = e.Position;
            new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle("Сигурни ли сте")

                .SetMessage("Искате да изтриете този момент?")

                .SetPositiveButton("Да", delegate
                {
                    Data.RemoveMoment(adapter[pos]);
                    adapter.Remove(adapter[pos]);
                    adapter.NotifyDataSetChanged();
                })

                .SetNegativeButton("Не", delegate { })

                .Show();

        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            MomentFragment fragment = new MomentFragment();
            var trans = FragmentManager.BeginTransaction();
            fragment.Show(trans, "Dialog Frag");
            fragment.OnMomentCreate += Fragment_OnMomentCreate;

        }

        private void Fragment_OnMomentCreate(object sender, MomentArgs e)
        {
            if (e.Picture != null
                 && !e.Title.Equals("")
                 && !e.Description.Equals(""))
            {

                Moment moment = new Moment(e.Title, e.Description, e.Picture);
                moment.UserId = userId;
                Data.AddMoment(moment);
                adapter.Add(moment);
                adapter.NotifyDataSetChanged();
            }
            else
            {
                Toast.MakeText(this, "Трябва да попълните всички полета", ToastLength.Long).Show();
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            ShowAlertDialogCloseApp("Сигурни ли сте",
                                    "Искате да излезнете?",
                                    "Да",
                                    "Не");
        }


        private void ShowAlertDialogCloseApp(string title,
                                     string message,
                                     string positiveButton,
                                     string negativeButton)
        {
            new Android.Support.V7.App.AlertDialog.Builder(this)
             .SetTitle(title)

             .SetMessage(message)

             .SetPositiveButton(positiveButton, delegate
             {
                 Finish();
             })

             .SetNegativeButton(negativeButton, delegate { })

             .Show();
        }

      
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void ShowToolbarFragments(int id)
        {
            switch (id)
            {
                case Resource.Id.family:
                    {
                        HideActivity(views);
                       
                        var trans = FragmentManager.BeginTransaction();
                        familyFragment = new FamilyFragment(this);
                        trans.Replace(Resource.Id.container, familyFragment, "Family");
                        trans.AddToBackStack("Family");
                        trans.Commit();
                        fragments.Add(familyFragment);
                        break;
                    }


                case Resource.Id.friends:
                    {
                        HideActivity(views);
                        
                        var trans = FragmentManager.BeginTransaction();
                        friendsFragment = new FriendsFragment(this);
                        trans.Replace(Resource.Id.container, friendsFragment, "Friends");
                        trans.AddToBackStack("Friends");
                        trans.Commit();
                        fragments.Add(friendsFragment);
                        break;
                    }

                case Resource.Id.collegues:
                    {
                        HideActivity(views);
                       
                        var trans = FragmentManager.BeginTransaction();
                        collegeusFragment = new CollegeusFragment(this);
                        trans.Replace(Resource.Id.container, collegeusFragment, "Collegeus");
                        trans.AddToBackStack("Collegeus");
                        trans.Commit();
                        fragments.Add(collegeusFragment);
                        break;
                    }

                case Resource.Id.moments:
                    {
                        RemoveFragments(fragments);
                        ShowActivity(views);
                        break;
                    }

                default:
                    break;

            }
        }

        private void HideActivity(List<View> viewObjects)
        {
            foreach (var item in viewObjects)
            {
                item.Visibility = ViewStates.Gone;
            }
        }

        private void ShowActivity(List<View> viewObjects)
        {
            foreach (var item in viewObjects)
            {
                item.Visibility = ViewStates.Visible;
            }
        }

        private void RemoveFragments(List<Fragment> fragments)
        {
            var trans = FragmentManager.BeginTransaction();
            foreach (var fragment in fragments)
            {
                if (fragment != null)
                {
                    FragmentManager.PopBackStack();

                }
            }
        }
    }
}