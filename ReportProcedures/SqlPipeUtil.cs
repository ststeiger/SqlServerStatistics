
using Microsoft.SqlServer.Server;


namespace ReportProcedures.SqlTools
{


    /// <summary>
    /// Holds the C# stored proc for calculating measures
    /// </summary>
    public static class SqlPipeUtil
    {


        // https://msdn.microsoft.com/en-us/library/ms127247(v=vs.110).aspx
        // https://stackoverflow.com/questions/8405320/how-do-you-handle-nullable-type-with-sqldatarecord
        // https://stackoverflow.com/questions/11091064/the-dbtype-nvarchar-is-invalid-for-this-constructor
        // https://www.simple-talk.com/sql/learn-sql-server/building-my-first-sql-server-2005-clr/
        // http://www.sqlservercentral.com/articles/SQLCLR/68850/
        // https://stackoverflow.com/questions/5336800/stored-procedure-in-sql-clr
        // http://www.sharpfellows.com/post/Returning-a-DataTable-over-SqlContextPipe
        // https://stackoverflow.com/questions/3945226/altering-results-prior-to-using-sqlcontext-pipe-send-in-a-net-sproc
        private static SqlMetaData GetMetaData(System.Data.DataColumn col)
        {
            // object obj = col.Table.Rows[0][col.ColumnName];
            // SqlMetaData met = SqlMetaData.InferFromValue(obj, col.ColumnName);
            // System.Console.WriteLine(met);


            System.Data.SqlDbType foo = TypeConverter.ToSqlDbType(col.DataType);


            if (foo == System.Data.SqlDbType.UniqueIdentifier)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.UniqueIdentifier, 16, 0, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.TinyInt)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.TinyInt, 1, 3, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.SmallInt)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.SmallInt, 2, 5, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.Int)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.Int, 4, 10, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.BigInt)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.BigInt, 8, 19, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.Decimal)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.Bit, 9, 18, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.Real)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.Real, 4, 24, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.Float)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.Float, 8, 53, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.SmallMoney)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.SmallMoney, 4, 10, 4, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.Money)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.Money, 8, 19, 4, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.Bit)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.Bit, 1, 1, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.Timestamp)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.Timestamp, 8, 0, 0, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.SmallDateTime)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.SmallDateTime, 4, 10, 4, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            if (foo == System.Data.SqlDbType.DateTime)
                return new SqlMetaData(col.ColumnName, System.Data.SqlDbType.DateTime, 8, 23, 3, 0, System.Data.SqlTypes.SqlCompareOptions.None, null);

            return new SqlMetaData(col.ColumnName, foo, col.MaxLength);
        } // End Function GetMetaData 


        /// <summary>
        /// Send some data over the SqlPipe
        /// </summary>
        /// <param name="tbl">The data to send</param>
        public static void SendDataTableOverPipe(System.Data.DataTable tbl)
        {
            // Build our record schema
            System.Collections.Generic.List<SqlMetaData> OutputColumns = 
                new System.Collections.Generic.List<SqlMetaData>(tbl.Columns.Count);

            foreach (System.Data.DataColumn col in tbl.Columns)
            {
                // SqlMetaData OutputColumn = new SqlMetaData(col.ColumnName, TypeConverter.ToSqlDbType(col.DataType), col.MaxLength);
                SqlMetaData OutputColumn = GetMetaData(col);

                OutputColumns.Add(OutputColumn);
            } // Next col

            // Build our SqlDataRecord and start the results
            SqlDataRecord record = new SqlDataRecord(OutputColumns.ToArray());
            SqlContext.Pipe.SendResultsStart(record);

            // Now send all the rows
            foreach (System.Data.DataRow row in tbl.Rows)
            {
                for (int col = 0; col < tbl.Columns.Count; col++)
                {
                    record.SetValue(col, row.ItemArray[col]);
                } // Next col

                SqlContext.Pipe.SendResultsRow(record);
            } // Next row 

            // And complete the results
            SqlContext.Pipe.SendResultsEnd();
        } // End Sub SendDataTableOverPipe 


    } // End Class SqlPipeUtil


} // End Namespace ReportProcedures.SqlTools
