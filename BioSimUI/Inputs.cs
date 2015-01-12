#region Imports

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

#endregion

namespace BioSimUI
{
	/// <summary>
	/// The parameter user input form for the host parasite dynamics simuluation
	/// </summary>
	public class simSetup : Form
	{
		private const string path = "input.txt";

		#region GUI Component Definitions

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel panel15;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Panel panel17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Panel panel18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Panel panel21;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Panel panel22;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Panel panel23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Panel panel25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Button runBtn;
		private System.Windows.Forms.NumericUpDown width;
		private System.Windows.Forms.NumericUpDown height;
		private System.Windows.Forms.Panel hostPnl;
		private System.Windows.Forms.Label hostLbl;
		private System.Windows.Forms.NumericUpDown decay;
		private System.Windows.Forms.NumericUpDown minResources;
		private System.Windows.Forms.NumericUpDown maxResources;
		private System.Windows.Forms.Panel parasitePnl;
		private System.Windows.Forms.NumericUpDown moveDist;
		private System.Windows.Forms.NumericUpDown constitution;
		private System.Windows.Forms.NumericUpDown damage;
		private System.Windows.Forms.Label parasiteLbl;
		private System.Windows.Forms.NumericUpDown resources;
		private System.Windows.Forms.NumericUpDown resReqired;
		private System.Windows.Forms.Panel simPnl;
		private System.Windows.Forms.Label simLbl;
		private System.Windows.Forms.NumericUpDown duration;
		private System.Windows.Forms.NumericUpDown repetitions;
		private System.Windows.Forms.NumericUpDown initParasites;
		private System.Windows.Forms.Label titleLbl;
		private System.Windows.Forms.Panel immunePnl;
		private System.Windows.Forms.Label immuneLbl;
		private System.Windows.Forms.Panel resPnl;
		private System.Windows.Forms.Label resLbl;
		private System.Windows.Forms.Panel dimensionsPnl;
		private System.Windows.Forms.Label dimensionsLbl;
		private System.Windows.Forms.Panel movePnl;
		private System.Windows.Forms.Label moveLbl;
		private System.Windows.Forms.Panel statusPnl;
		private System.Windows.Forms.Label statusLbl;
		private System.Windows.Forms.Panel immuneParasitePnl;
		private System.Windows.Forms.Label immuneParasiteLbl;
		private System.Windows.Forms.Panel reproductionPnl;
		private System.Windows.Forms.Label ReproductionLbl;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel seedPnl;
		private System.Windows.Forms.NumericUpDown seed;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown compitence;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel gridPnl;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown columns;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Panel panel12;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.NumericUpDown rows;
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor

		public simSetup()
		{
			InitializeComponent();
			load(path);
		}

		#endregion

		#region Destructor

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// TODO Check that this actualy closes the application down safely
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Main method

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			System.Windows.Forms.Application.Run(new simSetup());
		}

		#endregion

		#region File IO

		private void save( string path )
		{	
			string toSave = "";
			
			toSave += width.Value			 + "\r";
			toSave += height.Value			 + "\r";
			toSave += decay.Value			 + "\r";
			toSave += compitence.Value		 + "\r";
			toSave += rows.Value			 + "\r";
			toSave += columns.Value			 + "\r";
			toSave += minResources.Value	 + "\r";
			toSave += maxResources.Value	 + "\r";
			toSave += moveDist.Value		 + "\r";
			toSave += constitution.Value	 + "\r";
			toSave += damage.Value			 + "\r";
			toSave += resources.Value		 + "\r";
			toSave += resReqired.Value		 + "\r";
			toSave += duration.Value		 + "\r";
			toSave += repetitions.Value		 + "\r";
			toSave += initParasites.Value	 + "\r";
			toSave += seed.Value			 + "\r";

			if( checkBox1.Checked )
			{
				toSave += "True";
			}
			else
			{
				toSave += "False";
			}

			System.IO.StreamWriter fOut = new System.IO.StreamWriter(path, false);

			try 
			{
				fOut.Write(toSave);
			}
			catch( Exception )
			{
				// Stop writing values
			}
			finally
			{ 
				fOut.Close();
			}
		}

