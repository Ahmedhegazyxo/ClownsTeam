using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Data.SqlClient;
using System.Web;
namespace WebApplication1.Pages
{
    public class loginModel : PageModel
    {

        public string email;
        public string password;




        public string loginQuery()
        {
            var uid = "";

            string constr = "Server=(localdb)\\mssqllocaldb;Database=Thriftly;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select Uid from Account where Email = @Email and password = @Password";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Email", Request.Form["email"].ToString());
                    cmd.Parameters.AddWithValue("@Password", Request.Form["password"].ToString());

                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                          uid = sdr["Uid"].ToString();  
                        }
                    }
                    con.Close();
                }
                
            }
            return uid;
        }

        public void OnGet()
        {


        }
        public void OnPost()
        {
            var uid = loginQuery();
            if (uid != "")
            {
                //Add the Cookie to Browser.
                Response.Cookies.Append("userid",uid);
                Response.Redirect("/user");
            }

        }
    }
}
