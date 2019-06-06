using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace NotesFragment
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, Icon = "@drawable/notebookicon")]

    public class MainActivity : Activity
    {
        public Bundle _savedInstanceState { get; set; }
        public static MainActivity _mainActivity { get; set; }
        


        protected override void OnCreate(Bundle savedInstanceState)
        {

            _mainActivity = this;

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);


            DatabaseService.dbConnection = new DatabaseService();


            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetActionBar(toolbar);
            ActionBar.Title = "Notebook";
            _mainActivity = this;

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.top_menus, menu);

            return base.OnCreateOptionsMenu(menu);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {

                case Resource.Id.menu_add:
                    AddNote();
                    break;

                case Resource.Id.menu_delete:
                    DeleteDialog();
                    break;

                default:
                    break;

            }
            return base.OnOptionsItemSelected(item);

        }

        
        public void AddNote()
        {

            var emptyNote = new Notes();
            DatabaseService.dbConnection.AddNote(emptyNote);
            this.Recreate();

        }

        public void DeleteDialog()
        {

            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();

            alert.SetTitle("Warning");
            alert.SetMessage("Are you sure you want to delete this note?");
            alert.SetButton("Delete", (c, ev) =>
            {

                DatabaseService.dbConnection.DeleteNote(DatabaseService.noteList[PlayNoteFragment.StaticPlayId].Id);
                DatabaseService.noteList.RemoveAt(PlayNoteFragment.StaticPlayId);

                this.Recreate();

            });

            alert.SetButton2("Cancel", (c, ev) =>
            {

                return;

            });

            alert.Show();
        }


    }

}