
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;


namespace SqlServerStatistics
{


	// http://numerics.mathdotnet.com/docs/Functions.html
	public class SpecialFunctions
	{
		public SpecialFunctions()
		{ }


		[SqlFunction]
		public static SqlDouble Beta(SqlDouble z, SqlDouble w)
		{
			return MathNet.Numerics.SpecialFunctions.Beta(z.Value, w.Value);
		}


		[SqlFunction]
		public static SqlDouble BinomialCoefficient(SqlInt32 n, SqlInt32 k)
		{
			return MathNet.Numerics.SpecialFunctions.Binomial(n.Value, k.Value);
		}


		[SqlFunction]
		public static SqlDouble DiGamma(SqlDouble x)
		{
			return MathNet.Numerics.SpecialFunctions.DiGamma(x.Value);
		}


		[SqlFunction]
		public static SqlDouble ErrorFunction(SqlDouble x)
		{
			return MathNet.Numerics.SpecialFunctions.Erf(x.Value);
		}


		[SqlFunction]
		public static SqlDouble ErrorFunctionInverse(SqlDouble x)
		{
			return MathNet.Numerics.SpecialFunctions.ErfInv(x.Value);
		}


		[SqlFunction]
		public static SqlDouble ErrorFunctionComplementary(SqlDouble x)
		{
			return MathNet.Numerics.SpecialFunctions.Erfc(x.Value);
		}


		[SqlFunction]
		public static SqlDouble ErrorFunctionComplementaryInverse(SqlDouble x)
		{
			return MathNet.Numerics.SpecialFunctions.ErfcInv(x.Value);
		}


		[SqlFunction]
		public static SqlDouble Factorial(SqlInt32 x)
		{
			return MathNet.Numerics.SpecialFunctions.Factorial(x.Value);
		}


		[SqlFunction]
		public static SqlDouble Gamma(SqlDouble z)
		{
			return MathNet.Numerics.SpecialFunctions.Gamma(z.Value);
		}


	} // End Using Class SpecialFunctions 


} // End Using Namespace SqlServerStatistics 
