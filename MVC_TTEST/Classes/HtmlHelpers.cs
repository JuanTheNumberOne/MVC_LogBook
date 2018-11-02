using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_TTEST.Classes
{
    public static class HtmlHelpers
    {

        //Data types that contain the date
        [DataType(DataType.Date)] public static DateTime date_DateFrom { get; set; }
        [DataType(DataType.Date)] public static DateTime date_DateTo { get; set; }

        //Gets the user's function accroding to the number
        public static string Get_User_Function(int nUserFunction)
        {
            string sFunction;
            switch (nUserFunction)
            {
                case 1:
                    sFunction = "Inzynier procesu";
                    break;
                case 2:
                    sFunction = "Inzynier zakladu";
                    break;
                case 3:
                    sFunction = "Mistrz";
                    break;
                case 4:
                    sFunction = "Lider";
                    break;
                case 5:
                    sFunction = "Ustawiacz";
                    break;
                case 6:
                    sFunction = "AIUT";
                    break;
                default:
                    sFunction = "N/A";
                    break;
            }
            return sFunction;
        }
    }

}