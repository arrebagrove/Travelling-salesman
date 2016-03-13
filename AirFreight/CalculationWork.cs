using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirFreight
{
	/// <summary>
	/// Main object to calculate the travel route with Dynamic Programming mechnism
	/// </summary>
	public class CalculationWork
	{
		private TaskCompletionSource<List<City>> completionSource;
		private List<City> cityList, TravelRoute;
		private double[,] DistanceMatrix;

		public Task<List<City>> DoCalculation(List<City> inCityList, double[,] inDistanceMatrix)
		{
			cityList = inCityList.ToList();
			DistanceMatrix = inDistanceMatrix;

			this.completionSource = new TaskCompletionSource<List<City>>();
			
			new Thread(this.DPCalculation).Start();

			return this.completionSource.Task;
		}

		private void DPCalculation()
		{
			DPRoute();
			this.completionSource.SetResult(TravelRoute);
		}


		public void InitializeSubsetSizeOne(Dictionary<int, Subset> subsetDict, double[,] minDistance)
		{
			int cityNum = cityList.Count;
			Subset tempSubset;
			int sKey, i;
			List<int> tempLists;

			for (i = 0; i < cityNum; i++)
			{
				tempLists = new List<int>();
				tempLists.Add(i);
				tempSubset = new Subset(tempLists);
				sKey = tempSubset.getSubsetKey();
				subsetDict.Add(sKey, tempSubset);
				if (i == 0)
				{
					minDistance[sKey, 0] = 0;
				}
				else
				{
					minDistance[sKey, 0] = double.MaxValue;
				}
			}

		}

		/// <summary>
		/// Calculate Travel Route with Dynamic programming
		/// </summary>
		public void DPRoute()
		{
			int cityNum = cityList.Count;
			int sKey, cKey, prevKey, subsetSize, i, j, k, endCityIndex, prevCityIndex, prevCity;
			int maxSize = 1 << cityNum;
			double distance, calDistance;
			Subset tempSubset, curSubset;
			Dictionary<int, Subset> subsetDict = new Dictionary<int, Subset>();
			List<int> tempLists = new List<int>();
			List<int> subQueue = new List<int>();
			List<int> nextQueue;
			double[,] minDistance = new double[maxSize, cityNum];
			int[,] prevCityInRoute = new int[maxSize, cityNum];

			InitializeSubsetSizeOne(subsetDict, minDistance);
			subQueue.Add(1);   //Starting subset with just home city

			for (subsetSize = 2; subsetSize <= cityNum; subsetSize++)
			{
				nextQueue = new List<int>();
				//Populate all subsets with the size of subsetSize
				for (i = 0; i < subQueue.Count; i++)
				{
					prevKey = subQueue[i];
					tempSubset = subsetDict[prevKey];
					for (j = 1; j < cityNum; j++)
					{
						if (!tempSubset.ContainCity(j))
						{
							tempLists = tempSubset.getCityList();
							tempLists.Add(j);
							curSubset = new Subset(tempLists);
							cKey = curSubset.getSubsetKey();
							if (!subsetDict.ContainsKey(cKey))
							{
								subsetDict.Add(cKey, curSubset);
								minDistance[cKey, 0] = double.MaxValue;
								nextQueue.Add(cKey);
							}
						}
					}
				}

				for (i = 0; i < nextQueue.Count; i++)
				{
					cKey = nextQueue[i];
					curSubset = subsetDict[cKey];
					tempLists = curSubset.getCityList();
					for (j = 1; j < tempLists.Count; j++)
					{
						endCityIndex = tempLists[j];
						distance = double.MaxValue;
						prevKey = curSubset.removeOneCity(endCityIndex);
						prevCity = 0;
						for (k = 0; k < tempLists.Count; k++)
						{
							prevCityIndex = tempLists[k];
							if (prevCityIndex != endCityIndex)
							{
								calDistance = minDistance[prevKey, prevCityIndex] + DistanceMatrix[prevCityIndex, endCityIndex];
								if (calDistance < distance)
								{
									distance = calDistance;
									prevCity = prevCityIndex;
								}
							}
						}

						minDistance[cKey, endCityIndex] = distance;
						prevCityInRoute[cKey, endCityIndex] = prevCity;
					}
				}
				subQueue = nextQueue;
			}

			//At this point, the only subset left contains all the cities
			cKey = subQueue[0];
			distance = minDistance[cKey, 1] + DistanceMatrix[1, 0];
			endCityIndex = 1;
			for (j = 2; j < cityNum; j++)
			{
				if (minDistance[cKey, j] + DistanceMatrix[j, 0] < distance)
				{
					distance = minDistance[cKey, j] + DistanceMatrix[j, 0];
					endCityIndex = j;
				}
			}

			backTrackRoute(endCityIndex, cKey, prevCityInRoute, subsetDict);
		}

		/// <summary>
		/// Backtracking and generate the travel route
		/// </summary>
		/// <param name="endCityIndex"></param>
		/// <param name="sKey"></param>
		/// <param name="prevCityInRoute"></param>
		/// <param name="subsetDict"></param>
		public void backTrackRoute(int endCityIndex, int sKey, int[,] prevCityInRoute, Dictionary<int, Subset> subsetDict)
		{
			List<int> tempRoute = new List<int>();
			Subset curSubset;
			int cityIndex, tempCityIndex;

			while (endCityIndex != 0)
			{
				tempRoute.Add(endCityIndex);
				curSubset = subsetDict[sKey];
				tempCityIndex = endCityIndex;
				endCityIndex = prevCityInRoute[sKey, endCityIndex];
				sKey = curSubset.removeOneCity(tempCityIndex);
			}
			tempRoute.Add(0);

			TravelRoute = new List<City>();
			for (int i = tempRoute.Count - 1; i >= 0; i--)
			{
				cityIndex = tempRoute[i];
				TravelRoute.Add(cityList[cityIndex]);
			}
		}
	}
}
