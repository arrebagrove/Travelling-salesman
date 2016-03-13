using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFreight
{
	/// <summary>
	/// Coordinates class to store the Latitude and Longitude of any point in a flat Earth
	/// </summary>
	public class Coordinates
	{
		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public Coordinates()
		{
		}

		public Coordinates(double inLatitude, double inLongitude)
		{
			Latitude = inLatitude;
			Longitude = inLongitude;
		}

		/// <summary>
		/// Calculate the distance between the current coordinate and the target coordinate
		/// </summary>
		/// <param name="target">Target coordinates</param>
		/// <returns>Euclidean distance to the target coordinate</returns>
		public double getDistance(Coordinates target)
		{
			return getDistance(target.Latitude, target.Longitude);
		}

		/// <summary>
		/// Calculate the distance between the current coordinate and the target Latitude and Longitude
		/// </summary>
		/// <param name="targetLatitude"></param>
		/// <param name="targetLongitude"></param>
		/// <returns>Euclidean distance to the target</returns>
		public double getDistance(double targetLatitude, double targetLongitude)
		{
			return Math.Sqrt(Math.Pow(this.Latitude - targetLatitude, 2) + Math.Pow(this.Longitude - targetLongitude, 2));
		}

	}
}
