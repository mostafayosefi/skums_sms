using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace sendsms_vote
{
    /// <summary>
    /// Summary description for sens_sms
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class sens_sms : System.Web.Services.WebService
    {
        sms_srv.DorsaSmsWebService s1 = new sms_srv.DorsaSmsWebService();

        [WebMethod]
        public DataSet send_sms(string reciver_mobile, string NationalID, string student_no)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("result");
            //string from = "300082841111";
            string result_code = "";

            if (reciver_mobile != "")
            {

                //ارسال اس ام اس  
                try
                {
                    //string term = "";
                    string[] stringArray_destination = new string[1] {
            reciver_mobile
          };
                    string[] result = new string[1];
                    string hasstudent = "-1";
                    string flag = "-1";

                     string StudentNationalCode = NationalID;
 
                    if (StudentNationalCode == "1")
                    {
                         
                            hasstudent = "1";

                         
                    }

                    #region دانشچو هست
                    if (hasstudent == "1")
                    {

                         if ( student_no =="1" )
                        { 
                            
                                flag ="2";
                            
                        }
                       
                        if (flag == "1")
                        {
                            #region رای داده
                            result_code = "-2";
                            #endregion
                        }
                        else
                        {
                            ///رای نداده
                            ///
                            //////
                            int r = -1;
                            string random_text = RandomText(); 
                            sms_srv.Message msg = new sms_srv.Message();
                            msg.From = "300082841111";
                            msg.To = reciver_mobile;
                            msg.Text = "کد تصادفی شما برابر است با:" + random_text;
                            msg.Date = "2021-05-12";


                            r = s1.SendMessage("nazarim", "12369", msg);


                            switch (r)
                            {
                                case 10:
                                    result_code = "portalIdentity";
                                    break;
                                case 11:
                                    result_code = "username error";
                                    break;
                                case 12:
                                    result_code = "password error";
                                    break;
                                case 13:
                                    result_code = "from error";
                                    break;
                                case 14:
                                    result_code = "recieversNumber error";
                                    break;
                                case 15:
                                    result_code = "text result";
                                    break;
                                case 16:
                                    result_code = "userLogin failed";
                                    break;
                                case 17:
                                    result_code = "Account Don’t Activate";
                                    break;
                                case 18:
                                    result_code = "Don’t AuthorizedUser";
                                    break;
                                case 19:
                                    result_code = "CheckAccountForSend";
                                    break;
                                case 20:
                                    result_code = "Send sms failed";
                                    break;
                                case 21:
                                    result_code = "Code error";
                                    break;
                                default:
                                    result_code = "1";
                                    break;
                            }
                             

                            ///و بره به  صفحه رای گیری
                        }

                    }

                    #endregion
                    #region دانشجو نیست
                    else
                    {
                        result_code = "-1";
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                       result_code = "-300";
                    //result_code = "-200";  

                  //  result_code = ex.StackTrace+"<br>Hi"+ ex.Message;

                }
            }
            DataRow dr = dt.NewRow();
            dr[0] = result_code;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            return ds;
        }
        private string ConvertToShamsi(DateTime? input)
        {
            DateTime date = new DateTime();
            if (input != null)
            {
                date = input.Value;
            }
            if (input != null)
            {
                PersianCalendar pc = new PersianCalendar();
                return string.Format("{0}/{1}/{2}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date));

            }
            return "";
        }

        public string RandomText()
        {
            string resultTicket = "";

            Random rd = new Random();
            int rand_num = rd.Next(100000, 200000);
            resultTicket = rand_num.ToString();
            return resultTicket;

        }

        //[WebMethod]
        //public string test_sms() {
        //  DateTime dt = new DateTime();
        //  dt = DateTime.Now;
        //  string r = "0";
        //  string[] stringArray_destination = new string[1] {
        //    "09132167842"
        //  };
        //  int t = s1.SendMessageFromDorsaPortal("nazarim", "12345", " 300082841111", stringArray_destination, "test", System.DateTime.Now, "portal");
        //  if (t == 0) {
        //    r = "ok";
        //  }
        //  return r;
        //}

    }
}