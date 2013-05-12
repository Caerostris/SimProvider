using System;
using System.IO;

namespace SimProvider
{
	public class CSVExport
	{
		public CSVExport()
		{
			SimProvider.Bike sim = new SimProvider.Bike();
			SimProvider.Bike sim1 = new SimProvider.Bike();
			string chart = "Time,Velocity,Acceleration\n";
			chart += "0," + sim.Veclocity + "," + sim.Acceleration + "\n";

			for (int i = 1; i < 50; i++)
			{
				sim.update(1);
				chart += i + "," + sim.Veclocity + "," + sim.Acceleration + "\n";
			}

			chart += ",,\n,,\n,,\n";

			chart += "0," + sim1.Veclocity + "," + sim1.Acceleration + "\n";
			for(int i = 1; i < 100; i++)
			{
				sim1.update(0.5);
				chart += i + "," + sim1.Veclocity + "," + sim1.Acceleration + "\n";
			}
			System.IO.File.WriteAllText("chart.csv", chart);
		}
	}
}

