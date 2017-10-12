using MySch.Models;

namespace MySch.Bll.Entity
{
    public class BllStudMove : BllEntity<StudGradeMove>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        public string BanIDS { get; set; }

        public string OwnerIDS { get; set; }
    }
}