
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;


namespace SqlServerStatistics
{
	public class Trigonometry
	{
		public Trigonometry()
		{ }


		[SqlFunction]
		public static SqlDouble Sin(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Sin (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Sinc(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Sinc (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Cos(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Cos (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Tan(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Tan (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Cot(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Cot (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Sec(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Sec (x.Value));
		}



		[SqlFunction]
		public static SqlDouble Csc(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Csc (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Asin(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Asin (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Acos(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Acos (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Atan(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Atan (x.Value));
		}



		[SqlFunction]
		public static SqlDouble Acot(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Acot (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Acoth(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Acoth (x.Value));
		}


		[SqlFunction]
		public static SqlDouble Asec(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Asec (x.Value));
		}

		[SqlFunction]
		public static SqlDouble Acsc(SqlDouble x)
		{
			return new SqlDouble(MathNet.Numerics.Trig.Acsc (x.Value));
		}

	}
}

