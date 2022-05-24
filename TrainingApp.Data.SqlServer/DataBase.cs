using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Data.SqlServer
{
    public abstract class DataBase
    {
        private string _connectionString = "Data Source=DESKTOP-4CAE1CU;Integrated Security=True;Initial Catalog=AdventureWorks2019;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected SqlConnection GetOpenConnection()
        {
            var connecction = new SqlConnection(_connectionString);
            connecction.Open();
            return connecction;
        }

        protected string GetStringField(SqlDataReader reader, int fieldIndex)
        {
            if (reader.IsDBNull(fieldIndex))
                return null;

            return reader.GetString(fieldIndex);
        }
    }
}
