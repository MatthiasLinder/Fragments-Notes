using SQLite;

namespace NotesFragment
{

    public class Notes
    {

        [PrimaryKey, AutoIncrement, Column("_id")]

        public int Id { get; set; }
        [MaxLength(32)]

        public string Title { get; set; }
        [MaxLength(256)]

        public string Note { get; set; }

    }

}