using MySch.Models;

namespace MySch.Bll.View
{
    public class ViSchBan : BllBase<ViewSchBan>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Num { get; set; }
        public string PartIDS { get; set; }
        public string GradeIDS { get; set; }
        public string PartName { get; set; }
        public string GradeName { get; set; }
        public string Name { get; set; }
        public string TreeName { get; set; }
        public string MasterIDS { get; set; }
        public string MasterName { get; set; }
        public bool Graduated { get; set; }
        public bool IsCurrent { get; set; }
        public bool NotFeng { get; set; }
        public bool OnlyFixed { get; set; }

        public int TakeNum { get; set; }
        public int ChangeNum { get; set; }
        public int Differ { get; set; }
        public bool IsAbs { get; set; }
        public bool SameSex { get; set; }

        public string AccIDS { get; set; }
    }
}