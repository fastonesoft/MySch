using MySch.Models;

namespace MySch.Bll.Entity
{
    public class BllStudentDrop : BllEntity<Stud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        //修改：校区分级属性，降级
        public string StepIDS { get; set; }
    }
}