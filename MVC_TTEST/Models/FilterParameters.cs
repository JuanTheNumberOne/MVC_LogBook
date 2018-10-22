using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Model of the parameters used to do a filtered search of the database
namespace MVC_TTEST.Models
{
    public class FilterParameters
    {
        public string Date_from { get; set; }
        public string Date_to { get; set;}
        public string User_Selected { get; set;}
        public int Shift_Selected { get; set;}
    }
}