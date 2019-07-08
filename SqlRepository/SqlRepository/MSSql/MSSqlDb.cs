using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlRepository.MSSql
{
    public class MSSqlDb:IMSSqlDb
    {
        private readonly string _connectionString;

        public MSSqlDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Fetch Collections

        public DataSet FetchDataSet(string sp, List<SqlParameter> param = null)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(sp, conn)
                };
                adapter.Fill(ds);
            }

            return ds;
        }

        public DataTable FetchDataTable(string sp, List<SqlParameter> param = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter
                {

                    SelectCommand = new SqlCommand(sp, conn),
                };

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {

                    foreach (var item in param)
                    {
                        adapter.SelectCommand.Parameters.Add(item);
                    };
                }

                adapter.Fill(dt);
            }

            return dt;
        }

        #endregion

        #region Fetch Scalar Values

        public int FetchInt(string query, List<SqlParameter> param = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand comm = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = query
                };

                if (param != null)
                {
                    foreach (var item in param)
                    {
                        comm.Parameters.Add(item);
                    }
                }

                return Convert.ToInt32(comm.ExecuteScalar());
            }
        }

        public string FetchString(string query, List<SqlParameter> param = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand comm = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = query
                };


                if (param != null)
                {
                    foreach (var item in param)
                    {
                        comm.Parameters.Add(item);
                    }
                }

                return comm.ExecuteScalar().ToString();
            }
        }




        #endregion
    }
}
