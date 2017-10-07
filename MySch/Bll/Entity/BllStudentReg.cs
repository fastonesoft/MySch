using MySch.Models;

namespace MySch.Bll.Entity
{
    public class BllStudentReg : BllEntity<Stud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string IDC { get; set; }
        public string Name { get; set; }
        public string StepIDS { get; set; }
        public string FromSch { get; set; }
        public string Mobil1 { get; set; }
        public string Mobil2 { get; set; }
        public string Memo { get; set; }
        public string AccIDS { get; set; }
        public string RegUID { get; set; }
    }
}