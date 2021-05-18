using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CarCartelAutoCompany
{
    class SqlConnect
    {
        public SqlConnection sqlConnection()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-9VRV80A;Initial Catalog=dbCarCompany;Integrated Security=True");
            connection.Open();
            return connection;
        }
    }
}
