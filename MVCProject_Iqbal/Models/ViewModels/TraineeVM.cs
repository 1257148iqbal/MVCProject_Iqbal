using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject_Iqbal.Models.ViewModels
{
    public class TraineeVM
    {
        public int TraineeID { get; set; }
        public string TraineeName { get; set; }
        public string EmailAddress { get; set; }
        public string CellPhone { get; set; }
        public string ContactAddress { get; set; }
        public Nullable<int> TeacherID { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}