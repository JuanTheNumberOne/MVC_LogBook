using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using MVC_TTEST.Models;
using MVC_TTEST.Classes;


namespace MVC_TTEST.Controllers
{
    public class TestController : Controller
    {
        // Main page
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        //Table with data records view
        [HttpPost]
        public ActionResult _Logbook(FilterParameters Parameters)
        {

            //Create an entity based in the database
            Entities Logbook_DB = new Entities();

            //Create JSON error model and initialize with 0 values
            Error_Model_JSON Date_Parse_Error = new Error_Model_JSON();
            Date_Parse_Error.Error_Code = 0;
            Date_Parse_Error.Error_Alert = "";
            Date_Parse_Error.Error_Message = "";

            //Create the JSON alerts table
            Error_Model_JSON_Messages Alerts = new Error_Model_JSON_Messages();

            //Get the dates and cast them to the DateTime format
            string sDate_from = Parameters.Date_from;
            string sDate_to = Parameters.Date_to;
            DateTime oDate_From = new DateTime();
            DateTime oDate_To = new DateTime();

            //Get the user
            string sUser = Parameters.User_Selected;

            if (String.IsNullOrEmpty(sUser))
            {
                sUser = "All";
            }

            //Get the shift
            int iShift = Parameters.Shift_Selected;
            DateTime oShift_First =  DateTime.ParseExact ("06:00:00", "HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime oShift_Second = DateTime.ParseExact ("14:00:00", "HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime oShift_Third =  DateTime.ParseExact ("20:00:00", "HH:mm:ss", CultureInfo.InvariantCulture);

            //Parse the string to DateTime format (with exception handling)
            try
            {
                oDate_From = DateTime.ParseExact(sDate_from, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            //If no valid format exception
            catch (System.FormatException ex)
            {
                Date_Parse_Error.Error_Code = 100;
                Date_Parse_Error.Error_Alert = Alerts.Error_Alerts[2];
                Date_Parse_Error.Error_Message = ex.Message;
                return Json(Date_Parse_Error);
            }

            //If null argument exception get an early date
            catch (System.ArgumentNullException ex)
            {
                sDate_from = "01-01-2000";
                oDate_From = DateTime.ParseExact(sDate_from, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            try
            { 
                oDate_To = DateTime.ParseExact(sDate_to, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            //If no valid format exception
            catch (System.FormatException ex)
            {
                Date_Parse_Error.Error_Code = 100;
                Date_Parse_Error.Error_Alert = Alerts.Error_Alerts[3];
                Date_Parse_Error.Error_Message = ex.Message;
                return Json(Date_Parse_Error);
            }

            //If null argument exception get to a very far away date
            catch (System.ArgumentNullException ex)
            {
                sDate_to = "31-12-2100";
                oDate_To = DateTime.ParseExact(sDate_to, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            //Retrieve all the records
            List<USER_PARAMETER_LOGS> Parameter_History_List = Logbook_DB.USER_PARAMETER_LOGS.ToList();

            //Create a list of elements of the retrieved records according to the filter search parameters
            //The thing is that LINQ doesn't recognize the DateTime.ParseExact method when retrieving from the DB model but
            //it does when using it in a list. Life is brutal. Improvise/Adapt/Overcome


            Parameter_History_List = Parameter_History_List.Where
            //List filter parameters (Date, User, Shift)
            (b =>
            //Date
            DateTime.ParseExact(b.Date_Of_Change, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= oDate_From &&
            DateTime.ParseExact(b.Date_Of_Change, "dd-MM-yyyy", CultureInfo.InvariantCulture) <= oDate_To ).Where

            (y=>

            //User
            sUser != "All" ? 
            y.Logged_User == sUser : y.Logged_User != sUser).Where  //Second where is used because && operator was only executed when b.Logged_User != sUser

            (x=>
         
            //Shift
            iShift != 0 ?
                iShift == 1 ?
                    DateTime.ParseExact(x.Time_Of_Change, "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay >= oShift_First.TimeOfDay &&
                    DateTime.ParseExact(x.Time_Of_Change, "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay <= oShift_Second.TimeOfDay
                    :
                    iShift == 2 ?
                        DateTime.ParseExact(x.Time_Of_Change, "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay >= oShift_Second.TimeOfDay &&
                        DateTime.ParseExact(x.Time_Of_Change, "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay <= oShift_Third.TimeOfDay
                        :
                        DateTime.ParseExact(x.Time_Of_Change, "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay >= oShift_Third.TimeOfDay ||
                        DateTime.ParseExact(x.Time_Of_Change, "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay <= oShift_First.TimeOfDay
            : 
            x.Time_Of_Change != null
            )      
            //End of table filters

            //List sort parameters
            .OrderByDescending(x => DateTime.ParseExact(x.Date_Of_Change, "dd-MM-yyyy", CultureInfo.InvariantCulture))
            .ThenByDescending(x => DateTime.Parse(x.Date_Of_Change).TimeOfDay)
            //End sorting

            //Convert to list
            .ToList();
          

            List<ParameterLogView> Parameter_VM_List = Parameter_History_List.Select(x => new ParameterLogView 
            {
                Logged_User = x.Logged_User,
                User_Group_Number = x.User_Group_Number,
                Parameter_Changed = x.Parameter_Changed,
                Actual_Value = x.Actual_Value,
                Old_Value = x.Old_Value,
                Time_Of_Change = x.Time_Of_Change,
                Date_Of_Change = x.Date_Of_Change,
                ID = x.ID
            })
              .ToList();

            return PartialView(Parameter_VM_List);
        }

        

    }
}