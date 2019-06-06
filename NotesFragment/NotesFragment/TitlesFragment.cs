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

namespace NotesFragment
{
    public class TitlesFragment : ListFragment
    {
        int selectedPlayId;
        bool showingTwoFragments;

        DatabaseService dbService;

        public TitlesFragment()
        {

            dbService = new DatabaseService();

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {

            base.OnActivityCreated(savedInstanceState);

            var noteContainer = Activity.FindViewById(Resource.Id.playnote_container);

            var notes = dbService.GetAllNotes();

            List<string> items = new List<string>();

            foreach (var note in notes)
            {

                items.Add(note.Title);

            }

            ListAdapter = new ArrayAdapter<string>(Activity,
                Android.Resource.Layout.SimpleListItemActivated1,
                items);

            if (savedInstanceState != null)
                selectedPlayId = savedInstanceState.GetInt("current_play_id", 0);


            showingTwoFragments = noteContainer != null && noteContainer.Visibility == ViewStates.Visible;

            if (showingTwoFragments)
            {

                ListView.ChoiceMode = ChoiceMode.Single;
                ShowPlayNote(selectedPlayId);

            }

        }

        private void ShowPlayNote(int playId)
        {

            selectedPlayId = playId;
            if (showingTwoFragments)
            {

                ListView.SetItemChecked(selectedPlayId, true);

                var playNoteFragment = FragmentManager.FindFragmentById(Resource.Id.playnote_container)
                    as PlayNoteFragment;

                if (playNoteFragment == null || playNoteFragment.PlayId != playId)
                {

                    var container = Activity.FindViewById(Resource.Id.playnote_container);
                    var noteFrag = PlayNoteFragment.NewInstance(selectedPlayId);

                    FragmentTransaction ft = FragmentManager.BeginTransaction();

                    ft.Replace(Resource.Id.playnote_container, noteFrag);
                    ft.Commit();

                }

            }

            else
            {

                var intent = new Intent(Activity, typeof(PlayNoteActivity));
                intent.PutExtra("current_play_id", playId);
                StartActivity(intent);

            }

        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {

            ShowPlayNote(position);

        }

        public override void OnSaveInstanceState(Bundle outState)
        {

            base.OnSaveInstanceState(outState);
            outState.PutInt("current_play_id", selectedPlayId);

        }

    }

}