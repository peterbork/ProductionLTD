using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace productionltd
{
    public class Product
    {
        public int ID;
        public string Name { get; set; }
        public bool Type { get; set; }
        public string Size { get; set; }
        public List<Process> Processes { get; set; }

        public Product(string name, bool type, string size)
        {
            Name = name;
            Type = type;
            Size = size;
        }
        public void Save()
        {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("addProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.Add(new SqlParameter("@name", Name));
                cmd.Parameters.Add(new SqlParameter("@Size", Size));

                ID = (int)cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                new Alert(e.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
