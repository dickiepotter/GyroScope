using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace BioSimUI
{

	public class Outputs : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid			dataGrid;
		private System.Windows.Forms.DataGridTableStyle TableSyle;
		private System.Windows.Forms.ToolBar			toolBar;
		private System.Windows.Forms.ToolBarButton		Paused;
		private System.Windows.Forms.ToolBarButton		Stop;
		private System.Windows.Forms.ToolBarButton		Export;
		private System.Windows.Forms.ImageList			imageList;
		private System.ComponentModel.IContainer		components;
		private System.Windows.Forms.SaveFileDialog		saveFileDialog;
		private System.Windows.Forms.StatusBar			statusBar;
		private System.Windows.Forms.StatusBarPanel		timeStep;
		private System.Windows.Forms.StatusBarPanel		rows;
		private System.Windows.Forms.StatusBarPanel		execution;
		private System.Windows.Forms.Label				label1;

		private DataCollection data;
		private Model sim;

		public Outputs(Model sim, int simulationDuration)
		{
			InitializeComponent();

			sim.Status.ExecutionEvent += new Execution.ExecutionEventHandler(executionDataHandler);

			// Do not use events to collect data
            // data = new DataCollection(sim, false);
			// Use events to collect data
			 data = new DataCollection(sim, true);

			Export.Enabled = false;
			
			this.Show();
			
			data.Data.RowChanged += new DataRowChangeEventHandler(dataChangeHandler);	

			this.sim = sim;

			// Run creates a new thread on which to run the simulation
			// Do not use events to collect data
			//sim.Run(simulationDuration, data);
			// Use events to collect data
			sim.Run(simulationDuration);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Outputs));
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.TableSyle = new System.Windows.Forms.DataGridTableStyle();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.Export = new System.Windows.Forms.ToolBarButton();
			this.Paused = new System.Windows.Forms.ToolBarButton();
			this.Stop = new System.Windows.Forms.ToolBarButton();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.timeStep = new System.Windows.Forms.StatusBarPanel();
			this.rows = new System.Windows.Forms.StatusBarPanel();
			this.execution = new System.Windows.Forms.StatusBarPanel();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timeStep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rows)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.execution)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid
			// 
			this.dataGrid.AllowNavigation = false;
			this.dataGrid.AllowSorting = false;
			this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid.CaptionText = "Results";
			this.dataGrid.CausesValidation = false;
			this.dataGrid.DataMember = "";
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(0, 48);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ParentRowsVisible = false;
			this.dataGrid.ReadOnly = true;
			this.dataGrid.RowHeadersVisible = false;
			this.dataGrid.Size = new System.Drawing.Size(584, 584);
			this.dataGrid.TabIndex = 0;
			this.dataGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								 this.TableSyle});
			// 
			// TableSyle
			// 
			this.TableSyle.DataGrid = this.dataGrid;
			this.TableSyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.TableSyle.MappingName = "";
			// 
			// toolBar
			// 
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.Export,
																					   this.Paused,
																					   this.Stop});
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageList;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(584, 44);
			this.toolBar.TabIndex = 1;
			this.toolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// Export
			// 
			this.Export.ImageIndex = 0;
			this.Export.Text = "Export";
			// 
			// Paused
			// 
			this.Paused.ImageIndex = 1;
			this.Paused.Text = "Pause";
			// 
			// Stop
			// 
			this.Stop.ImageIndex = 2;
			this.Stop.Text = "Stop";
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Comma delimeted files|*.csv";
			// 
			// statusBar
			// 
			this.statusBar.CausesValidation = false;
			this.statusBar.Location = new System.Drawing.Point(0, 638);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.timeStep,
																						 this.rows,
																						 this.execution});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(584, 24);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "statusBar";
			// 
			// timeStep
			// 
			this.timeStep.Icon = ((System.Drawing.Icon)(resources.GetObject("timeStep.Icon")));
			this.timeStep.Text = "TimeStep";
			this.timeStep.Width = 150;
			// 
			// rows
			// 
			this.rows.Icon = ((System.Drawing.Icon)(resources.GetObject("rows.Icon")));
			this.rows.Text = "Rows";
			this.rows.Width = 150;
			// 
			// execution
			// 
			this.execution.Icon = ((System.Drawing.Icon)(resources.GetObject("execution.Icon")));
			this.execution.Text = "Stopped";
			this.execution.Width = 150;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(264, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(328, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please wait whilst pausing and stopping execution, the effect will not take effec" +
				"t until the current time step has completed ";
			// 
			// Outputs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScrollMargin = new System.Drawing.Size(5, 5);
			this.ClientSize = new System.Drawing.Size(584, 662);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.toolBar);
			this.Controls.Add(this.dataGrid);
			this.Name = "Outputs";
			this.Text = "Outputs";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timeStep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rows)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.execution)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if( sim.Status != null )
			{
				if( e.Button.Text == "Pause" || e.Button.Text == "Continue")
				{
					if( sim.Status.Status == Execution.State.PAUSED )
					{
						this.Paused.ImageIndex = 1;
						this.Paused.Text = "Pause";
						sim.Status.Run();
						
					}
					else if( sim.Status.Status == Execution.State.RUNNING )
					{
						this.Paused.ImageIndex = 3;
						this.Paused.Text = "Continue";
						sim.Status.Pause();
					}
				}
				else if( e.Button.Text == "Stop" )
				{
					if(sim.Status.Status == Execution.State.PAUSED)
					{
						sim.Status.ImmediateStop();
						Paused.Enabled = false;
						Stop.Enabled = false;
					}
					else if( sim.Status.Status == Execution.State.RUNNING )
					{
						sim.Status.Stop();
						Paused.Enabled = false;
						Stop.Enabled = false;
					}
				}
			}

			if( e.Button.Text == "Export" )
			{
				Export.Enabled = false;
				

				try
				{
					this.saveFileDialog.Title = "Export all data to .CVS file";
					this.saveFileDialog.ShowDialog();
					data.Save( saveFileDialog.OpenFile() );
					
					this.saveFileDialog.Title = "Export summery data to .CVS file";
					this.saveFileDialog.ShowDialog();
					data.SaveSummary( saveFileDialog.OpenFile() );
				}
				catch( Exception )
				{
					// Don nothing, dont try to save!
				}
				finally
				{
					Export.Enabled = true;
				}
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				sim.ImmediateHalt();
				sim = null;

				if(components != null)
				{
					components.Dispose();
				}
				
			}
			base.Dispose( disposing );
		}

		delegate void stateParameterDelegate( Execution.State value );

		private void executionDataHandler(object sender, Execution.ExecutionEventArgs e)
		{
			BeginInvoke(new stateParameterDelegate(updateState), new object[]{e.CurrentState});
		}

		private void updateState(Execution.State state)
		{
			execution.Text = state.ToString();

			if( state == Execution.State.STOPPED || state == Execution.State.PAUSED )
			{
				dataGrid.SetDataBinding(data.Data, "");
				Export.Enabled	=  true;
				
				if( state == Execution.State.STOPPED )
				{
					Stop.Enabled	= false;
					Paused.Enabled	= false;
				}
			}
			else
			{
				dataGrid.SetDataBinding(null, "");
				Export.Enabled = false;
			}
		}

		private void dataChangeHandler(object sender, DataRowChangeEventArgs e)
		{
			BeginInvoke(new MethodInvoker(updateRows));
			BeginInvoke(new MethodInvoker(updateTime));
		}

		private void updateRows()
		{
			rows.Text = data.Rows + " rows";
		}

		private void updateTime()
		{
			if(data.Rows >0)
			{
				timeStep.Text = "TimeStep " + data.Data.Rows[data.Rows -1]["Time"];
			}
		}
	}
}
