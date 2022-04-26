using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Web;

namespace WebApplication1.Pages
{
    public class RegisterModel : PageModel
    {
        public string firstName;
        public string lastName;
        public string email;
        public string password;
        public string phoneNumber;
        public string address;
        public string confirmPassword;
        public string birthDate;

        public void insertQuery() {
            string constr = "Data Source = localhost\\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "INSERT INTO Account(Email , Password , Birthdate  , UserAdress , FirstName , LastName , PhoneNo) VALUES (@email , @Password , @Birthdate  , @UserAdress , @FirstName , @LastName , @PhoneNo )";
                  
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@email", Request.Form["txtEmail"].ToString());
                    cmd.Parameters.AddWithValue("@Password", Request.Form["password"].ToString());
                    cmd.Parameters.AddWithValue("@Birthdate", Request.Form["birthdate"].ToString());
                    cmd.Parameters.AddWithValue("@UserAdress", Request.Form["address"].ToString());
                    cmd.Parameters.AddWithValue("@FirstName", Request.Form["first-name"].ToString());
                    cmd.Parameters.AddWithValue("@LastName", Request.Form["last-name"].ToString());
                    cmd.Parameters.AddWithValue("@PhoneNo", Request.Form["phone"].ToString());





                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                        }
                    }
                    con.Close();
                }
            }
        }
    
        public void OnGet()
        {
        }
        public void OnPost()
        {

            insertQuery();  
        }
    }
}
