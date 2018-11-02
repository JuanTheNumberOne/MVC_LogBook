using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_TTEST.Models
{
    public class User_Model
    {
        public List<string> User_List { get; set; }

        public User_Model()
        {
            User_List = new List<string>();
        }
    }
}