using System.Data.SqlClient;

namespace WindowsFormsAppLearn
{
    internal class DBConnection
    {
        SqlConnection con = new SqlConnection();

        public SqlConnection getDBConnection() {
            con=new SqlConnection("Data Source=DESKTOP-5KH52MH;Initial Catalog=POSSample;Integrated Security=True;Pooling=False");
            return con;
        }
    }
}
