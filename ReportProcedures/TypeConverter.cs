
namespace ReportProcedures.SqlTools
{


    /// <summary>
    /// Convert a base data type to another base data type
    /// </summary>
    /// <remarks>
    /// Based on code from http://dotnetpulse.blogspot.com/2006/04/convert-net-type-to-sqldbtype-or.html
    /// </remarks>
    public sealed class TypeConverter
    {


        private struct DbTypeMapEntry
        {
            public System.Type Type;
            public System.Data.DbType DbType;
            public System.Data.SqlDbType SqlDbType;

            public DbTypeMapEntry(System.Type type, System.Data.DbType dbType, System.Data.SqlDbType sqlDbType)
            {
                this.Type = type;
                this.DbType = dbType;
                this.SqlDbType = sqlDbType;
            }

        };


        private readonly static System.Collections.Generic.List<DbTypeMapEntry> _DbTypeList = new System.Collections.Generic.List<DbTypeMapEntry>();


        static TypeConverter()
        {
            DbTypeMapEntry dbTypeMapEntry = new DbTypeMapEntry(typeof(bool), System.Data.DbType.Boolean, System.Data.SqlDbType.Bit);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(byte), System.Data.DbType.Double, System.Data.SqlDbType.TinyInt);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(byte[]), System.Data.DbType.Binary, System.Data.SqlDbType.Image);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(System.DateTime), System.Data.DbType.DateTime, System.Data.SqlDbType.DateTime);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(System.Decimal), System.Data.DbType.Decimal, System.Data.SqlDbType.Decimal);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(double), System.Data.DbType.Double, System.Data.SqlDbType.Float);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(System.Guid), System.Data.DbType.Guid, System.Data.SqlDbType.UniqueIdentifier);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(System.Int16), System.Data.DbType.Int16, System.Data.SqlDbType.SmallInt);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(System.Int32), System.Data.DbType.Int32, System.Data.SqlDbType.Int);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(System.Int64), System.Data.DbType.Int64, System.Data.SqlDbType.BigInt);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(object), System.Data.DbType.Object, System.Data.SqlDbType.Variant);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(string), System.Data.DbType.String, System.Data.SqlDbType.VarChar);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(string), System.Data.DbType.String, System.Data.SqlDbType.NVarChar);
            _DbTypeList.Add(dbTypeMapEntry);
        } // End Constructor 


        private TypeConverter()
        { } // End Constructor 

        



        /// <summary>
        /// Convert db type to .Net data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static System.Type ToNetType(System.Data.DbType dbType)
        {
            DbTypeMapEntry entry = Find(dbType);
            return entry.Type;
        }


        /// <summary>
        /// Convert TSQL type to .Net data type
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static System.Type ToNetType(System.Data.SqlDbType sqlDbType)
        {
            DbTypeMapEntry entry = Find(sqlDbType);
            return entry.Type;
        }


        /// <summary>
        /// Convert .Net type to Db type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static System.Data.DbType ToDbType(System.Type type)
        {
            DbTypeMapEntry entry = Find(type);
            return entry.DbType;
        }


        /// <summary>
        /// Convert TSQL data type to DbType
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static System.Data.DbType ToDbType(System.Data.SqlDbType sqlDbType)
        {
            DbTypeMapEntry entry = Find(sqlDbType);
            return entry.DbType;
        }


        /// <summary>
        /// Convert .Net type to TSQL data type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static System.Data.SqlDbType ToSqlDbType(System.Type type)
        {
            DbTypeMapEntry entry = Find(type);
            return entry.SqlDbType;
        }


        /// <summary>
        /// Convert DbType type to TSQL data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static System.Data.SqlDbType ToSqlDbType(System.Data.DbType dbType)
        {
            DbTypeMapEntry entry = Find(dbType);
            return entry.SqlDbType;
        }


        private static DbTypeMapEntry Find(System.Type type)
        {
            // FOO! Doesn't work 
            if (object.ReferenceEquals(type, typeof(NpgsqlTypes.BitString))) // isdirectory bit(1),
                return new DbTypeMapEntry(typeof(string), System.Data.DbType.String, System.Data.SqlDbType.Bit);
            
            foreach (DbTypeMapEntry entry in _DbTypeList)
            {
                if (entry.Type == type)
                {
                    return entry;
                }
            }

            throw new System.ApplicationException("Referenced an unsupported Type");
        }


        private static DbTypeMapEntry Find(System.Data.DbType dbType)
        {
            foreach (DbTypeMapEntry entry in _DbTypeList)
            {
                if (entry.DbType == dbType)
                {
                    return entry;
                }
            }

            throw new System.ApplicationException("Referenced an unsupported DbType");
        }


        private static DbTypeMapEntry Find(System.Data.SqlDbType sqlDbType)
        {
            foreach (DbTypeMapEntry entry in _DbTypeList)
            {
                

                if ((int) entry.SqlDbType == (int)sqlDbType)
                {
                    return entry;
                }
            }

            throw new System.ApplicationException("Referenced an unsupported SqlDbType");
        }


    } // End Class TypeConverter


} // End Namespace ReportProcedures.SqlTools
