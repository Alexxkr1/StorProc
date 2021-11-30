using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StoredProc.Data;
using StoredProcedure.Models;

namespace StoredProcedure.Controllers
{
    public class OwnersController : Controller
    {
        public StoredProcDbContext _context;
        public IConfiguration _config { get; }

        public OwnersController
            (
            StoredProcDbContext context,
            IConfiguration config
            )
        {
            _context = context;
            _config = config;

        }

        public IActionResult Index()
        {
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchOwners";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<OwnerModel> model = new List<OwnerModel>();
                while (sdr.Read())
                {
                    var details = new OwnerModel();
                    details.company_name = sdr["company_name"].ToString();
                    details.first_name = sdr["first_name"].ToString();
                    details.street_number = Convert.ToInt32(sdr["street_number"]);
                    details.race = sdr["race"].ToString();
                    model.Add(details);
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Index(string company_name, string first_name, int street_number, string race)
        {
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchOwners";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (company_name != null)
                {
                    SqlParameter param = new SqlParameter("@company_name", company_name);
                    cmd.Parameters.Add(param);
                }
                if (first_name != null)
                {
                    SqlParameter param = new SqlParameter("@first_name", first_name);
                    cmd.Parameters.Add(param);
                }
                if (street_number != 0)
                {
                    SqlParameter param = new SqlParameter("@street_number", street_number);
                    cmd.Parameters.Add(param);
                }
                if (race != null)
                {
                    SqlParameter param = new SqlParameter("@race", race);
                    cmd.Parameters.Add(param);
                }
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<OwnerModel> model = new List<OwnerModel>();
                while (sdr.Read())
                {
                    var details = new OwnerModel();
                    details.company_name = sdr["company_name"].ToString();
                    details.first_name = sdr["first_name"].ToString();
                    details.street_number = Convert.ToInt32(sdr["street_number"]);
                    details.race = sdr["race"].ToString();
                    model.Add(details);
                }
                return View(model);
            }
        }
    }
}