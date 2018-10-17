using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyNameSpace
{
    public class CallingStoreProcedure
    {
        /// <summary>
        /// This method doesn't return anythig and just receive simple params
        /// </summary>
        /// <param name="connectionString">Contains the connection string</param>
        /// <param name="param1">Contains the first param</param>
        /// <param name="param2">Contains the second param</param>
        /// <param name="param3">Contains the third param</param>
        public void CallSP(string connectionString, int param1, string param2, DateTime param3)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("dbo.myStoreProcedure1", cnn);
                    command.CommandType = CommandType.StoredProcedure;

                    //adding parameters
                    command.Parameters.Add(new SqlParameter("@paramName1", param1));
                    command.Parameters.Add(new SqlParameter("@paramName2", param2));
                    command.Parameters.Add(new SqlParameter("@paramName3", param3));
                    //Executes the store procedure
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method call a store procedure passing a datatable as parameter and returns another datatable
        /// </summary>
        /// <param name="connectionString">Contains the connection string</param>
        /// <param name="param1">Contains the first param</param>
        /// <returns>Contains the result of called store procedure</returns>
        public DataTable CallSP(string connectionString, int param1, DataTable param2)
        {
            try
            {
                DataTable result = null;
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("dbo.myStoreProcedure", cnn);
                    command.CommandType = CommandType.StoredProcedure;

                    //adding table parameter
                    var firstParam = new SqlParameter("@paramName1", param1);
                    var secondParam = new SqlParameter("@paramName1", param2);
                    //Specify the parameter as struct datatype
                    secondParam.SqlDbType = SqlDbType.Structured;
                    //Specify the datatype name in the database
                    secondParam.TypeName = "dbo.types_myDataType";
                    command.Parameters.Add(secondParam);

                    //Executes the store procedure
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(result);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// In this method you can view how build the datatable to pass to an store procedure
        /// </summary>
        /// <param name="connectionString">Contains the connection string</param>
        /// <returns>Contains the result of called store procedure</returns>
        public DataTable CallSP(string connectionString)
        {
            try
            {
                DataTable result = null;

                //Define the datatable with the same names as the sqlServer datatype
                var dataTable = new DataTable();
                dataTable.Columns.Add("field1", typeof(Int32));
                dataTable.Columns.Add("field2", typeof(String));

                result = CallSP(connectionString, 0, dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}