using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PLDS_Data.Databases
{
    public class MySqlDb
    {
        public MySqlDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;

        #region Fetch Collections

        public DataSet FetchDataSet(string query)
        {
            DataSet ds = new DataSet();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(connection: conn, selectCommandText: query);

                    MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);

                    adapter.Fill(ds);
                }

                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataTable FetchDataTable(string sp, List<MySqlParameter> param = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_connectionString))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter()
                    {
                        SelectCommand = CreateCommand(sp, con, param)
                    };

                    da.Fill(dt);
                }

                return dt;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Fetch Scalar Values

        public int FetchInt(string query, List<MySqlParameter> param = null)
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                MySqlCommand comm = CreateCommand(query, con, param);

                return Convert.ToInt32(comm.ExecuteScalar());
            }
        }

        public double FetchDouble(string query)
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                MySqlCommand comm = new MySqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query
                };

                con.Open();

                return Convert.ToDouble(comm.ExecuteScalar());
            }
        }

        public string FetchString(string sp, List<MySqlParameter> param = null)
        {
            try
            {
                using (var con = new MySqlConnection(_connectionString))
                {
                    MySqlCommand cmd = CreateCommand(sp, con, param);

                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool FetchBoolean(string query, List<MySqlParameter> param = null)
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                MySqlCommand comm = CreateCommand(query, con, param);

                return Convert.ToBoolean(comm.ExecuteScalar());
            }
        }

        #endregion

        #region Execute NonQuery

        public void ExecuteNonQuery(string sp, List<MySqlParameter> param = null)
        {
            try
            {
                using (var con = new MySqlConnection(_connectionString))
                {
                    MySqlCommand cmd = CreateCommand(sp, con, param);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        private MySqlCommand CreateCommand(string sp, MySqlConnection conn, List<MySqlParameter> param = null)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text
                };

                StringBuilder strParam = new StringBuilder();

                if (param == null)
                {
                    cmd.CommandText = sp;
                }
                else
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        strParam.Append(param[i].ParameterName.ToString());
                        if (i + 1 < param.Count)
                        { strParam.Append(","); }

                        cmd.Parameters.Add(param[i]);
                    }
                    cmd.CommandText = sp + "(" + strParam + ")";
                }

                conn.Open();

                return cmd;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
