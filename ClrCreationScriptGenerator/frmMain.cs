
using System.Windows.Forms;


namespace ClrCreationScriptGenerator
{


    public partial class frmMain : Form
    {


        public frmMain()
        {
            InitializeComponent();
        } // End Constructor
        

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            this.txtScript.WordWrap = false;
            bool bLocal = false;
            bool warnCRL = false;
            
            
            // string assemblyName = "SQLServerStatistics";
            string assemblyName = "SqlServerStatistics";
            // string assemblyName = "HelperFunctions";
            // assemblyName = "SqlServerExtensions";

            // System.Type tTypeToExport = typeof(HelperFunctions.StringHelper);
            // System.Type tTypeToExport = typeof(SqlServerStatistics.ExcelFunctions);
            System.Type tTypeToExport = typeof(SqlServerExtensions.Password);

            System.Reflection.Assembly ass = tTypeToExport.Assembly;
            assemblyName = ass.GetName().Name;
            System.Console.WriteLine(assemblyName);

            byte[] bytes = System.IO.File.ReadAllBytes(ass.Location);
            string strHexFile = bLocal ? "'" + ass.Location.Replace("'", "''") + "'" : 
                "0x" + System.BitConverter.ToString(bytes).Replace("-", "");

            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if(warnCRL)
                sb.AppendLine(@"

-- https://software.intel.com/en-us/blogs/2009/10/16/sql-server-2008-sqlclr-net-framework-version 

-- SQL-Server 2005: CLR 2.0 
-- SQL-Server 2008 R1: CLR 2.0 
-- SQL-Server 2008 R2: CLR 2.0 
-- SQL-Server 2012: CLR 4.0 
-- SQL-Server 2014: CLR 4.0 


-- i.e. it would break backwards compatibility 
-- because the SQL Server Engine can only host 
-- one version of the CLR. 


-- http://www.sqlskills.com/blogs/bobb/on-sql-server-and-net-4-0/

-- SQL-Server 2008: 
-- Specifying 4.0 in a configuration file will force SQL Server 
-- to load the 4.0 version, but then attempting to 
-- create and initialize a variable of type ""geography"" 
-- fails with the message ""Method's type signature is 
-- not interop compatible"". 

-- http://stackoverflow.com/questions/2781624/sqlclr-using-the-wrong-version-of-the-net-framework 
-- supportedRuntime tag in sqlservr.exe.config in the \Binn folder 





-- SELECT 
--	 name 
--	,compatibility_level 
--	,
--	CASE compatibility_level 
--		WHEN 65  THEN 'SQL Server 6.5' 
--		WHEN 70  THEN 'SQL Server 7.0' 
--		WHEN 80  THEN 'SQL Server 2000' 
--		WHEN 90  THEN 'SQL Server 2005' 
--		WHEN 100 THEN 'SQL Server 2008/R2' 
--		WHEN 110 THEN 'SQL Server 2012' 
--		WHEN 120 THEN 'SQL Server 2014' 
--	END AS version_name 
-- FROM sys.databases 


SELECT compatibility_level FROM sys.databases WHERE name = DB_NAME(); 

-- -- http://stackoverflow.com/questions/1501596/how-to-check-sql-server-database-compatibility-after-sp-dbcmptlevel-is-deprecate 
-- -- sp_dbcmptlevel 
-- -- ALTER DATABASE AdventureWorks2012 SET COMPATIBILITY_LEVEL = 90; 
-- -- DECLARE @sql varchar(4000) 
-- -- SET @sql = N'ALTER DATABASE ' + QUOTENAME(DB_NAME()) + N' SET COMPATIBILITY_LEVEL = 90;' 
-- -- PRINT @sql 
-- -- EXECUTE(@sql) 


-- -- Determine the current CLR version 
SELECT * FROM sys.dm_clr_properties 


-- -- information regarding the application domains 
-- SELECT * FROM sys.dm_clr_appdomains 
-- -- loaded assemblies 
-- SELECT * FROM sys.dm_clr_loaded_assemblies 
-- -- and tasks 
-- SELECT * FROM sys.dm_clr_tasks 




");


            sb.AppendLine(@"
sp_configure 'show advanced options', 1; 
GO
RECONFIGURE; 
GO
sp_configure 'clr enabled', 1; 
GO
RECONFIGURE; 
GO


DECLARE @sql nvarchar(MAX) 
SET @sql = 'ALTER DATABASE ' + QUOTENAME(DB_NAME()) + ' SET TRUSTWORTHY ON;' 
-- PRINT @sql; 
EXECUTE(@sql); 
GO



-- Restore sid when db restored from backup... 
DECLARE @Command NVARCHAR(MAX) 
SET @Command = N'ALTER AUTHORIZATION ON DATABASE::<<DatabaseName>> TO <<LoginName>>' 
SELECT @Command = REPLACE 
				  ( 
					  REPLACE(@Command, N'<<DatabaseName>>', QUOTENAME(SD.Name)) 
					  , N'<<LoginName>>' 
					  ,
					  QUOTENAME
					  (
						  COALESCE
						  (
							   SL.name 
							  ,(SELECT TOP 1 name FROM sys.server_principals WHERE type_desc = 'SQL_LOGIN' AND is_disabled = 'false' ORDER BY principal_id ASC )
						  )
					  )
				  ) 
FROM sys.databases AS SD
LEFT JOIN sys.server_principals  AS SL 
	ON SL.SID = SD.owner_sid 
	
	
WHERE SD.Name = DB_NAME() 

PRINT @command 
EXECUTE(@command) 
GO


");


            // Drop Functions/Procedures
            sb.AppendFormat(@"

DECLARE @n char(2) 
DECLARE @stmt nvarchar(MAX) 
SET @n = CHAR(13) + CHAR(10) 


SET @stmt = ''
SELECT @stmt = @stmt + @n + 'DROP PROCEDURE ' + QUOTENAME(SCHEMA_NAME(syso.schema_id)) + '.' + QUOTENAME(syso.name) + ';' 
FROM sys.assemblies AS sysa 
INNER JOIN sys.assembly_modules AS sysm ON sysm.assembly_id = sysa.assembly_id 
INNER JOIN sys.objects AS syso ON syso.object_id = sysm.object_id 
WHERE (1=1) 
AND sysa.NAME = '{0}'
AND syso.type IN  (N'P', N'PC') 
ORDER BY SCHEMA_NAME(syso.schema_id), syso.name  


PRINT @stmt 
EXECUTE(@stmt) 


SET @stmt = ''
SELECT @stmt = @stmt + @n + 'DROP FUNCTION ' + QUOTENAME(SCHEMA_NAME(syso.schema_id)) + '.' + QUOTENAME(syso.name) + ';' 
FROM sys.assemblies AS sysa 
INNER JOIN sys.assembly_modules AS sysm ON sysm.assembly_id = sysa.assembly_id 
INNER JOIN sys.objects AS syso ON syso.object_id = sysm.object_id 
WHERE (1=1) 
AND sysa.NAME = '{0}' 
AND syso.type IN (N'FN', N'IF', N'TF', N'FS', N'FT') 
ORDER BY SCHEMA_NAME(syso.schema_id), syso.name  


PRINT @stmt 
EXECUTE(@stmt) 


PRINT 'DROPPING ASSEMBLY ""{0}""' 
GO 


IF  EXISTS (SELECT * FROM sys.assemblies asms WHERE asms.name = N'{0}' and is_user_defined = 1)
DROP ASSEMBLY [" + assemblyName.Replace("]", "]]") + @"]
GO


-- Without drop: ALTER ASSEMBLY ComplexNumber FROM 
CREATE ASSEMBLY [" + assemblyName.Replace("]", "]]") + @"] AUTHORIZATION [dbo] 
    FROM {1} 
