
namespace ReportProcedures
{


    internal static class AnySqlHelper
    {


        internal static string GetConnectionString()
        {
            Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();

            csb.Host = "127.0.0.1";
            csb.Port = 5432;
            csb.Database = "testdb";
            // csb.Database = "blogz";
            // csb.Database = "gogs";



            // CREATE ROLE ssrswebservices PASSWORD 'foobar' SUPERUSER CREATEDB CREATEROLE INHERIT LOGIN;
            csb.UserName = "ssrswebservices";
            csb.Password = "foobar";

            return csb.ConnectionString;
        }


        internal static string GetMsConnectionString()
        {
            bool m_Debug = true;

            if (m_Debug)
            {
                System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
                csb.InitialCatalog = "TestDB";
                csb.DataSource = System.Environment.MachineName;
                csb.IntegratedSecurity = true;

                return csb.ConnectionString;
            }

            return "context connection=true";
        }


        internal static System.Data.Common.DbProviderFactory GetFactory(System.Type type)
        {
            if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
            {
                // Provider factories are singletons with Instance field having
                // the sole instance
                System.Reflection.FieldInfo field = type.GetField("Instance"
                    , System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static
                );

                if (field != null)
                {
                    return (System.Data.Common.DbProviderFactory)field.GetValue(null);
                    //return field.GetValue(null) as DbProviderFactory;
                } // End if (field != null)

            } // End if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))

            throw new System.Exception("DataProvider is missing!");
            //throw new System.Configuration.ConfigurationException("DataProvider is missing!");
        } // End Function GetFactory


    } // End class AnySqlHelper


} // End Namespace ReportProcedures