		private void load( string path )
		{	
			if( new System.IO.FileInfo(path).Exists )
			{
				System.IO.StreamReader fIn = new System.IO.StreamReader(path);
				
				try 
				{
					width.Value				= decimal.Parse( fIn.ReadLine() );
					height.Value			= decimal.Parse( fIn.ReadLine() );
					decay.Value				= decimal.Parse( fIn.ReadLine() );
					compitence.Value		= decimal.Parse( fIn.ReadLine() );
					rows.Value				= decimal.Parse( fIn.ReadLine() );
					columns.Value			= decimal.Parse( fIn.ReadLine() );
					minResources.Value		= decimal.Parse( fIn.ReadLine() );
					maxResources.Value		= decimal.Parse( fIn.ReadLine() );
					moveDist.Value			= decimal.Parse( fIn.ReadLine() );
					constitution.Value		= decimal.Parse( fIn.ReadLine() );
					damage.Value			= decimal.Parse( fIn.ReadLine() );
					resources.Value			= decimal.Parse( fIn.ReadLine() );
					resReqired.Value		= decimal.Parse( fIn.ReadLine() );
					duration.Value			= decimal.Parse( fIn.ReadLine() );
					repetitions.Value		= decimal.Parse( fIn.ReadLine() );
					initParasites.Value		= decimal.Parse( fIn.ReadLine() );
					seed.Value				= decimal.Parse( fIn.ReadLine() );
				

					if( fIn.ReadLine() == "True" )
					{
						checkBox1.Checked = true;
					}
					else
					{
						checkBox1.Checked = false;
					}
				}
				catch( Exception )
				{
					// Stop reading values
				}
				finally
				{ 
					// Close the stream
					fIn.Close(); 
				}

			}
		}

		#endregion

		#region Run the simulation

