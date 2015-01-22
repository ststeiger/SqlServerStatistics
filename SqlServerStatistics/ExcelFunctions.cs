
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;


namespace SqlServerStatistics
{


    // SqlServerStatistics.ExcelFunctions
    public class ExcelFunctions
    {


        // MathNet.Numerics.ExcelFunctions.

		[SqlFunction]
		public static SqlDouble BetaDist(SqlDouble x, SqlDouble alpha, SqlDouble beta)
		{
			return MathNet.Numerics.ExcelFunctions.BetaDist(x.Value, alpha.Value, beta.Value);
		}


		[SqlFunction]
		public static SqlDouble BetaInv(SqlDouble probability, SqlDouble alpha, SqlDouble beta)
		{
			return MathNet.Numerics.ExcelFunctions.BetaInv(probability.Value, alpha.Value, beta.Value);
		}


		[SqlFunction]
		public static SqlDouble FDist(SqlDouble x, SqlInt32 degreesFreedom1, SqlInt32 degreesFreedom2)
		{
			return MathNet.Numerics.ExcelFunctions.FDist(x.Value, degreesFreedom1.Value, degreesFreedom2.Value);
		}


		[SqlFunction]
		public static SqlDouble FInv(SqlDouble probability, SqlInt32 degreesFreedom1, SqlInt32 degreesFreedom2)
		{
			return MathNet.Numerics.ExcelFunctions.FInv(probability.Value, degreesFreedom1.Value, degreesFreedom2.Value);
		}


		[SqlFunction]
		public static SqlDouble GammaDist(SqlDouble x, SqlDouble alpha, SqlDouble beta, SqlBoolean cumulative)
		{
			return MathNet.Numerics.ExcelFunctions.GammaDist(x.Value, alpha.Value, beta.Value, cumulative.Value);
		}


		[SqlFunction]
		public static SqlDouble GammaInv(SqlDouble probability, SqlDouble alpha, SqlDouble beta)
		{
			return MathNet.Numerics.ExcelFunctions.GammaInv(probability.Value, alpha.Value, beta.Value);
		}


		[SqlFunction]
		public static SqlDouble NormDist(SqlDouble x, SqlDouble mean, SqlDouble standardDev, SqlBoolean cumulative)
		{
			return MathNet.Numerics.ExcelFunctions.NormDist(x.Value, mean.Value, standardDev.Value, cumulative.Value);
		}


		[SqlFunction]
		public static SqlDouble NormInv(SqlDouble probability, SqlDouble mean, SqlDouble standardDev)
		{
			return MathNet.Numerics.ExcelFunctions.NormInv(probability.Value, mean.Value,standardDev.Value);
		}


		[SqlFunction]
		public static SqlDouble NormSDist(SqlDouble z)
		{
			return MathNet.Numerics.ExcelFunctions.NormSDist(z.Value);
		}


		[SqlFunction]
		public static SqlDouble NormSInv(SqlDouble probability)
		{
			return MathNet.Numerics.ExcelFunctions.NormSInv(probability.Value);
		}


		[SqlFunction]
		public static SqlDouble TDist(SqlDouble x, SqlInt32 degFreedom, SqlInt32 tails)
		{
			SqlDouble result = 0.00;
			try
			{       
				result = MathNet.Numerics.ExcelFunctions.TDist(x.Value, degFreedom.Value, tails.Value);
			}
			catch
			{
				// throw; // Optionally throw/log/ignore/whatever
			}
			return result;
		} // End Function TDist


		[SqlFunction]
		public static SqlDouble TInv(SqlDouble probability, SqlInt32 degFreedom)
		{
			SqlDouble result = 0.00;
			try
			{
				result = MathNet.Numerics.ExcelFunctions.TInv(probability.Value, degFreedom.Value);
			}
			catch
			{
				// throw; // Optionally throw/log/ignore/whatever
			}
			return result;
		} // End Function TInv


    } // End Class ExcelFunctions


} // End Namespace SqlServerStatistics
