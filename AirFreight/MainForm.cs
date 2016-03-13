using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirFreight
{
	public partial class MainForm : Form
	{
		public const int MAX_NUMBER_OF_CITIES = 21;

		private List<City> cityList;
		private List<City> TravelRoute = new List<City>();
		private PictureBox pbMaps = new PictureBox();
		private Maps drawingMaps = new Maps();

		private double [,] DistanceMatrix;
		double minimumTravelDistance = 0;
		Task<List<City>> myTask;
		CancellationToken ct;
		CancellationTokenSource tokenSource2;

		private bool isFinished = false;
		System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

		public MainForm()
		{
			InitializeComponent();
			cityList = new List<City>();

			flwPanel.Controls.Add(drawingMaps);
			pbProgress.Visible = false;
			pbProgress.Value = 0;
			lblStatus.Text = "";
			myTask = null;
			myTimer.Interval = 1000;
			myTimer.Tick += myTimer_Tick;
			myTimer.Start();
		}

		void myTimer_Tick(object sender, EventArgs e)
		{
			if (isFinished && TravelRoute != null)
			{
				printTravelingRoute();
				lblStatus.Text = "Finished.";
				btnStop.Enabled = false;
				pbProgress.Visible = false;
				cityList = new List<City>();   //Reset city list
			}
			else
			{
				if (cityList.Count > 0 && pbProgress.Value >= 1 && pbProgress.Value < cityList.Count)
				{
					pbProgress.Value++;
				}
			}
		}


		private void btnInput_Click(object sender, EventArgs e)
		{
			string result = SelectFile(textInputFile.Text, "Select Input File");
			if (result != null)
			{
				textInputFile.Text = result;
			}
		}

		/// <summary>
		/// Handle selecting a file
		/// </summary>
		/// <param name="firstChoice"></param>
		/// <param name="secondChoice"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		private string SelectFile(string firstChoice, string title)
		{
			OpenFileDialog fDialog = new OpenFileDialog();
			fDialog.Filter = "Text Files |*.txt";
			fDialog.Title = title;
			if (firstChoice.Length > 0)
			{
				fDialog.InitialDirectory = Path.GetDirectoryName(firstChoice);
			}

			fDialog.CheckFileExists = true;
			fDialog.CheckPathExists = true;
			if (fDialog.ShowDialog() == DialogResult.OK)
			{
				return fDialog.FileName.ToString();
			}
			return null;
		}

		private void bntCompute_Click(object sender, EventArgs e)
		{
			StreamReader sr;
			string cityName;
			double latitude, longitude;
			int maxNumOfCity = 0;
			City curCity;
			cityList = new List<City>();
			isFinished = false;

			try
			{
				sr = File.OpenText(textInputFile.Text);
				{
					string line;
					line = sr.ReadLine();
					Int32.TryParse(line, out maxNumOfCity);
					if (maxNumOfCity > MAX_NUMBER_OF_CITIES) { 
						maxNumOfCity = MAX_NUMBER_OF_CITIES; 
					}

					while ((line = sr.ReadLine()) != null)
					{
						string[] pieces = line.Split(',');
						if (pieces.Length < 2)
						{
							continue;
						}
						cityName = pieces[0];
						double.TryParse(pieces[1], out latitude);
						double.TryParse(pieces[2], out longitude);
						curCity = new City(cityName, latitude, longitude);

						cityList.Add(curCity);
						if (cityList.Count >= maxNumOfCity)
						{
							break;
						}
					}

					pbProgress.Maximum = maxNumOfCity;
					pbProgress.Value = 1;   //Start the progress bar
					pbProgress.Visible = true;
					lblStatus.Text = "Processing ...";
					btnStop.Enabled = true;

					GetDistanceMatrix();
					GetGreedyRoute();

					tokenSource2 = new CancellationTokenSource();
					ct = tokenSource2.Token;

					myTask = this.StartBackgroundWork();
					myTask.ContinueWith( (t, tks) =>
					{
						if (t.Status == TaskStatus.RanToCompletion)
						{
							this.TravelRoute = t.Result;
							isFinished = true;
						}
					}, TaskScheduler.FromCurrentSynchronizationContext()
					, tokenSource2.Token);

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region Helper functions
		/// <summary>
		/// Populate the city distance matrix
		/// </summary>
		public void GetDistanceMatrix()
		{
			int cityNum = cityList.Count;
			DistanceMatrix = new double[MAX_NUMBER_OF_CITIES+1, MAX_NUMBER_OF_CITIES+1];
			double curDistance;

			for (int i=0; i<cityNum; i++) {
				for (int j=i+1; j<cityNum; j++) {
					DistanceMatrix[i, i] = double.MaxValue;

					curDistance = cityList[i].getDistance(cityList[j]);
					DistanceMatrix[i, j] = curDistance;
					DistanceMatrix[j, i] = curDistance;
				}
			}
		}

		/// <summary>
		/// Print the Traveling route
		/// </summary>
		public void printTravelingRoute()
		{
			City curCity, nextCity;
			minimumTravelDistance = 0;
			txtRoute.Text = "Travel route: " + System.Environment.NewLine;
			for (int i = 0; i < TravelRoute.Count; i++)
			{
				curCity = TravelRoute[i];
				txtRoute.Text += (i+1) + ". " + curCity.Name + System.Environment.NewLine;
				if (i < TravelRoute.Count - 1)
				{
					nextCity = TravelRoute[i+1];
				}
				else
				{
					nextCity = cityList[0];
				}
				minimumTravelDistance += curCity.getDistance(nextCity);
			}

			curCity = cityList[0];
			txtRoute.Text += (TravelRoute.Count + 1) + ". " + curCity.Name + System.Environment.NewLine;
			lblMinDistance.Text = "Minimum travel distance: " + String.Format("{0:0.00}", minimumTravelDistance);
			lblTravelMap.Text = "Travel map: ";
			drawingMaps.DrawRoute(TravelRoute);
			this.Refresh();
			TravelRoute = null;
		}
		#endregion

		#region Greedy route
		/// <summary>
		/// Greedy route: always go to the nearest city
		/// </summary>
		public void GetGreedyRoute()
		{
			int cityNum = cityList.Count;
			int numOfVisited, i, curCityIndex, nextCityIndex;
			bool[] visited = new bool[cityNum];
			double minDistance;
			List<int> TravelIndex = new List<int>();

			City curCity = cityList[0];   //Home city - Albany
			numOfVisited = 1;
			curCityIndex = 0;
			visited[0] = true;
			TravelIndex.Add(0);

			while (numOfVisited < cityNum)
			{
				minDistance = double.MaxValue;
				nextCityIndex = 0;
				for (i = 0; i < cityNum; i++)
				{
					if (!visited[i] && DistanceMatrix[curCityIndex, i] < minDistance)
					{
						minDistance = DistanceMatrix[curCityIndex, i];
						nextCityIndex = i;
					}
				}

				visited[nextCityIndex] = true;
				TravelIndex.Add(nextCityIndex);
				curCityIndex = nextCityIndex;
				numOfVisited++;
			}

			//Reconstruct the route and draw it	
			this.TravelRoute = new List<City>();
			for (i = 0; i < TravelIndex.Count; i++)
			{
				curCityIndex = TravelIndex[i];
				curCity = cityList[curCityIndex];
				TravelRoute.Add(curCity);
			}

			printTravelingRoute();
		}
		#endregion

		private void btnStop_Click(object sender, EventArgs e)
		{
			lblStatus.Text = "Stopped!";
			pbProgress.Visible = false;

			isFinished = true;
			TravelRoute = null;
			tokenSource2.Cancel();
		}

		private Task<List<City>> StartBackgroundWork()
		{
			return new CalculationWork().DoCalculation(cityList, DistanceMatrix);
		}


	}
}