WITH PERMISSION_SET = UNSAFE
GO

", assemblyName.Replace("'", "''"), strHexFile);


            WriteFunctionDefinition(assemblyName, sb, tTypeToExport);

            // http://stackoverflow.com/questions/9983378/monodevelop-convert-line-ending-dialog
            // Compensate for line endings on different platforms...
            this.txtScript.Text = sb.Replace("\r\n", "\n").Replace("\n", System.Environment.NewLine).ToString();
        } // End Sub frmMain_Load


		public void WriteFunctionDefinition(string assemblyName, System.Text.StringBuilder sb, System.Type t )
		{
			System.Reflection.Assembly ass = t.Assembly;

			System.Collections.Generic.Dictionary<System.Type, string> typeDict = initTypeDict();

			foreach (System.Type tThisType in ass.GetTypes())
			{
				if (tThisType.FullName.StartsWith ("MathNet."))
					continue;

				if (string.IsNullOrEmpty (tThisType.Namespace))
					continue;

				// System.Console.WriteLine(tThisType.Namespace);
				// System.Console.WriteLine(tThisType.FullName);
				WriteFunctionDefinition (assemblyName, sb, tThisType, typeDict);
			} // Next tThisType 

		}


		public static System.Collections.Generic.Dictionary<System.Type, string> initTypeDict()
		{
			//System.Collections.Specialized.StringDictionary sd = new System.Collections.Specialized.StringDictionary();
			System.Collections.Generic.Dictionary<System.Type, string> dict = new System.Collections.Generic.Dictionary<System.Type, string>();
			dict.Add(typeof(System.Data.SqlTypes.SqlBoolean), "bit");
			dict.Add(typeof(System.Data.SqlTypes.SqlInt16), "smallint");
			dict.Add(typeof(System.Data.SqlTypes.SqlInt32), "int");
			dict.Add(typeof(System.Data.SqlTypes.SqlInt64), "bigint");
			dict.Add(typeof(System.Data.SqlTypes.SqlSingle), "real");
			dict.Add(typeof(System.Data.SqlTypes.SqlDouble), "float");
			dict.Add(typeof(System.Data.SqlTypes.SqlMoney), "money");
			dict.Add(typeof(System.Data.SqlTypes.SqlDecimal), "decimal(19,5)");
			dict.Add(typeof(System.Data.SqlTypes.SqlString), "nvarchar(4000)");
			dict.Add(typeof(System.Data.SqlTypes.SqlGuid), "uniqueidentifier");
            dict.Add(typeof(System.Data.SqlTypes.SqlDateTime), "datetime");
            
			return dict;
		} // End Sub WriteFunctionDefinition


		public void WriteFunctionDefinition(string assemblyName, System.Text.StringBuilder sb, System.Type t
			,System.Collections.Generic.Dictionary<System.Type, string> typeDict)
		{
			System.Reflection.MethodInfo[] mis = t.GetMethods(System.Reflection.BindingFlags.Static
				| System.Reflection.BindingFlags.Public
			);


			foreach (System.Reflection.MethodInfo mi in mis)
			{
				string strRetType = "Unknown_" + mi.ReturnType.Name;

				if (typeDict.ContainsKey(mi.ReturnType))
					strRetType = typeDict[mi.ReturnType];


                string methodType = "FUNCTION";
                string typeIn = "N'FN', N'IF', N'TF', N'FS', N'FT'";
                if (object.ReferenceEquals(mi.ReturnType, typeof(void)))
                {
                    methodType = "PROCEDURE";
                    typeIn = "N'P', N'PC'";
                    // continue;
                }






                sb.AppendLine(@"         
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + mi.Name.Replace("]", "]]") + @"]') AND type in (" + typeIn + @")) 
DROP " + methodType + " [dbo].[" + mi.Name.Replace("]", "]]") + @"] 
GO


