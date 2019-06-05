using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace NotesApp
{
    public class DatabaseService
    {
        public SQLiteConnection db;

        public static List<Notes> noteList { get; set; }
        public static DatabaseService dbConnection { get; set; }

        public DatabaseService()
        {

            CreateDatabase();

            CreateTableWithData();

            noteList = GetAllNotes().ToList();

        }

        public void CreateDatabase()
        {

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "notesDb01.db3");
            db = new SQLiteConnection(dbPath);

        }

        public Notes GetOneNote(int id)
        {

            return db.Table<Notes>().Where(x => x.Id == id).FirstOrDefault();

        }

        public void AddNote(Notes note)
        {

            note.Title = "";
            note.Note = "";

            db.Insert(note);

        }

        public void UpdateNote(int id, string title, string content)
        {

            var note = new Notes();

            note.Id = id;
            note.Title = title;
            note.Note = content;

            db.Update(note);

        }

        public List<Notes> GetAllNotes()
        {

            var table = db.Table<Notes>();
            return table.ToList();

        }

        public void DeleteNote(int id)
        {

            var note = new Notes();
            note.Id = id;
            db.Delete(note);

        }

        public void CreateTableWithData()
        {

            db.CreateTable<Notes>();
            if (db.Table<Notes>().Count() == 0)
            {

                var newNotes = new Notes();


                newNotes.Title = "Example note";
                newNotes.Note = "Hello! This is an example note.";


                db.Insert(newNotes);

            }

        }
    }
}