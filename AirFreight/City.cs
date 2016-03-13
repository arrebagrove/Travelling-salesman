using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFreight
{
	/// <summary>
	/// City object to store name and coordinate of each city
	/// </summary>
	public class City : Coordinates
	{
		/// <summary>
		/// Name of the city
		/// </summary>
		public string Name { get; set; }

		public City()
		{
		}

		public City(string cityName, double inLatitude, double inLongitude)
		{
			this.Name = cityName;
			this.Latitude = inLatitude;
			this.Longitude = inLongitude;
		}

		/// <summary>
		/// Calculate the distance between this city and the target city
		/// </summary>
		/// <param name="targetCity"></param>
		/// <returns></returns>
		public double getDistance(City targetCity)
		{
			return base.getDistance(targetCity.Latitude, targetCity.Longitude);
		}

		public override string ToString()
		{
			return Name + ", Lat:" + Latitude + ", Long:" + Longitude;
		}
	}
}
