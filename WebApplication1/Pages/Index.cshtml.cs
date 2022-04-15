using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Web;


namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {

        public List<Governorate> listGovernates = new List<Governorate>();
        public List<City> listCities = new List<City>();
        

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<string> getCities(string gid)
        {
            var list = new List<string>();
            listCities.ForEach(c =>
            {
                if (c.Gid == gid)
                    list.Add(c.CityName_en);
            });

            return list;
        }

        public void OnGet()

        {
            FetchCities();  
            FetchGovernates();
        }
        void FetchCities()
        {
            listCities.Clear();
            string constr = "Server=(localdb)\\mssqllocaldb;Database=Thriftly;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM City;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            listCities.Add(new City( sdr["CityName_en"].ToString() , sdr ["CityId"].ToString(), sdr["Gid"].ToString()));
                        }
                    }
                    con.Close();
                }
            }
        }


        void FetchGovernates() {
            listGovernates.Clear();
            string constr = "Server=(localdb)\\mssqllocaldb;Database=Thriftly;Trusted_Connection=True;MultipleActiveResultSets=true";
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
                            listGovernates.Add(new Governorate(sdr["Gid"].ToString(),sdr["GName_en"].ToString()));                    
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