		private void runBtn_Click(object sender, System.EventArgs e)
		{
			save(path);

			HostFactory hostFactory = new HostFactory((float)minResources.Value, (float)maxResources.Value, (float)damage.Value, (float)decay.Value, (float)compitence.Value, (int)rows.Value, (int)columns.Value);
			ParasiteFactory parasiteFactory = new ParasiteFactory((float)constitution.Value, (float)resources.Value, (float)resReqired.Value);
			SimFactory simFactory = new SimFactory((int)initParasites.Value, (float)moveDist.Value, parasiteFactory, (float)height.Value, (float)width.Value, hostFactory);

			Model sim;
			if( checkBox1.Checked )
			{
				sim = new Model((int)repetitions.Value, simFactory, (int)seed.Value);
			}
			else
			{
				sim = new Model((int)repetitions.Value, simFactory);
			}
			
			Outputs output = new Outputs(sim, (int)duration.Value);
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.width = new System.Windows.Forms.NumericUpDown();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.height = new System.Windows.Forms.NumericUpDown();
			this.runBtn = new System.Windows.Forms.Button();
			this.hostPnl = new System.Windows.Forms.Panel();
			this.resPnl = new System.Windows.Forms.Panel();
			this.resLbl = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.minResources = new System.Windows.Forms.NumericUpDown();
			this.panel7 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.maxResources = new System.Windows.Forms.NumericUpDown();
			this.immunePnl = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.compitence = new System.Windows.Forms.NumericUpDown();
			this.immuneLbl = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.decay = new System.Windows.Forms.NumericUpDown();
			this.dimensionsPnl = new System.Windows.Forms.Panel();
			this.dimensionsLbl = new System.Windows.Forms.Label();
			this.hostLbl = new System.Windows.Forms.Label();
			this.gridPnl = new System.Windows.Forms.Panel();
			this.panel10 = new System.Windows.Forms.Panel();
			this.label15 = new System.Windows.Forms.Label();
			this.columns = new System.Windows.Forms.NumericUpDown();
			this.label17 = new System.Windows.Forms.Label();
			this.panel12 = new System.Windows.Forms.Panel();
			this.label20 = new System.Windows.Forms.Label();
			this.rows = new System.Windows.Forms.NumericUpDown();
			this.parasitePnl = new System.Windows.Forms.Panel();
			this.reproductionPnl = new System.Windows.Forms.Panel();
			this.ReproductionLbl = new System.Windows.Forms.Label();
			this.panel21 = new System.Windows.Forms.Panel();
			this.label21 = new System.Windows.Forms.Label();
			this.resReqired = new System.Windows.Forms.NumericUpDown();
			this.movePnl = new System.Windows.Forms.Panel();
			this.moveLbl = new System.Windows.Forms.Label();
			this.panel11 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.moveDist = new System.Windows.Forms.NumericUpDown();
			this.statusPnl = new System.Windows.Forms.Panel();
			this.panel18 = new System.Windows.Forms.Panel();
			this.label19 = new System.Windows.Forms.Label();
			this.resources = new System.Windows.Forms.NumericUpDown();
			this.statusLbl = new System.Windows.Forms.Label();
			this.panel15 = new System.Windows.Forms.Panel();
			this.label13 = new System.Windows.Forms.Label();
			this.constitution = new System.Windows.Forms.NumericUpDown();
			this.immuneParasitePnl = new System.Windows.Forms.Panel();
			this.immuneParasiteLbl = new System.Windows.Forms.Label();
			this.panel17 = new System.Windows.Forms.Panel();
			this.label16 = new System.Windows.Forms.Label();
			this.damage = new System.Windows.Forms.NumericUpDown();
			this.parasiteLbl = new System.Windows.Forms.Label();
			this.simPnl = new System.Windows.Forms.Panel();
			this.seedPnl = new System.Windows.Forms.Panel();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.seed = new System.Windows.Forms.NumericUpDown();
			this.panel25 = new System.Windows.Forms.Panel();
			this.label26 = new System.Windows.Forms.Label();
			this.initParasites = new System.Windows.Forms.NumericUpDown();
			this.panel22 = new System.Windows.Forms.Panel();
			this.label22 = new System.Windows.Forms.Label();
			this.duration = new System.Windows.Forms.NumericUpDown();
			this.panel23 = new System.Windows.Forms.Panel();
			this.label24 = new System.Windows.Forms.Label();
			this.repetitions = new System.Windows.Forms.NumericUpDown();
			this.simLbl = new System.Windows.Forms.Label();
			this.titleLbl = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
			this.hostPnl.SuspendLayout();
			this.resPnl.SuspendLayout();
			this.panel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.minResources)).BeginInit();
			this.panel7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.maxResources)).BeginInit();
			this.immunePnl.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.compitence)).BeginInit();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.decay)).BeginInit();
			this.dimensionsPnl.SuspendLayout();
			this.gridPnl.SuspendLayout();
			this.panel10.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.columns)).BeginInit();
			this.panel12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rows)).BeginInit();
			this.parasitePnl.SuspendLayout();
			this.reproductionPnl.SuspendLayout();
			this.panel21.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.resReqired)).BeginInit();
			this.movePnl.SuspendLayout();
			this.panel11.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.moveDist)).BeginInit();
			this.statusPnl.SuspendLayout();
			this.panel18.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.resources)).BeginInit();
			this.panel15.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.constitution)).BeginInit();
			this.immuneParasitePnl.SuspendLayout();
			this.panel17.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.damage)).BeginInit();
			this.simPnl.SuspendLayout();
			this.seedPnl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.seed)).BeginInit();
			this.panel25.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.initParasites)).BeginInit();
			this.panel22.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.duration)).BeginInit();
			this.panel23.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.repetitions)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.width);
			this.panel1.Location = new System.Drawing.Point(8, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(192, 32);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Width";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// width
			// 
			this.width.DecimalPlaces = 4;
			this.width.Increment = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	65536});
			this.width.Location = new System.Drawing.Point(64, 8);
			this.width.Maximum = new System.Decimal(new int[] {
																  1000000,
																  0,
																  0,
																  0});
			this.width.Name = "width";
			this.width.TabIndex = 0;
			this.width.Validating += new System.ComponentModel.CancelEventHandler(this.width_Validating);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.height);
			this.panel2.Location = new System.Drawing.Point(8, 64);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(192, 32);
			this.panel2.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 24);
			this.label2.TabIndex = 0;
			this.label2.Text = "Height";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// height
			// 
			this.height.DecimalPlaces = 4;
			this.height.Increment = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 65536});
			this.height.Location = new System.Drawing.Point(64, 8);
			this.height.Maximum = new System.Decimal(new int[] {
																   1000000,
																   0,
																   0,
																   0});
			this.height.Name = "height";
			this.height.TabIndex = 1;
			this.height.Validating += new System.ComponentModel.CancelEventHandler(this.height_Validating);
			// 
			// runBtn
			// 
			this.runBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.runBtn.Location = new System.Drawing.Point(0, 638);
			this.runBtn.Name = "runBtn";
			this.runBtn.Size = new System.Drawing.Size(512, 40);
			this.runBtn.TabIndex = 18;
			this.runBtn.Text = "Run";
			this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
			// 
			// hostPnl
			// 
			this.hostPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.hostPnl.Controls.Add(this.resPnl);
			this.hostPnl.Controls.Add(this.immunePnl);
			this.hostPnl.Controls.Add(this.dimensionsPnl);
			this.hostPnl.Controls.Add(this.hostLbl);
			this.hostPnl.Controls.Add(this.gridPnl);
			this.hostPnl.Location = new System.Drawing.Point(8, 40);
			this.hostPnl.Name = "hostPnl";
			this.hostPnl.Size = new System.Drawing.Size(232, 480);
			this.hostPnl.TabIndex = 0;
			// 
			// resPnl
			// 
			this.resPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.resPnl.Controls.Add(this.resLbl);
			this.resPnl.Controls.Add(this.panel6);
			this.resPnl.Controls.Add(this.panel7);
			this.resPnl.Location = new System.Drawing.Point(8, 368);
			this.resPnl.Name = "resPnl";
			this.resPnl.Size = new System.Drawing.Size(208, 104);
			this.resPnl.TabIndex = 0;
			// 
			// resLbl
			// 
			this.resLbl.BackColor = System.Drawing.Color.Transparent;
			this.resLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.resLbl.Location = new System.Drawing.Point(0, 0);
			this.resLbl.Name = "resLbl";
			this.resLbl.Size = new System.Drawing.Size(208, 24);
			this.resLbl.TabIndex = 0;
			this.resLbl.Text = "Resources";
			this.resLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.label6);
			this.panel6.Controls.Add(this.minResources);
			this.panel6.Location = new System.Drawing.Point(8, 24);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(192, 32);
			this.panel6.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "Minimum available";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// minResources
			// 
			this.minResources.DecimalPlaces = 4;
			this.minResources.Increment = new System.Decimal(new int[] {
																		   1,
																		   0,
																		   0,
																		   65536});
			this.minResources.Location = new System.Drawing.Point(112, 8);
			this.minResources.Maximum = new System.Decimal(new int[] {
																		 1000000,
																		 0,
																		 0,
																		 0});
			this.minResources.Name = "minResources";
			this.minResources.Size = new System.Drawing.Size(72, 20);
			this.minResources.TabIndex = 6;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.label7);
			this.panel7.Controls.Add(this.maxResources);
			this.panel7.Location = new System.Drawing.Point(8, 64);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(192, 32);
			this.panel7.TabIndex = 0;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 16);
			this.label7.TabIndex = 0;
			this.label7.Text = "Maximum available";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// maxResources
			// 
			this.maxResources.DecimalPlaces = 4;
			this.maxResources.Increment = new System.Decimal(new int[] {
																		   1,
																		   0,
																		   0,
																		   65536});
			this.maxResources.Location = new System.Drawing.Point(112, 8);
			this.maxResources.Maximum = new System.Decimal(new int[] {
																		 1000000,
																		 0,
																		 0,
																		 0});
			this.maxResources.Name = "maxResources";
			this.maxResources.Size = new System.Drawing.Size(72, 20);
			this.maxResources.TabIndex = 7;
			this.maxResources.Validating += new System.ComponentModel.CancelEventHandler(this.resources_Validating);
			// 
			// immunePnl
			// 
			this.immunePnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.immunePnl.Controls.Add(this.panel3);
			this.immunePnl.Controls.Add(this.immuneLbl);
			this.immunePnl.Controls.Add(this.panel4);
			this.immunePnl.Location = new System.Drawing.Point(8, 144);
			this.immunePnl.Name = "immunePnl";
			this.immunePnl.Size = new System.Drawing.Size(208, 104);
			this.immunePnl.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.compitence);
			this.panel3.Location = new System.Drawing.Point(8, 64);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(192, 32);
			this.panel3.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Compitence";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// compitence
			// 
			this.compitence.DecimalPlaces = 4;
			this.compitence.Increment = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 65536});
			this.compitence.Location = new System.Drawing.Point(112, 8);
			this.compitence.Maximum = new System.Decimal(new int[] {
																	   1000000,
																	   0,
																	   0,
																	   0});
			this.compitence.Name = "compitence";
			this.compitence.Size = new System.Drawing.Size(72, 20);
			this.compitence.TabIndex = 3;
			// 
			// immuneLbl
			// 
			this.immuneLbl.BackColor = System.Drawing.Color.Transparent;
			this.immuneLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.immuneLbl.Location = new System.Drawing.Point(0, 0);
			this.immuneLbl.Name = "immuneLbl";
			this.immuneLbl.Size = new System.Drawing.Size(200, 24);
			this.immuneLbl.TabIndex = 0;
			this.immuneLbl.Text = "Immune response";
			this.immuneLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.label8);
			this.panel4.Controls.Add(this.label4);
			this.panel4.Controls.Add(this.decay);
			this.panel4.Location = new System.Drawing.Point(8, 24);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(192, 32);
			this.panel4.TabIndex = 0;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(96, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(16, 16);
			this.label8.TabIndex = 3;
			this.label8.Text = "%";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Decay";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// decay
			// 
			this.decay.DecimalPlaces = 2;
			this.decay.Location = new System.Drawing.Point(112, 8);
			this.decay.Name = "decay";
			this.decay.Size = new System.Drawing.Size(72, 20);
			this.decay.TabIndex = 2;
			// 
			// dimensionsPnl
			// 
			this.dimensionsPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dimensionsPnl.Controls.Add(this.dimensionsLbl);
			this.dimensionsPnl.Controls.Add(this.panel1);
			this.dimensionsPnl.Controls.Add(this.panel2);
			this.dimensionsPnl.Location = new System.Drawing.Point(8, 32);
			this.dimensionsPnl.Name = "dimensionsPnl";
			this.dimensionsPnl.Size = new System.Drawing.Size(208, 104);
			this.dimensionsPnl.TabIndex = 0;
			// 
			// dimensionsLbl
			// 
			this.dimensionsLbl.BackColor = System.Drawing.Color.Transparent;
			this.dimensionsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dimensionsLbl.Location = new System.Drawing.Point(0, 0);
			this.dimensionsLbl.Name = "dimensionsLbl";
			this.dimensionsLbl.Size = new System.Drawing.Size(200, 24);
			this.dimensionsLbl.TabIndex = 0;
			this.dimensionsLbl.Text = "Dimensions";
			this.dimensionsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// hostLbl
			// 
			this.hostLbl.BackColor = System.Drawing.Color.Transparent;
			this.hostLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hostLbl.Location = new System.Drawing.Point(0, 0);
			this.hostLbl.Name = "hostLbl";
			this.hostLbl.Size = new System.Drawing.Size(232, 24);
			this.hostLbl.TabIndex = 0;
			this.hostLbl.Text = "Host";
			this.hostLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// gridPnl
			// 
			this.gridPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gridPnl.Controls.Add(this.panel10);
			this.gridPnl.Controls.Add(this.label17);
			this.gridPnl.Controls.Add(this.panel12);
			this.gridPnl.Location = new System.Drawing.Point(8, 256);
			this.gridPnl.Name = "gridPnl";
			this.gridPnl.Size = new System.Drawing.Size(208, 104);
			this.gridPnl.TabIndex = 18;
			// 
			// panel10
			// 
			this.panel10.Controls.Add(this.label15);
			this.panel10.Controls.Add(this.columns);
			this.panel10.Location = new System.Drawing.Point(8, 64);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(192, 32);
			this.panel10.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 8);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(56, 16);
			this.label15.TabIndex = 0;
			this.label15.Text = "Columns";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// columns
			// 
			this.columns.Location = new System.Drawing.Point(64, 8);
			this.columns.Maximum = new System.Decimal(new int[] {
																	1000000,
																	0,
																	0,
																	0});
			this.columns.Minimum = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.columns.Name = "columns";
			this.columns.TabIndex = 3;
			this.columns.Value = new System.Decimal(new int[] {
																  1,
																  0,
																  0,
																  0});
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.Transparent;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label17.Location = new System.Drawing.Point(0, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(200, 24);
			this.label17.TabIndex = 0;
			this.label17.Text = "Immity grid";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel12
			// 
			this.panel12.Controls.Add(this.label20);
			this.panel12.Controls.Add(this.rows);
			this.panel12.Location = new System.Drawing.Point(8, 24);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(192, 32);
			this.panel12.TabIndex = 0;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(8, 8);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(40, 16);
			this.label20.TabIndex = 0;
			this.label20.Text = "Rows";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// rows
			// 
			this.rows.Location = new System.Drawing.Point(64, 8);
			this.rows.Maximum = new System.Decimal(new int[] {
																 1000000,
																 0,
																 0,
																 0});
			this.rows.Minimum = new System.Decimal(new int[] {
																 1,
																 0,
																 0,
																 0});
			this.rows.Name = "rows";
			this.rows.TabIndex = 5;
			this.rows.Value = new System.Decimal(new int[] {
															   1,
															   0,
															   0,
															   0});
			// 
			// parasitePnl
			// 
			this.parasitePnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.parasitePnl.Controls.Add(this.reproductionPnl);
			this.parasitePnl.Controls.Add(this.movePnl);
			this.parasitePnl.Controls.Add(this.statusPnl);
			this.parasitePnl.Controls.Add(this.immuneParasitePnl);
			this.parasitePnl.Controls.Add(this.parasiteLbl);
			this.parasitePnl.Location = new System.Drawing.Point(248, 40);
			this.parasitePnl.Name = "parasitePnl";
			this.parasitePnl.Size = new System.Drawing.Size(256, 480);
			this.parasitePnl.TabIndex = 1;
			// 
			// reproductionPnl
			// 
			this.reproductionPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.reproductionPnl.Controls.Add(this.ReproductionLbl);
			this.reproductionPnl.Controls.Add(this.panel21);
			this.reproductionPnl.Location = new System.Drawing.Point(8, 248);
			this.reproductionPnl.Name = "reproductionPnl";
			this.reproductionPnl.Size = new System.Drawing.Size(232, 72);
			this.reproductionPnl.TabIndex = 17;
			// 
			// ReproductionLbl
			// 
			this.ReproductionLbl.BackColor = System.Drawing.Color.Transparent;
			this.ReproductionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ReproductionLbl.Location = new System.Drawing.Point(0, 0);
			this.ReproductionLbl.Name = "ReproductionLbl";
			this.ReproductionLbl.Size = new System.Drawing.Size(224, 24);
			this.ReproductionLbl.TabIndex = 0;
			this.ReproductionLbl.Text = "Reproduction";
			this.ReproductionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel21
			// 
			this.panel21.Controls.Add(this.label21);
			this.panel21.Controls.Add(this.resReqired);
			this.panel21.Location = new System.Drawing.Point(8, 24);
			this.panel21.Name = "panel21";
			this.panel21.Size = new System.Drawing.Size(216, 32);
			this.panel21.TabIndex = 0;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 8);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(128, 16);
			this.label21.TabIndex = 0;
			this.label21.Text = "Resource requirements";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// resReqired
			// 
			this.resReqired.DecimalPlaces = 4;
			this.resReqired.Increment = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 65536});
			this.resReqired.Location = new System.Drawing.Point(136, 8);
			this.resReqired.Maximum = new System.Decimal(new int[] {
																	   1000000,
																	   0,
																	   0,
																	   0});
			this.resReqired.Name = "resReqired";
			this.resReqired.Size = new System.Drawing.Size(72, 20);
			this.resReqired.TabIndex = 10;
			// 
			// movePnl
			// 
			this.movePnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.movePnl.Controls.Add(this.moveLbl);
			this.movePnl.Controls.Add(this.panel11);
			this.movePnl.Location = new System.Drawing.Point(8, 32);
			this.movePnl.Name = "movePnl";
			this.movePnl.Size = new System.Drawing.Size(232, 72);
			this.movePnl.TabIndex = 16;
			// 
			// moveLbl
			// 
			this.moveLbl.BackColor = System.Drawing.Color.Transparent;
			this.moveLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.moveLbl.Location = new System.Drawing.Point(0, 0);
			this.moveLbl.Name = "moveLbl";
			this.moveLbl.Size = new System.Drawing.Size(224, 24);
			this.moveLbl.TabIndex = 0;
			this.moveLbl.Text = "Movement";
			this.moveLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel11
			// 
			this.panel11.Controls.Add(this.label10);
			this.panel11.Controls.Add(this.moveDist);
			this.panel11.Location = new System.Drawing.Point(8, 24);
			this.panel11.Name = "panel11";
			this.panel11.Size = new System.Drawing.Size(216, 32);
			this.panel11.TabIndex = 0;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(104, 16);
			this.label10.TabIndex = 0;
			this.label10.Text = "Distance (linear)";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// moveDist
			// 
			this.moveDist.DecimalPlaces = 4;
			this.moveDist.Increment = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   65536});
			this.moveDist.Location = new System.Drawing.Point(120, 8);
			this.moveDist.Maximum = new System.Decimal(new int[] {
																	 1000000,
																	 0,
																	 0,
																	 0});
			this.moveDist.Name = "moveDist";
			this.moveDist.Size = new System.Drawing.Size(88, 20);
			this.moveDist.TabIndex = 8;
			this.moveDist.Validating += new System.ComponentModel.CancelEventHandler(this.moveDist_Validating);
			// 
			// statusPnl
			// 
			this.statusPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.statusPnl.Controls.Add(this.panel18);
			this.statusPnl.Controls.Add(this.statusLbl);
			this.statusPnl.Controls.Add(this.panel15);
			this.statusPnl.Location = new System.Drawing.Point(8, 360);
			this.statusPnl.Name = "statusPnl";
			this.statusPnl.Size = new System.Drawing.Size(232, 112);
			this.statusPnl.TabIndex = 15;
			// 
			// panel18
			// 
			this.panel18.Controls.Add(this.label19);
			this.panel18.Controls.Add(this.resources);
			this.panel18.Location = new System.Drawing.Point(8, 64);
			this.panel18.Name = "panel18";
			this.panel18.Size = new System.Drawing.Size(216, 32);
			this.panel18.TabIndex = 0;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(8, 8);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(64, 16);
			this.label19.TabIndex = 0;
			this.label19.Text = "Resources";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// resources
			// 
			this.resources.DecimalPlaces = 4;
			this.resources.Increment = new System.Decimal(new int[] {
																		1,
																		0,
																		0,
																		65536});
			this.resources.Location = new System.Drawing.Point(88, 8);
			this.resources.Maximum = new System.Decimal(new int[] {
																	  1000000,
																	  0,
																	  0,
																	  0});
			this.resources.Name = "resources";
			this.resources.TabIndex = 12;
			// 
			// statusLbl
			// 
			this.statusLbl.BackColor = System.Drawing.Color.Transparent;
			this.statusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.statusLbl.Location = new System.Drawing.Point(0, 0);
			this.statusLbl.Name = "statusLbl";
			this.statusLbl.Size = new System.Drawing.Size(224, 24);
			this.statusLbl.TabIndex = 0;
			this.statusLbl.Text = "Initial status";
			this.statusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel15
			// 
			this.panel15.Controls.Add(this.label13);
			this.panel15.Controls.Add(this.constitution);
			this.panel15.Location = new System.Drawing.Point(8, 24);
			this.panel15.Name = "panel15";
			this.panel15.Size = new System.Drawing.Size(216, 32);
			this.panel15.TabIndex = 0;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 8);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 0;
			this.label13.Text = "Constitution";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// constitution
			// 
			this.constitution.DecimalPlaces = 4;
			this.constitution.Increment = new System.Decimal(new int[] {
																		   1,
																		   0,
																		   0,
																		   65536});
			this.constitution.Location = new System.Drawing.Point(88, 8);
			this.constitution.Maximum = new System.Decimal(new int[] {
																		 1000000,
																		 0,
																		 0,
																		 0});
			this.constitution.Minimum = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 0});
			this.constitution.Name = "constitution";
			this.constitution.TabIndex = 11;
			this.constitution.Value = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   0});
			// 
			// immuneParasitePnl
			// 
			this.immuneParasitePnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.immuneParasitePnl.Controls.Add(this.immuneParasiteLbl);
			this.immuneParasitePnl.Controls.Add(this.panel17);
			this.immuneParasitePnl.Location = new System.Drawing.Point(8, 136);
			this.immuneParasitePnl.Name = "immuneParasitePnl";
			this.immuneParasitePnl.Size = new System.Drawing.Size(232, 72);
			this.immuneParasitePnl.TabIndex = 16;
			// 
			// immuneParasiteLbl
			// 
			this.immuneParasiteLbl.BackColor = System.Drawing.Color.Transparent;
			this.immuneParasiteLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.immuneParasiteLbl.Location = new System.Drawing.Point(0, 0);
			this.immuneParasiteLbl.Name = "immuneParasiteLbl";
			this.immuneParasiteLbl.Size = new System.Drawing.Size(224, 24);
			this.immuneParasiteLbl.TabIndex = 0;
			this.immuneParasiteLbl.Text = "Immune response";
			this.immuneParasiteLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel17
			// 
			this.panel17.Controls.Add(this.label16);
			this.panel17.Controls.Add(this.damage);
			this.panel17.Location = new System.Drawing.Point(8, 24);
			this.panel17.Name = "panel17";
			this.panel17.Size = new System.Drawing.Size(216, 32);
			this.panel17.TabIndex = 0;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 8);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(96, 16);
			this.label16.TabIndex = 0;
			this.label16.Text = "Damage taken";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// damage
			// 
			this.damage.DecimalPlaces = 4;
			this.damage.Increment = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 65536});
			this.damage.Location = new System.Drawing.Point(120, 8);
			this.damage.Maximum = new System.Decimal(new int[] {
																   1000000,
																   0,
																   0,
																   0});
			this.damage.Name = "damage";
			this.damage.Size = new System.Drawing.Size(88, 20);
			this.damage.TabIndex = 9;
			// 
			// parasiteLbl
			// 
			this.parasiteLbl.BackColor = System.Drawing.Color.Transparent;
			this.parasiteLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.parasiteLbl.Location = new System.Drawing.Point(0, 0);
			this.parasiteLbl.Name = "parasiteLbl";
			this.parasiteLbl.Size = new System.Drawing.Size(248, 24);
			this.parasiteLbl.TabIndex = 0;
			this.parasiteLbl.Text = "Parasite";
			this.parasiteLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// simPnl
			// 
			this.simPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.simPnl.Controls.Add(this.seedPnl);
			this.simPnl.Controls.Add(this.panel25);
			this.simPnl.Controls.Add(this.panel22);
			this.simPnl.Controls.Add(this.panel23);
			this.simPnl.Controls.Add(this.simLbl);
			this.simPnl.Location = new System.Drawing.Point(8, 528);
			this.simPnl.Name = "simPnl";
			this.simPnl.Size = new System.Drawing.Size(496, 104);
			this.simPnl.TabIndex = 2;
			// 
			// seedPnl
			// 
			this.seedPnl.Controls.Add(this.checkBox1);
			this.seedPnl.Controls.Add(this.label3);
			this.seedPnl.Controls.Add(this.seed);
			this.seedPnl.Location = new System.Drawing.Point(248, 64);
			this.seedPnl.Name = "seedPnl";
			this.seedPnl.Size = new System.Drawing.Size(232, 32);
			this.seedPnl.TabIndex = 1;
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(104, 8);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(24, 24);
			this.checkBox1.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Seed";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// seed
			// 
			this.seed.Location = new System.Drawing.Point(144, 8);
			this.seed.Maximum = new System.Decimal(new int[] {
																 1000000,
																 0,
																 0,
																 0});
			this.seed.Name = "seed";
			this.seed.Size = new System.Drawing.Size(75, 20);
			this.seed.TabIndex = 17;
			// 
			// panel25
			// 
			this.panel25.Controls.Add(this.label26);
			this.panel25.Controls.Add(this.initParasites);
			this.panel25.Location = new System.Drawing.Point(8, 64);
			this.panel25.Name = "panel25";
			this.panel25.Size = new System.Drawing.Size(232, 32);
			this.panel25.TabIndex = 0;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(8, 8);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(144, 16);
			this.label26.TabIndex = 0;
			this.label26.Text = "Initial quantity of parasites";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// initParasites
			// 
			this.initParasites.Location = new System.Drawing.Point(152, 8);
			this.initParasites.Maximum = new System.Decimal(new int[] {
																		  1000000,
																		  0,
																		  0,
																		  0});
			this.initParasites.Minimum = new System.Decimal(new int[] {
																		  1,
																		  0,
																		  0,
																		  0});
			this.initParasites.Name = "initParasites";
			this.initParasites.Size = new System.Drawing.Size(72, 20);
			this.initParasites.TabIndex = 15;
			this.initParasites.Value = new System.Decimal(new int[] {
																		1,
																		0,
																		0,
																		0});
			// 
			// panel22
			// 
			this.panel22.Controls.Add(this.label22);
			this.panel22.Controls.Add(this.duration);
			this.panel22.Location = new System.Drawing.Point(248, 24);
			this.panel22.Name = "panel22";
			this.panel22.Size = new System.Drawing.Size(232, 32);
			this.panel22.TabIndex = 0;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 8);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(88, 16);
			this.label22.TabIndex = 0;
			this.label22.Text = "Duration";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// duration
			// 
			this.duration.Location = new System.Drawing.Point(104, 8);
			this.duration.Maximum = new System.Decimal(new int[] {
																	 1000000,
																	 0,
																	 0,
																	 0});
			this.duration.Minimum = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 0});
			this.duration.Name = "duration";
			this.duration.TabIndex = 14;
			this.duration.Value = new System.Decimal(new int[] {
																   1,
																   0,
																   0,
																   0});
			// 
			// panel23
			// 
			this.panel23.Controls.Add(this.label24);
			this.panel23.Controls.Add(this.repetitions);
			this.panel23.Location = new System.Drawing.Point(8, 24);
			this.panel23.Name = "panel23";
			this.panel23.Size = new System.Drawing.Size(232, 32);
			this.panel23.TabIndex = 0;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(8, 8);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(80, 16);
			this.label24.TabIndex = 0;
			this.label24.Text = "Repetitions";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// repetitions
			// 
			this.repetitions.Location = new System.Drawing.Point(104, 8);
			this.repetitions.Maximum = new System.Decimal(new int[] {
																		1000000,
																		0,
																		0,
																		0});
			this.repetitions.Minimum = new System.Decimal(new int[] {
																		1,
																		0,
																		0,
																		0});
			this.repetitions.Name = "repetitions";
			this.repetitions.TabIndex = 13;
			this.repetitions.Value = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			// 
			// simLbl
			// 
			this.simLbl.BackColor = System.Drawing.Color.Transparent;
			this.simLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.simLbl.Location = new System.Drawing.Point(0, 0);
			this.simLbl.Name = "simLbl";
			this.simLbl.Size = new System.Drawing.Size(488, 24);
			this.simLbl.TabIndex = 0;
			this.simLbl.Text = "Simulation";
			this.simLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// titleLbl
			// 
			this.titleLbl.BackColor = System.Drawing.Color.Transparent;
			this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.titleLbl.Location = new System.Drawing.Point(0, 0);
			this.titleLbl.Name = "titleLbl";
			this.titleLbl.Size = new System.Drawing.Size(504, 32);
			this.titleLbl.TabIndex = 17;
			this.titleLbl.Text = "Host-Parasite Dynamics Simulation";
			this.titleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// simSetup
			// 
			this.AcceptButton = this.runBtn;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 678);
			this.Controls.Add(this.titleLbl);
			this.Controls.Add(this.simPnl);
			this.Controls.Add(this.parasitePnl);
			this.Controls.Add(this.hostPnl);
			this.Controls.Add(this.runBtn);
			this.MaximizeBox = false;
			this.Name = "simSetup";
			this.Text = "Simulation Setup";
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
			this.hostPnl.ResumeLayout(false);
			this.resPnl.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.minResources)).EndInit();
			this.panel7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.maxResources)).EndInit();
			this.immunePnl.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.compitence)).EndInit();
			this.panel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.decay)).EndInit();
			this.dimensionsPnl.ResumeLayout(false);
			this.gridPnl.ResumeLayout(false);
			this.panel10.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.columns)).EndInit();
			this.panel12.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rows)).EndInit();
			this.parasitePnl.ResumeLayout(false);
			this.reproductionPnl.ResumeLayout(false);
			this.panel21.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.resReqired)).EndInit();
			this.movePnl.ResumeLayout(false);
			this.panel11.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.moveDist)).EndInit();
			this.statusPnl.ResumeLayout(false);
			this.panel18.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.resources)).EndInit();
			this.panel15.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.constitution)).EndInit();
			this.immuneParasitePnl.ResumeLayout(false);
			this.panel17.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.damage)).EndInit();
			this.simPnl.ResumeLayout(false);
			this.seedPnl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.seed)).EndInit();
			this.panel25.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.initParasites)).EndInit();
			this.panel22.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.duration)).EndInit();
			this.panel23.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.repetitions)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void width_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( width.Value <= 0 ){e.Cancel = true; }
		}

		private void height_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( height.Value <= 0 ){e.Cancel = true; }
		}

		private void resources_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( minResources.Value > maxResources.Value ){e.Cancel = true; }
		}

		private void moveDist_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( moveDist.Value > width.Value ){e.Cancel = true; }
		}

	}
}
