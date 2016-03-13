using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirFreight
{
	public partial class Maps : UserControl
	{
		private List<City> CitiesToDraw;
		private Pen pen1 = new Pen(Color.Blue, 2F);
		private Pen pen2 = new Pen(Color.Red, 5F);
		private Pen pen3 = new Pen(Color.Purple, 3F);
		private double minLatitude;
		private double minLongitude;

		public Maps()
		{
			InitializeComponent();
			CitiesToDraw = null;
			this.Paint += Maps_Paint;
		}

		public void DrawRoute(List<City> Route)
		{
			CitiesToDraw = Route;
			FindMinValues();
			this.Refresh();
		}


		public void FindMinValues()
		{
			minLatitude = double.MaxValue;
			minLongitude = double.MaxValue;
			City curCity;

			for (int i = 0; i < CitiesToDraw.Count; i++)
			{
				curCity = CitiesToDraw[i];
				if (curCity.Latitude < minLatitude)
				{
					minLatitude = curCity.Latitude;
				}

				if (curCity.Longitude < minLongitude)
				{
					minLongitude = curCity.Longitude;
				}
			}


		}

		private void Maps_Paint(object sender, PaintEventArgs e)
		{
			if (CitiesToDraw == null)
			{
				return;
			}

			Graphics gfx = e.Graphics;
			int cityNum = CitiesToDraw.Count;
			City curCity;
			System.Drawing.Point[] p = new System.Drawing.Point[cityNum];
			Rectangle rect;

			for (int i = 0; i < cityNum; i++)
			{
				curCity = CitiesToDraw[i];
				p[i].X = (Int32)((curCity.Longitude - minLongitude + 1) * 4);
				p[i].Y = (Int32)((curCity.Latitude - minLatitude + 1) * 4);

				if (i > 0)
				{
					rect = new Rectangle(p[i].X - 1, p[i].Y - 1, 3, 3);
					gfx.DrawEllipse(pen3, rect);
				}
			}

			gfx.DrawPolygon(pen1, p);
			rect = new Rectangle(p[0].X -2, p[0].Y -2, 5, 5);
			gfx.DrawEllipse(pen2, rect);
		}
	}
}
