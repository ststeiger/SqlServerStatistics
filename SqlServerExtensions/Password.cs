
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;


namespace SqlServerExtensions
{


    public class Password
    {

        //[SqlFunction]
        //public static SqlDouble Sin(SqlDouble x)
        //{
        //    return new SqlDouble(123);
        //}


        [SqlFunction]
        public static SqlString Encrypt(SqlString x)
        {
            string cryptText = Cryptography.DES.Crypt(x.Value);

            return new SqlString(cryptText);
        } // End Function Encrypt 


        [SqlFunction]
        public static SqlString Decrypt(SqlString x)
        {
            string plainText = Cryptography.DES.DeCrypt(x.Value);

            return new SqlString(plainText);
        } // End Function Decrypt 


    } // End Class Password 


} // End Namespace SqlServerExtensions 
