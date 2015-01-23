
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;


namespace SqlServerStatistics
{


	public class Combinatorics
	{


		public Combinatorics()
		{
		}


		public static SqlDouble Variations(SqlInt32 n, SqlInt32 k)
		{
			return new SqlDouble( MathNet.Numerics.Combinatorics.Variations (n.Value, k.Value));
		}


		public static SqlDouble VariationsWithRepetition(SqlInt32 n, SqlInt32 k)
		{
			return new SqlDouble( MathNet.Numerics.Combinatorics.VariationsWithRepetition (n.Value, k.Value));
		}


		public static SqlDouble Combinations(SqlInt32 n, SqlInt32 k)
		{
			return new SqlDouble( MathNet.Numerics.Combinatorics.Combinations (n.Value, k.Value));
		}


		public static SqlDouble CombinationsWithRepetition(SqlInt32 n, SqlInt32 k)
		{
			return new SqlDouble( MathNet.Numerics.Combinatorics.CombinationsWithRepetition (n.Value, k.Value));
		}


		public static SqlDouble Permutations(SqlInt32 n)
		{
			return new SqlDouble( MathNet.Numerics.Combinatorics.Permutations (n.Value));
		}


	}


}
