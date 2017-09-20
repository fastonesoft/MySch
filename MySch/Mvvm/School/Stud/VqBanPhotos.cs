using MySch.Bll;
using MySch.Models;

namespace MySch.Mvvm.School.Stud
{
    public class VqBanPhotos: BllBase<QrWxStudentUpload>
    {
        public string ID { get; set; }
        //StudGrade中的StudIDS，Student中的IDS
        public string IDS { get; set; }
        public string Num { get; set; }
        public string Name { get; set; }
        public string StudSex { get; set; }
        public int? Score { get; set; }
        public string StudGradeID { get; set; }
    }
}