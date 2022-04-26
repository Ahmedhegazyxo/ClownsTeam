using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Web;

namespace WebApplication1.Pages
{
    public class categoryModel : PageModel
    {
        public List<Products> listProducts = new List<Products>();
        //public List<City> listCities = new List<City>();


        private readonly ILogger<IndexModel> _logger;

        public categoryModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            FetchProducts();
        }

        void FetchProducts()
        {
            listProducts.Clear();
            string constr = "Data Source = localhost\\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Product;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            listProducts.Add(new Products(sdr["PId"].ToString(), sdr["PName"].ToString(), Convert.ToInt32(sdr["Price"].ToString()),//Convert.ToInt32("233300")
                                sdr["Size"].ToString(), sdr["Material"].ToString(), Convert.ToInt32(sdr["NumberAvailable"].ToString()), sdr["Color"].ToString(),
                                sdr["Description"].ToString(), sdr["Brand"].ToString(), sdr["Email"].ToString(), sdr["CatId"].ToString(), sdr["Gid"].ToString()));
                        }
                    }
                    con.Close();
                }
            }
        }

    }
}

public class Products
{
    public string PId;
    public string PName;
    public float Price;
    public string Size;
    public string Material;
    public int NumberAvailable;
    public string Color;
    public string Description;
    public string Brand;
    public string Email;
    public string Catid;
    public string GId;

    public  Products(string PId, string PName, float Price, string Size, string Material
        , int NumberAvailable, string Color, string Description, string Brand, string Email
        , string Catid, string GId)
    {
        this.PId = PId;
        this.PName= PName;
        this.Price= Price;
        this.Size= Size;
        this.Material= Material;
        this.NumberAvailable= NumberAvailable;
        this.Color= Color;
        this.Description= Description;
        this.Brand= Brand;
        this.Email= Email;
        this.Catid= Catid;
        this.GId= GId;
}
}