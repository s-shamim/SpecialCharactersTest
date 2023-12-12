using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SpecialCharactersTest.Models;
using System.Data;


namespace SpecialCharactersTest.Data
{
    public class StoredProcedureManager : IStoredProcedureManager
    {
        protected readonly SpecialCharactersTestContext DbContext;

        public StoredProcedureManager(SpecialCharactersTestContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<DataTable> GetDataFromStoredProcedure(string SpName, List<SqlParameter> SpParameters)
        {
            SqlConnection connection = new SqlConnection(DbContext.Database.GetDbConnection().ConnectionString);

            using (connection)
            {
                await connection.OpenAsync();

                SqlCommand cmd = new SqlCommand(SpName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(SpParameters.ToArray());

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0)
                        return ds.Tables[0];
                }
            }
            return null;
        }



        public async Task<bool> UpdateDataFromStoredProcedure(string SpName, List<SqlParameter> SpParameters)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(DbContext.Database.GetDbConnection().ConnectionString);

            using (connection)
            {
                await connection.OpenAsync();

                SqlCommand cmd = new SqlCommand(SpName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(SpParameters.ToArray());

                cmd.ExecuteReader();

                result = true;
            }
            return result;
        }

    }
}
