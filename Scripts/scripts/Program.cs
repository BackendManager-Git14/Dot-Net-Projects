using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.ComponentModel;

namespace scripts
{
    class data
    {
        public int id;
        public string u_name;
        public DateTime b_date;
        public string image_path;
        public string email_add;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-QSUUJHO\\VE_ANY;Initial Catalog=db1;Persist Security Info=True;User ID=sa;Password=cha_mudaye14;");
            

            DateTime today = DateTime.Today;
            string date_today = DateTime.Now.ToString("MM-dd");

            // condition checking for if there are multiple user with same bday

            con.Open();

            string cnt_qry = "select count(b_date) as date_cnt from user_data where MONTH(b_date) = @month AND DAY(b_date) = @day";
            
            SqlCommand cmd = new SqlCommand(cnt_qry, con);
            cmd.Parameters.Add(new SqlParameter("@month", today.Month));
            cmd.Parameters.Add(new SqlParameter("@day", today.Day));

            int count = (int)cmd.ExecuteScalar();

            //upar ni procedure count ni val fetch krva maate raakheli che to check current date na multiple records che k single record 

            if (count > 1) //jo more than 1 user hashe to aa section trigger thshe
            {
                string multi_qry = "select * from user_data where MONTH(b_date) = @month AND DAY(b_date) = @day";
                cmd = new SqlCommand(multi_qry, con);
                cmd.Parameters.Add(new SqlParameter("@month", today.Month));
                cmd.Parameters.Add(new SqlParameter("@day", today.Day));

                List<data> user_list = new List<data>(); //multiple records store krva list defined
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data user_obj = new data
                    {
                        id = Convert.ToInt16(dr["id"]),
                        u_name = dr["u_name"].ToString(),
                        b_date = (DateTime)dr["b_date"],
                        image_path = dr["image_path"].ToString(),
                        email_add = dr["email_add"].ToString()

                    };
                    user_list.Add(user_obj); //loop ma rei ne object through data list ma store thshe 
                }
                for (int i = 0; i < user_list.Count; i++) //hve upar user_list ma jetli row hshe tetli vakht loop ma email send thshe in my case 4 rows hti to 4 mail aavya even diff email acc ma 
                {
                    if (date_today == user_list[i].b_date.ToString("MM-dd")) //last time condition checking , html body ma list na elements nu access jova nu bhulti nai
                    {
                        string from_email_addr = "aksharpp2910@gmail.com";  
                        string p_num = "yntf tbup vehl hnar";  

                        // file fetching section// 

                        string url = "https://raw.githubusercontent.com/BackendManager-Git14/Work_Images/main/Work_Images/Visual/";
                        string removepath = "~/Visual/";
                        string path = user_list[i].image_path.Replace(removepath, url); // [list_name][index].[Emp_Name] aavi rite list elements access leshe
                                                                                        // or you can try foreach loop

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(from_email_addr);
                        mailMessage.Subject = "idk";
                        mailMessage.To.Add(new MailAddress(user_list[i].email_add));
                        mailMessage.Body = $@"

                                                <html>
                                                <head>

                                                </head>
                                                <body>
                                                <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                                                <tr>
                                                <td align='center'>
                                                    <table class='template' cellpadding='0' cellspacing='0' border='0' background=""https://raw.githubusercontent.com/BackendManager-Git14/Web-Work-1/main/visual%20resources/bday_template.png"" style=""max-width: 487px; width: 487px;background-size: contain;"">
                                                        <tr height=""150px"">
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr height=""26px"">
                                                            <td>
                                                                <p class='name' style=""text-align: center; color: #fff;font-size: 18px;"">{user_list[i].u_name} </p>  
                                                            </td>
                                                        </tr>
                                                        <tr height=""120px"">
                                                            <td align=""center"">
                                                                <table width='100%' cellpadding='0' cellspacing='0' border='0' style=""margin-top: 50px;"">
                                                                    <tr>
                                                                        <td align=""center"">
                                                                            <img src='https://raw.githubusercontent.com/BackendManager-Git14/Web-Work-1/main/visual%20resources/profile.png' alt='User Image' class='user-image' style=""max-width: 110px;"">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr height=""150px"">
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                </tr>
                                                </table>
                                                </body>
                                                </html>


                           ";
                        mailMessage.IsBodyHtml = true;
                        var smtpclient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(from_email_addr, p_num),
                            EnableSsl = true
                        };
                        smtpclient.Send(mailMessage);
                        Console.Read();

                    }

                    else
                    {
                        Console.WriteLine("No birthday Today");
                        Console.Read();
                    }
                }
            } //end of multiuser email logic

            else //idhr current date pr single row fetch thyi hshe to aa logic use thshe
            {
                string single_qry = "select * from user_data where MONTH(b_date) = @month AND DAY(b_date) = @day";
                cmd = new SqlCommand(single_qry, con);
                cmd.Parameters.Add(new SqlParameter("@month", today.Month));
                cmd.Parameters.Add(new SqlParameter("@day", today.Day));

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                data single_user = new data
                {
                    id = Convert.ToInt16(dr["id"]),
                    u_name = dr["u_name"].ToString(),
                    b_date = (DateTime)dr["b_date"],
                    email_add = dr["email_add"].ToString(),
                    image_path = dr["image_path"].ToString()
                };

                dr.Close();
                if (date_today == single_user.b_date.ToString("MM-dd"))
                {
                    string from_email_addr = "aksharpp2910@gmail.com"; 
                    string p_num = "yntf tbup vehl hnar"; 

                    // file fetching section// 

                    string url = "https://raw.githubusercontent.com/BackendManager-Git14/Work_Images/main/Work_Images/Visual/";
                    string removepath = "~/Visual/";
                    string path = single_user.image_path.Replace(removepath, url);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(from_email_addr);
                    mailMessage.Subject = "idk";
                    mailMessage.To.Add(new MailAddress(single_user.email_add));
                    mailMessage.Body = $@"

                                                <html>
                                                <head>

                                                </head>
                                                <body>
                                                <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                                                <tr>
                                                <td align='center'>
                                                    <table class='template' cellpadding='0' cellspacing='0' border='0' background=""https://raw.githubusercontent.com/BackendManager-Git14/Web-Work-1/main/visual%20resources/bday_template.png"" style=""max-width: 487px; width: 487px;background-size: contain;"">
                                                        <tr height=""150px"">
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr height=""26px"">
                                                            <td>
                                                                <p class='name' style=""text-align: center; color: #fff;font-size: 18px;"">{single_user.u_name} </p>  
                                                            </td>
                                                        </tr>
                                                        <tr height=""120px"">
                                                            <td align=""center"">
                                                                <table width='100%' cellpadding='0' cellspacing='0' border='0' style=""margin-top: 50px;"">
                                                                    <tr>
                                                                        <td align=""center"">
                                                                            <img src='https://raw.githubusercontent.com/BackendManager-Git14/Web-Work-1/main/visual%20resources/profile.png' alt='User Image' class='user-image' style=""max-width: 110px;"">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr height=""150px"">
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                </tr>
                                                </table>
                                                </body>
                                                </html>


                           ";
                    mailMessage.IsBodyHtml = true;
                    var smtpclient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(from_email_addr, p_num),
                        EnableSsl = true
                    };
                    smtpclient.Send(mailMessage);
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("No birthday Today");
                    Console.Read();
                }
            }
        }
    }
}


