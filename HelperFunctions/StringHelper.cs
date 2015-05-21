
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;


namespace HelperFunctions
{


    public class StringHelper
    {


        [SqlFunction]
        public static SqlString StripInvalidFileNameChars(SqlString s)
        {
            return new SqlString(StripInvalidFileNameChars_implements(s.Value));
        }


        [SqlFunction]
        public static SqlString StripInvalidPathChars(SqlString s)
        {
            return new SqlString(StripInvalidPathChars_implements(s.Value));
        }


        [SqlFunction]
        public static SqlString StripInvalidFileNameAndPathChars(SqlString s)
        {
            return new SqlString(StripInvalidFileNameAndPathChars_implements(s.Value));
        }

        

        // COR.ASP.NET.StripInvalidFileNameChars("")
        private static string StripInvalidFileNameChars_implements(string str)
        {
            string strReturnValue = null;

            if (str == null)
                return strReturnValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            char[] achrInvalid = System.IO.Path.GetInvalidFileNameChars();
            foreach (char cThisChar in str)
            {
                bool bIsValid = true;

                foreach (char cInvalid in achrInvalid)
                {
                    if (cThisChar == cInvalid)
                    {
                        bIsValid = false;
                        break; // TODO: might not be correct. Was : Exit For
                    } // End if (cThisChar == cInvalid) 

                } // Next cInvalid 

                if (bIsValid)
                    sb.Append(cThisChar);
            } // Next cThisChar 

            strReturnValue = sb.ToString();
            sb = null;
            return strReturnValue;
        } // End Function StripInvalidFileNameChars


        // COR.ASP.NET.StripInvalidPathChars("")
        private static string StripInvalidPathChars_implements(string str)
        {
            string strReturnValue = null;

            if (str == null)
                return strReturnValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            char[] achrInvalidPathChars = System.IO.Path.GetInvalidPathChars();

            foreach (char cThisChar in str)
            {
                bool bIsValid = true;

                foreach (char cInvalid in achrInvalidPathChars)
                {
                    if (cThisChar == cInvalid)
                    {
                        bIsValid = false;
                        break; // TODO: might not be correct. Was : Exit For
                    }  // End if (cThisChar == cInvalid)

                } // Next cInvalid 

                if (bIsValid)
                    sb.Append(cThisChar);
            } // Next cThisChar 

            strReturnValue = sb.ToString();
            sb = null;
            return strReturnValue;
        } // End Function StripInvalidPathChars


        // COR.ASP.NET.StripInvalidFileNameAndPathChars("")
        private static string StripInvalidFileNameAndPathChars_implements(string str)
        {
            string strReturnValue = null;
            strReturnValue = StripInvalidFileNameChars_implements(str);
            strReturnValue = StripInvalidPathChars_implements(strReturnValue);

            if (strReturnValue != null)
                strReturnValue = strReturnValue.Trim();

            // If filename consists entirely out of invalid path chars || filename = string.empty
            if (string.IsNullOrEmpty(strReturnValue))
            {
                string extension = System.IO.Path.GetExtension(str);
                strReturnValue = "Download" + extension;
            } // End if (string.IsNullOrEmpty(strReturnValue)) 

            return strReturnValue;
        } // End Function StripInvalidFileNameAndPathChars


    }


}
