using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Model
    {
        string Connection = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        public IEnumerable<string> Images { get; set; }
        public class SearchItems
        {
            public string Fullname { get; set; }
            public string ProductName { get; set; }
            public string Manufacturer { get; set; }
            public string Salesrating { get; set; }
            public string Title { get; set; }
            public string Price { get; set; }
            public string Listprice { get; set; }
        }

        public DataSet ShowSearchData(SearchItems objinfo)
        {
            DataSet ds;
            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SPO_SearchDynamic ";
                    cmd.Parameters.Add(new SqlParameter("@fullname", SqlDbType.VarChar, 100)).Value = objinfo.Fullname;
                    cmd.Parameters.Add(new SqlParameter("@productname", SqlDbType.VarChar, 50)).Value = objinfo.ProductName;
                    cmd.Parameters.Add(new SqlParameter("@manufacturer", SqlDbType.VarChar, 50)).Value = objinfo.Manufacturer;
                    cmd.Parameters.Add(new SqlParameter("@rating", SqlDbType.VarChar, 10)).Value = objinfo.Salesrating;
                    cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 200)).Value = objinfo.Title;
                    cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.VarChar, 100)).Value = objinfo.Price;
                    cmd.Parameters.Add(new SqlParameter("@listprice", SqlDbType.VarChar, 100)).Value = objinfo.Listprice;
                 
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        ds = new DataSet();
                        da.Fill(ds);
                        con.Close();
                        da.Dispose();
                    }
                }
            }
            return ds;
        }
    }
}