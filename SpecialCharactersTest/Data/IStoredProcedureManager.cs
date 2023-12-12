using Microsoft.Data.SqlClient;
using System.Data;

namespace SpecialCharactersTest.Data
{
    public interface IStoredProcedureManager
    {
        public Task<DataTable> GetDataFromStoredProcedure(string SpName, List<SqlParameter> SpParameters);
        public Task<bool> UpdateDataFromStoredProcedure(string SpName, List<SqlParameter> SpParameters);
    }
}
