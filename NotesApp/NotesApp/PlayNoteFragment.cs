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
using Android.Text;
using System.Threading.Tasks;

namespace NotesApp
{

    public class PlayNoteFragment : Fragment
    {

        public int PlayId => Arguments.GetInt("current_play_id", 0);

        public static int StaticPlayId { get; set; }

        DatabaseService dbService;

        public override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            dbService = new DatabaseService();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            var notes = DatabaseService.dbConnection.GetAllNotes();
            StaticPlayId = PlayId;

            List<int> noteIdList = DatabaseService.noteList.Select(x => x.Id).ToList();
            List<string> titlesList = DatabaseService.noteList.Select(x => x.Title).ToList();
            List<string> notesList = DatabaseService.noteList.Select(x => x.Note).ToList();

            var titleText = new EditText(Activity);
            var contentText = new EditText(Activity);

            var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));

            titleText.SetPadding(padding, padding, padding, padding);
            titleText.TextSize = 24;
            titleText.Tag = noteIdList[PlayId];


            contentText.SetPadding(padding, padding, padding, padding);
            contentText.TextSize = 24;
            contentText.Tag = noteIdList[PlayId];

            try
            {
                titleText.Text = titlesList[PlayId];
                contentText.Text = notesList[PlayId];
            }

            catch
            {
                titleText.Text = titlesList[0];
                contentText.Text = notesList[0];
            }

            if (titleText.Text == "")
            {
                titleText.Hint = "Note's title";
            }

            if (contentText.Text == "")
            {
                contentText.Hint = "Note's content";
            }


            titleText.TextChanged += TitleText_TextChanged;
            contentText.TextChanged += ContentText_TextChanged;

            var linearLayout = new LinearLayout(Activity);


            linearLayout.Orientation = Orientation.Vertical;
            linearLayout.AddView(titleText);
            linearLayout.AddView(contentText);

            var scroller = new ScrollView(Activity);
            scroller.AddView(linearLayout);

            return scroller;
        }


        public void TitleText_TextChanged(object sender, TextChangedEventArgs e)
        {

            var titleTextChanged = (EditText)sender;
            int id = (int)titleTextChanged.Tag;

            DatabaseService.dbConnection.UpdateNote(id, titleTextChanged.Text, dbService.GetOneNote(id).Note);
            Task.Delay(1000);

        }


        public void ContentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            var contentTextChanged = (EditText)sender;
            int id = (int)contentTextChanged.Tag;

            DatabaseService.dbConnection.UpdateNote(id, dbService.GetOneNote(id).Title, contentTextChanged.Text);
            Task.Delay(1000);

        }


        public static PlayNoteFragment NewInstance(int playId)
        {

            var bundle = new Bundle();
            bundle.PutInt("current_play_id", playId);
            return new PlayNoteFragment { Arguments = bundle };

        }

    }

}