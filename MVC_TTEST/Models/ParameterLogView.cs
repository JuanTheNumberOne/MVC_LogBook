using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_TTEST.Models
{
    public class ParameterLogView
    {
        //DataBase columns
        public string Logged_User { get; set; }
        public Nullable<short> User_Group_Number { get; set; }
        public string Parameter_Changed { get; set; }
        public string Actual_Value { get; set; }
        public string Old_Value { get; set; }
        public string Time_Of_Change { get; set; }
        public string Date_Of_Change { get; set; }
        public long ID { get; set; }


    }
}