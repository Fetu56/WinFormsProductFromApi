using System.Data.SqlClient;

namespace ApiProductCategory
{
    public class ConnectionSingleton
    {
        private static readonly object lck = new object();
        private static ConnectionSingleton instance = null;
        public static ConnectionSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lck)
                    {
                        if (instance == null)
                        {
                            instance = new ConnectionSingleton();
                        }
                    }
                }
                return instance;
            }
        }
        public SqlConnection connection;
        ConnectionSingleton()
        {
            connection = new SqlConnection("Data Source=database-2.clarxrpnfllk.eu-central-1.rds.amazonaws.com;User Id=admin;Password=EgorPrivet123;Initial Catalog=main");
            connection.Open();
        }
    }
}
