using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_TTEST.Models
{
    public class Error_Model_JSON
    {
        public int Error_Code { get; set; }
        public string Error_Alert { get; set; }
        public string Error_Message { get; set; }
    }


    //Array of strings containing all possible predefined alerts
    public class Error_Model_JSON_Messages
    {
        private string[] _Error_Alerts;
        public string[] Error_Alerts
        {

            get
            {
                return _Error_Alerts;
            }


            set
            {
                for (int i = 0; i < _Error_Alerts.Length; i++)
                {
                    if (_Error_Alerts[i] != null) _Error_Alerts[i] = _Error_Alerts[i].Trim();
                }
            }
        }

        //Konstruktor
        public Error_Model_JSON_Messages()
        {
            _Error_Alerts = new string[5];
            _Error_Alerts[0] = "No date provided in 'Date From' input. Use the pop-up calendar or write the date in dd-mm-yyyy format (example: 01-01-2000)";
            _Error_Alerts[1] = "No date provided in 'Date To' input. Use the pop-up calendar or write the date in dd-mm-yyyy format (example: 01-01-2000)";
            _Error_Alerts[2] = "Date provided in 'Date From' input has invalid format. Use the pop-up calendar or write the date in dd-mm-yyyy format (example: 01-01-2000)";
            _Error_Alerts[3] = "Date provided in 'Date To' input has invalid format. Use the pop-up calendar or write the date in dd-mm-yyyy format (example: 01-01-2000)";

            //Second way of initializing an array
            //{
            //    "No date provided in Date From input. Use the pop-up calendar or write the date in dd-mm-yyyy format (example: 01-01-2000)", 
            //    "No date provided in Date To input. Use the pop-up calendar or write the date in dd-mm-yyyy format (example: 01-01-2000)"    

            //};
        }
    }
}