using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages
{
    public class userModel : PageModel
    {
        public string username = "";
        public void OnGet()
        {
            loginQuery();
        }


        public string loginQuery()
        {
            var uid = "";

            string constr = "Server=(localdb)\\mssqllocaldb;Database=Thriftly;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select FirstName,LastName from Account where Uid = @uid";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@uid", Request.Cookies["userid"]);
                    Console.WriteLine(Request.Cookies["userid"]);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            username = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                        }
                    }
                    con.Close();
                }

            }
            return uid;
        }
    }
}
