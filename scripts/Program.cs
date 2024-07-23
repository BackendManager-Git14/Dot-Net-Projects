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

namespace scripts
{
    class data
    {
        public int id;
        public string u_name;
        public string b_date;
        public string image_path;
        public string email_add;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-QSUUJHO\\VE_ANY;Initial Catalog=db1;Persist Security Info=True;User ID=sa;Password=cha_mudaye14;");

            string date_today = DateTime.Now.ToString("yyyy-MM-dd");
            
            con.Open();

            string qry = "select * from user_data where b_date = @date_today";
            //string qry = "select * from user_data;";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.Add(new SqlParameter("@date_today", date_today));

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            data data = new data
            {
                id = Convert.ToInt32(dr["id"]),
                u_name = dr["u_name"].ToString(),
                b_date = dr["b_date"].ToString(),
                image_path = dr["image_path"].ToString(),
                email_add = dr["email_add"].ToString()
            };

            /* Testing Portion 
             
            string removepath = "~/Visual/";
            string path = data.image_path.Replace(removepath,"");

            string url = "https://raw.githubusercontent.com/BackendManager-Git14/Work_Images/main/Work_Images/Visual/";
            string source = url + path;

            Console.Write(source);
            Console.Read();

            
            Console.WriteLine(data.u_name);
            Console.WriteLine(date_today);
            Console.WriteLine(data.b_date);
            Console.ReadLine();

            */

            
            if (date_today == data.b_date)
            {
                string from_email_addr = "aksharpp2910@gmail.com"; //input sender's gmail add 
                string p_num = "yntf tbup vehl hnar"; //input pswd here 

                // file fetching section// 
             
                string url = "https://raw.githubusercontent.com/BackendManager-Git14/Work_Images/main/Work_Images/Visual/";
                string removepath = "~/Visual/";
                string path = data.image_path.Replace(removepath,url);
                //string source = url + path;

                // Email Sending Logic //

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from_email_addr);
                mailMessage.Subject = "idk";
                mailMessage.To.Add(new MailAddress(data.email_add));
                mailMessage.Body = $@"
                                          <html>
                                            <body> 
                                                your are hack <br>
                                                <img src='{path}' height='200' width='200' />
                                                <h2> Happy Birthday {data.u_name} </h2>
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
