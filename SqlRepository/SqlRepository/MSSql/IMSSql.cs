using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlRepository.MSSql
{
    public interface IMSSqlDb
    {
        #region Fetch Collections

        DataSet FetchDataSet(string sp, List<SqlParameter> param = null);
        DataTable FetchDataTable(string sp, List<SqlParameter> param = null);

        #endregion

        #region Fetch Scalar Values

        int FetchInt(string query, List<SqlParameter> param = null);

        string FetchString(string query, List<SqlParameter> param = null);
       
        #endregion
    }
}
