using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace SqlRepository.MySql
{
    interface IMySqlDb
    {
        #region Fetch Collections

        DataSet FetchDataSet(string query);
        DataTable FetchDataTable(string sp, List<MySqlParameter> param = null);

        #endregion

        #region Fetch Scalar Values

        int FetchInt(string query, List<MySqlParameter> param = null);
        double FetchDouble(string query);
        string FetchString(string sp, List<MySqlParameter> param = null);
        bool FetchBoolean(string query, List<MySqlParameter> param = null);

        #endregion

        #region Execute NonQuery

        void ExecuteNonQuery(string sp, List<MySqlParameter> param = null);

        #endregion
    }
}
