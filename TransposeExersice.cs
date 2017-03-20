using System;
using System.Linq;

namespace MetricTransposer
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			bool displayMenu = true;
			while (displayMenu)
			{
				displayMenu = MainMenu();
			}
		}


		public static bool MainMenu()
		{
			Console.Clear();
			Console.WriteLine("1) Transpose metric using pre-difned metric data");
			Console.WriteLine("2) Transpose metric using your own metric data");
			Console.WriteLine("3) Exit");

			string userInput = Console.ReadLine();

			if (userInput == "1")
			{
				Console.Clear();
				Transpose.PrintTransposedVsOriginalMetrics(Globals.MetricData);
				Console.ReadLine();
				return true;
			}

			if (userInput == "2")
			{
				Console.Clear();
				string[,] userMetric = Metrics.GetUserMetricInput();
				Transpose.PrintTransposedVsOriginalMetrics(userMetric);
				Console.ReadLine();
				return true;
			}

			if (userInput == "3")
				return false;
			else
			{
				Console.WriteLine("Please enter valid option");
				return true;
			}
		}
	}

	public static class Transpose
	{
		public static string[,] TransposeMetric(string[,] metric)
		{
			string[,] transpoedMetric = new string[metric.GetLength(1), metric.GetLength(0)];
			for (int i = 0; i < metric.GetLength(0); i++)
			{
				for (int j = i; j < metric.GetLength(1); j++)
				{
					transpoedMetric[j, i] = metric[i, j];
					transpoedMetric[i, j] = metric[j, i];
				}
			}
			return transpoedMetric;
		}

		public static void PrintTransposedMetric(string[,] metric)
		{
			Metrics.MetricPrinter(Transpose.TransposeMetric(metric));
		}

		public static void PrintTransposedVsOriginalMetrics(string[,] metric)
		{
			Console.WriteLine("The original metric was:");
			Metrics.MetricPrinter(metric);
			Console.WriteLine();
			Console.WriteLine("The metric after transpose is: ");
			Transpose.PrintTransposedMetric(metric);
		}
	}

	public static class Metrics
	{
		public static void MetricPrinter(string[,] metric)
		{
			string[,] EqualizedMetric = Metrics.MetricStringEqualizer(metric);
			for (int i = 0; i < metric.GetLength(0); i++)
			{
				for (int j = 0; j < EqualizedMetric.GetLength(1); j++)
				{
					Console.Write("|{0}", EqualizedMetric[i, j]);
				}
				Console.Write("|\n");
			}
		}

		public static string[,] GetUserMetricInput()
		{
			Console.WriteLine("Enter your metric size:");
			Console.WriteLine("Please enter the number of rows: ");
			int metricRows = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine("\nPlease enter the number of colums: ");
			int metricCols = Convert.ToInt32(Console.ReadLine());

			string[,] userMetric = new string[metricRows, metricCols];

			for (int i = 0; i < metricRows; i++)
			{
				for (int j = 0; j < metricCols; j++)
				{
					Console.Write("[{0},{1}]: ", i, j);
					userMetric[i, j] = Console.ReadLine();
				}
				Console.WriteLine();
			}
			return userMetric;
		}

		private static string[,] MetricStringEqualizer(string[,] metric)
		//Take a metric as an input
		//Equalizing the number of char in each cell
		//Filling the gap with spaces (" ")
		{
			string[,] equalizedMetric = new string[metric.GetLength(0), metric.GetLength(1)];
			int maxCharsInMetric = Metrics.FindMaxCharInMatrics(metric);
			for (int i = 0; i < equalizedMetric.GetLength(0); i++)
			{
				for (int j = 0; j < equalizedMetric.GetLength(1); j++)
				{
					int charGap = maxCharsInMetric - metric[i, j].Length;
					equalizedMetric[i, j] = String.Concat(Enumerable.Repeat(" ", charGap)) + metric[i, j];
				}
			}

			return equalizedMetric;
		}

		private static int FindMaxCharInMatrics(string[,] metric)
		{
			int maxChar = 0;
			for (int i = 0; i < metric.GetLength(0); i++)
			{
				for (int j = 0; j < metric.GetLength(1); j++)
				{
					if (metric[i, j].Length > maxChar)
					{
						maxChar = metric[i, j].Length;
					}
				}
			}
			return maxChar;
		}
	}

	public static class Globals
	{
		public static string[,] MetricData = new string[,] { { "01", "02", "03" }, { "05", "06", "07" }, { "09", "10", "11" } };
	}
}
