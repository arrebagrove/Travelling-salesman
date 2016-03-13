using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFreight
{
	/// <summary>
	/// This object stores a subset of the original list of cities
	/// </summary>
	public class Subset
	{
		private List<int> citySubset;

		public Subset()
		{
			citySubset = null;
		}

		public Subset(List<int> cities)
		{
			citySubset = cities;
			citySubset.Sort();
		}

		/// <summary>
		/// Get the list of cities inside this subset
		/// </summary>
		/// <returns>List of cities</returns>
		public List<int> getCityList()
		{
			return citySubset.ToList();  //Make sure to clone the list
		}

		/// <summary>
		/// Check to see if this subset contains a specific city or not
		/// </summary>
		/// <param name="cityIndex"></param>
		/// <returns>true or false</returns>
		public bool ContainCity(int cityIndex)
		{
			if (citySubset.Contains(cityIndex))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Get the representation of the subset as an integer
		/// </summary>
		/// <returns>Integer key based on which cities are inside the subset</returns>
		public int getSubsetKey()
		{
			if (citySubset == null)
			{
				return 0;
			}
			int subsetKey = 0;

			foreach (int cityIndex in citySubset)
			{
				subsetKey += (1 << cityIndex);
			}
			return subsetKey;
		}

		/// <summary>
		/// Try to remove 1 city from this subset
		/// </summary>
		/// <param name="cityIndex">index of the city to remove</param>
		/// <returns>key of the resulting subset after removing the city</returns>
		public int removeOneCity(int cityIndex)
		{
			int subsetKey = this.getSubsetKey();
			return subsetKey - (1 << cityIndex);
		}

	}
}
