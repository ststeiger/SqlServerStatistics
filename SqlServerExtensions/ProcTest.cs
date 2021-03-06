﻿
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;


using System.Data.SqlClient;


namespace SqlServerExtensions
{


    class ProcTest
    {

        // https://msdn.microsoft.com/en-us/library/ms131094.aspx
        // https://msdn.microsoft.com/en-us/library/5czye81z(v=vs.90).aspx
        [Microsoft.SqlServer.Server.SqlProcedure]
        private static void PriceSum(out SqlInt32 value)
        {
            using (System.Data.Common.DbConnection connection = new SqlConnection("context connection=true"))
            {
                value = 0;
                connection.Open();
                using (System.Data.Common.DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Price FROM Products";

                    System.Data.Common.DbDataReader reader = command.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            value += reader.GetInt32(0);
                        } // Whend 

                    } // End using reader

                } // End Using command 

            } // End Using connection 

        } // End Sub PriceSum 


        [Microsoft.SqlServer.Server.SqlProcedure]
        private static void HelloWorld()
        {
            SqlContext.Pipe.Send("Hello world! It's now " + System.DateTime.Now.ToString() + "\n");

            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                if(connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand command = new SqlCommand("SELECT ProductNumber FROM ProductMaster", connection);
                SqlDataReader reader = command.ExecuteReader();
                SqlContext.Pipe.Send(reader);
            } // End Using connection 

        } // End Sub HelloWorld 
        

    } // End Class 


} // End Namespace 
