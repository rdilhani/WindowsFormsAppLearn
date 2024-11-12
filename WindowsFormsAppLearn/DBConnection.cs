using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppLearn
{
    internal class DBConnection
    {
        SqlConnection con = new SqlConnection();

        public SqlConnection getDBConnection() {
            con=new SqlConnection("Data Source=DESKTOP-5KH52MH;Initial Catalog=DIT71;Integrated Security=True;Pooling=False");
            return con;
        }
    }
}
