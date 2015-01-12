using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace BioSimUI
{
	public class DataCollection: ISimulationOutput
	{
		private DataTable data					= new DataTable ( "Simulation Data"						);
		private DataColumn timeColumn			= new DataColumn( "Time",				typeof(int)		);
		private DataColumn xColumn				= new DataColumn( "X",					typeof(Decimal) );
		private DataColumn yColumn				= new DataColumn( "Y",					typeof(Decimal) );
		private DataColumn resourceColumn		= new DataColumn( "Resources",			typeof(Decimal) );
		private DataColumn constitutionColumn	= new DataColumn( "Constitution",		typeof(Decimal) );
		private DataColumn parasiteColumn		= new DataColumn( "Parasite",			typeof(int)		);
		private DataColumn simColumn			= new DataColumn( "Simulation",			typeof(int)		);

		public DataCollection(Model sim, bool listenForSimEvents)
		{
			init();
			
			if(listenForSimEvents)
			{sim.ParasiteEvent += new Model.ParasiteEventHandler(simDataHandler) ;}
		}

		public DataCollection(Model sim)
		{
			init();
		}

		private void init()
		{
			data.BeginInit();

			data.Columns.AddRange
				(
				new System.Data.DataColumn[] 
					{
						simColumn,
						timeColumn,
						parasiteColumn,
						xColumn,
						yColumn,
						resourceColumn,
						constitutionColumn
					}
				);

			data.EndInit();
		}

		public void Add
		(
			int simulation, 
			int time, 
			int parasite, 
			float xPosition, 
			float yPosition,
			float resources,
			float constitution
		)
		{
			DataRow newRow				= data.NewRow();
			newRow["Time"]				= time;
			newRow["X"]					= xPosition;
			newRow["Y"]					= yPosition;
			newRow["Resources"]			= resources;
			newRow["Constitution"]		= constitution;
			newRow["Parasite"]			= parasite;
			newRow["Simulation"]		= simulation;
		
			lock(data)
			{
				data.Rows.Add(newRow);
			}
		}

		private void simDataHandler(object sender, Model.ParasiteEventArgs e)
		{
			DataRow newRow				= data.NewRow();
			newRow["Time"]				= e.Time;
			newRow["X"]					= e.ParasiteChanged.Location.X;
			newRow["Y"]					= e.ParasiteChanged.Location.Y;
			newRow["Resources"]			= e.ParasiteChanged.Resources;
			newRow["Constitution"]		= e.ParasiteChanged.Constitution;
			newRow["Parasite"]			= e.ParasiteChanged.ID;
			newRow["Simulation"]		= e.HostAttachedTo.ID;
		
			lock(data)
			{
				data.Rows.Add(newRow);
			}
		}

		public DataTable Data
		{
			get
			{ 
				lock(data)
				{
					return data; 
				}
			}
		}

		public DataTable SnapShot
		{
			get
			{ 
				lock(data)
				{
					return data.Copy();
				}
			}
		}

		public int Rows
		{
			get
			{ 
				lock(data)
				{
					return data.Rows.Count; 
				}
			}
		}

		private int getMaxSim()
		{
			int maxSim =0;
			for( int i=0; (int)data.Rows[i]["Simulation"] >= maxSim; i++ )
			{
				maxSim = (int)data.Rows[i]["Simulation"];
			}
			return maxSim;
		}

		public void Save(Stream fOut)
		{
			StreamWriter writer = new StreamWriter(fOut);

			lock(data)
			{
				foreach( DataRow row in data.Rows )
				{
					string rowStr = "";
					foreach( object obj in row.ItemArray )
					{
						rowStr += obj + ", ";
					}

					// Remove superfluous comma
					rowStr.Remove(rowStr.Length-2, 2);

					writer.WriteLine(rowStr);
				}
			}
			
			writer.Close();
		}

		public void SaveSummary(Stream fOut)
		{
			StreamWriter writer = new StreamWriter(fOut);
	
			lock(data)
			{
				int currentTime = 1;
				int parasites = 0;
				int simulations = getMaxSim();

				for(int row=0; row< data.Rows.Count; row ++ )
				{
					int writeRow =0;

					if( (int)data.Rows[row]["Time"] != currentTime )
					{
						writeRow = row -1;
						currentTime = (int)data.Rows[row]["Time"];
					}
					
					if( row >= ( data.Rows.Count -1 ) )
					{
						parasites ++;
						writeRow = row;
					}

					if( writeRow > 0 )
					{
						writer.WriteLine( "{0}, {1}", (int)data.Rows[writeRow]["Time"], ((float)parasites/(float)simulations) );
						parasites =0;
					}
				
					parasites ++;
				}
			}

			writer.Close();
		}

	}
}
