using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Web;


namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {

        public List<Governorate> listGovernates = new List<Governorate>();
        public List<Category> listCategories = new List<Category>();
        public List<City> listCities = new List<City>();
        public List<Products> listProducts = new List<Products>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /*public List<string> getCities(string Gid)
        {
            var list = new List<string>();
            listCities.ForEach(c =>
            {
                if (c.Gid == Gid) 
                    list.Add(c.CityName_en); 

            });

            return list;
        }*/

        public void OnPost()
        {
            SerchProduct();


        }

        public void OnGet()

        {
            FetchCities();
            FetchCategory();
            FetchGovernates();
            


        }

        void SerchProduct()
        {
            listGovernates.Clear();
            string constr = "Data Source = localhost\\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM governorate;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            listGovernates.Add(new Governorate(sdr["Gid"].ToString(), sdr["GName_en"].ToString()));
                        }
                    }
                    con.Close();
                }
            }


        }

        void FetchGovernates()
        {
            listGovernates.Clear();
            string constr = "Data Source = localhost\\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM governorate;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            listGovernates.Add(new Governorate(sdr["Gid"].ToString(), sdr["GName_en"].ToString()));
                        }
                    }
                    con.Close();
                }
            }
        }
        void FetchCategory()
        {
            listGovernates.Clear();
            string constr = "Data Source = localhost\\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Category;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            listCategories.Add(new Category(sdr["CatId"].ToString(), sdr["CName"].ToString()));
                        }
                    }
                    con.Close();
                }
            }
        }
        void FetchCities()
        {
            listCities.Clear();
            //string constr = "Server=(localdb)\\mssqllocaldb;Database=Thriftly;Trusted_Connection=True;MultipleActiveResultSets=true";
            string constr = "Data Source = localhost\\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM City;";
                using (SqlCommand cmd = new SqlCommand(query)) //new constr    Data Source = localhost\MSSQLSERVER01; Initial Catalog = model; Integrated Security = True
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            listCities.Add(new City(sdr["CityName_en"].ToString(), sdr["CityId"].ToString(), sdr["Gid"].ToString()));
                        }
                    }
                    con.Close();
                }
            }
        }


    }

       


       
}



public class Governorate
{
    public string gId;
    public string gNameEn;

    public Governorate (string id , string name)
    {
        this.gId = id;  
        this.gNameEn = name;    
    }
}
public class Category
{
    public string CatId;
    public string CName;

    public Category(string CatId, string CName)
    {
        this.CatId = CatId;
        this.CName = CName;
    }
}
public class City
{

    public string CityName_en;

    public string CityId;

    public string Gid;
    public City (string name , string id , string govId)
    {
        this.CityId = id;
        this.CityName_en = name;
        this.Gid = govId;   
    }
}
public class Product
{

    public string CityName_en;

    public string CityId;

    public string Gid;
    public Product(string name, string id, string govId)
    {
        this.CityId = id;
        this.CityName_en = name;
        this.Gid = govId;
    }
}