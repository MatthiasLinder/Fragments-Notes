﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace NotesFragment
{
    [Activity(Label = "", Theme = "@style/AppTheme.NoActionBar")]

    public class PlayNoteActivity : AppCompatActivity
    {

        private int PlayId { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
                Finish();

            PlayId = Intent.Extras.GetInt("current_play_id", 0);

            var playNoteFrag = PlayNoteFragment.NewInstance(PlayId);

            FragmentManager.BeginTransaction()
                            .Add(Android.Resource.Id.Content, playNoteFrag)
                            .Commit();

        }

    }

}