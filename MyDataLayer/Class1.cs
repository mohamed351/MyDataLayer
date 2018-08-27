
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;


namespace MyDataLayer
{
    public class MyData : IDisposable
    {
        /// <summary>
        /// this is to get the ConnectionString 
        /// </summary>
        readonly string ConnectionServer;

        /// <summary>
        /// Database Conncetion this connection 
        /// </summary>
        /// <param name="Connection">Connection String </param>
        public MyData(string Connection)
        {
            ConnectionServer = Connection;
        }




        /// <summary>
        /// This is method Deal with Database 
        /// </summary>
        /// <param name="Command">SQL Statment</param>
        /// <param name="MyStatment">Choose Statment</param>
        /// <returns></returns>
        public object MyMethod(string Command, TypeStatment MyStatment)
        {
            using (SqlConnection connect = new SqlConnection(ConnectionServer))
            {

                connect.Open();
                SqlCommand cmd = new SqlCommand(Command, connect);
                cmd.CommandType = CommandType.Text;

                switch (MyStatment)
                {
                    case TypeStatment.Select:
                        SqlDataReader read = cmd.ExecuteReader();
                        DataTable table = new DataTable();
                        table.Load(read);
                        return table;

                    case TypeStatment.UpdateOrDeleteOrInsert:
                        int RowNumbers = cmd.ExecuteNonQuery();
                        return RowNumbers;

                    case TypeStatment.AggregateFunction:
                        return cmd.ExecuteScalar();

                    default:
                        return "Unknow Value";

                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="MyStatment"></param>
        /// <returns></returns>
        public async Task<object> MyMethodAsync(string Command, TypeStatment MyStatment)
        {
            using (SqlConnection connect = new SqlConnection(ConnectionServer))
            {

                await connect.OpenAsync();
                SqlCommand cmd = new SqlCommand(Command, connect);
                cmd.CommandType = CommandType.Text;

                switch (MyStatment)
                {
                    case TypeStatment.Select:
                        SqlDataReader read = await cmd.ExecuteReaderAsync();
                        DataTable table = new DataTable();
                        table.Load(read);
                        return table;

                    case TypeStatment.UpdateOrDeleteOrInsert:
                        int RowNumbers = await cmd.ExecuteNonQueryAsync();
                        return RowNumbers;

                    case TypeStatment.AggregateFunction:
                        return await cmd.ExecuteScalarAsync();

                    default:
                        return "Unknow Value";

                }
            }


        }









        /// <summary>
        /// This is method Deals with Database 
        /// </summary>
        /// <param name="storedProcedure">Stord Procedure</param>
        /// <param name="Par">Sql Parameter </param>
        /// <param name="MyStatment">Type of Statment</param>
        /// <returns></returns>
        public object MyMethod(string storedProcedure, SqlParameter[] Par, TypeStatment MyStatment)
        {
            using (SqlConnection connect = new SqlConnection(ConnectionServer))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(storedProcedure, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                if (Par != null)
                {
                    for (int i = 0; i < Par.Length; i++)
                    {
                        cmd.Parameters.Add(Par[i]);

                    }
                }
                switch (MyStatment)
                {
                    case TypeStatment.Select:
                        SqlDataReader read = cmd.ExecuteReader();
                        DataTable table = new DataTable();
                        table.Load(read);
                        return table;

                    case TypeStatment.UpdateOrDeleteOrInsert:
                        int Rows = cmd.ExecuteNonQuery();
                        return Rows;

                    case TypeStatment.AggregateFunction:
                        return cmd.ExecuteScalar();

                    default:
                        return "Unknown";



                }





            }



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="Par"></param>
        /// <param name="MyStatment"></param>
        /// <returns></returns>
        public async Task<object> MyMethodAsync(string storedProcedure, SqlParameter[] Par, TypeStatment MyStatment)
        {
            using (SqlConnection connect = new SqlConnection(ConnectionServer))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(storedProcedure, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                if (Par != null)
                {
                    for (int i = 0; i < Par.Length; i++)
                    {
                        cmd.Parameters.Add(Par[i]);

                    }
                }
                switch (MyStatment)
                {
                    case TypeStatment.Select:
                        SqlDataReader read = await cmd.ExecuteReaderAsync();
                        DataTable table = new DataTable();
                        table.Load(read);
                        return table;

                    case TypeStatment.UpdateOrDeleteOrInsert:
                        int Rows = await cmd.ExecuteNonQueryAsync();
                        return Rows;

                    case TypeStatment.AggregateFunction:
                        return await cmd.ExecuteScalarAsync();

                    default:
                        return "Unknown";



                }


                
            }
  }
        /// <summary>
        /// GC
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
        }




        /// <summary>
        /// this is enum made to choose if the stored procedure or statement is Select or Update or Insert or Delete
        /// </summary>
        public enum TypeStatment
        {
            /// <summary>
            /// Select Data
            /// </summary>
            Select,
            /// <summary>
            /// Update or Delete or Insert
            /// </summary>
            UpdateOrDeleteOrInsert,
            /// <summary>
            /// one Value
            /// </summary>
            AggregateFunction
        }

    }
}


