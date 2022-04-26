using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Web;


namespace WebApplication1.Pages
{
    public class registerModel : PageModel
    {
        
        public string PName;
        public float Price;
        public string Size;
        public string Material;
        public int NumberAvailable;
        public string Color;
        public string Description;
        public string Brand;
       


        public void AddProduct()
        {
            string constr = "Data Source = localhost\\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "INSERT INTO Product(PName , Price , Size , Material , NumberAvailable , Color , Description , Brand,Email) VALUES (@PName , @Price , @Size  , @Material , @NumberAvailable , @Color, @Description, @Brand, @Email)";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@PName", Request.Form["PName"].ToString());
                    cmd.Parameters.AddWithValue("@Price", Request.Form["Price"].ToString());
                    cmd.Parameters.AddWithValue("@Size", Request.Form["Size"].ToString());
                    cmd.Parameters.AddWithValue("@Material", Request.Form["Material"].ToString());
                    cmd.Parameters.AddWithValue("@NumberAvailable", Request.Form["NumberAvailable"].ToString());
                    cmd.Parameters.AddWithValue("@Color", Request.Form["Color"].ToString());
                    cmd.Parameters.AddWithValue("@Description", Request.Form["Description"].ToString());
                    cmd.Parameters.AddWithValue("@Brand", Request.Form["Brand"].ToString());
                    cmd.Parameters.AddWithValue("@Email", Request.Form["Email"].ToString());



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

            AddProduct();
        }
    }
}
