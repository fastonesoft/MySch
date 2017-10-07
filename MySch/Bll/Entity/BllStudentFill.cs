using MySch.Models;

namespace MySch.Bll.Entity
{
    public class BllStudentFill : BllEntity<Stud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Mobil2 { get; set; }
        public string Birth { get; set; }
        public string Home { get; set; }
    }
}