");



				sb.Append(@"
CREATE " + methodType + " [dbo].[" + mi.Name.Replace("]", "]]") + @"](");

				System.Reflection.ParameterInfo[] pis = mi.GetParameters();
				for (int i = 0; i < pis.Length; ++i)
				{
					System.Reflection.ParameterInfo pi = pis[i];

                    System.Type tReal = pi.ParameterType;

                    // WTF ?
                    // http://www.blackwasp.co.uk/ReflectOutRefParams.aspx
                    // http://stackoverflow.com/questions/738277/net-reflection-how-to-get-real-type-from-out-parameterinfo
                    if (pi.IsOut || pi.ParameterType.IsByRef)
                        tReal = tReal.GetElementType();

					string strParType = "Unknown_" + pi.ParameterType.Name;

                    if (typeDict.ContainsKey(tReal))
                    {
                        if (pi.IsOut || pi.ParameterType.IsByRef)
                            strParType = typeDict[tReal] + " output";
                        else
                            strParType = typeDict[tReal];
                    }
                        

					if (i != 0)
						sb.Append(", ");

                    sb.Append("@" + pi.Name + " AS " + strParType);
				} // Next i 

				sb.Append(@") ");


                if (!object.ReferenceEquals(mi.ReturnType, typeof(void)))
                    sb.Append(@"
RETURNS " + strRetType + @" WITH EXECUTE AS CALLER ");

                sb.AppendLine(@"
AS EXTERNAL NAME [" + assemblyName.Replace("]", "]]") + @"].[" + t.FullName.Replace("]", "]]") + @"].[" + mi.Name.Replace("]", "]]") + @"] 
GO

");

			} // Next mi 

		} // End Sub WriteFunctionDefinition


        private void txtScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.A)
                this.txtScript.SelectAll();
        } // End Sub txtScript_KeyDown


    } // End Class frmMain : Form


} // End Namespace ClrCreationScriptGenerator
