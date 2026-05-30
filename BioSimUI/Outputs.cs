using System;
using System.Windows.Forms;
using System.Data;


namespace BioSimUI
{

	public class Outputs : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGridView			dataGrid;
		private System.Windows.Forms.ToolStrip				toolBar;
		private System.Windows.Forms.ToolStripButton		exportButton;
		private System.Windows.Forms.ToolStripButton		pauseButton;
		private System.Windows.Forms.ToolStripButton		stopButton;
		private System.ComponentModel.IContainer			components;
		private System.Windows.Forms.SaveFileDialog			saveFileDialog;
		private System.Windows.Forms.StatusStrip			statusBar;
		private System.Windows.Forms.ToolStripStatusLabel	timeStepLabel;
		private System.Windows.Forms.ToolStripStatusLabel	rowsLabel;
		private System.Windows.Forms.ToolStripStatusLabel	executionLabel;
		private System.Windows.Forms.Label					label1;

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

			exportButton.Enabled = false;

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
			this.dataGrid = new System.Windows.Forms.DataGridView();
			this.toolBar = new System.Windows.Forms.ToolStrip();
			this.exportButton = new System.Windows.Forms.ToolStripButton();
			this.pauseButton = new System.Windows.Forms.ToolStripButton();
			this.stopButton = new System.Windows.Forms.ToolStripButton();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.timeStepLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.rowsLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.executionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.toolBar.SuspendLayout();
			this.statusBar.SuspendLayout();
			this.SuspendLayout();
			//
			// dataGrid
			//
			this.dataGrid.AllowUserToAddRows = false;
			this.dataGrid.AllowUserToDeleteRows = false;
			this.dataGrid.AllowUserToOrderColumns = false;
			this.dataGrid.CausesValidation = false;
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ReadOnly = true;
			this.dataGrid.RowHeadersVisible = false;
			this.dataGrid.TabIndex = 0;
			//
			// toolBar
			//
			this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
																					 this.exportButton,
																					 this.pauseButton,
																					 this.stopButton});
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.TabIndex = 1;
			//
			// exportButton
			//
			this.exportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.exportButton.Name = "exportButton";
			this.exportButton.Text = "Export";
			this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
			//
			// pauseButton
			//
			this.pauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.pauseButton.Name = "pauseButton";
			this.pauseButton.Text = "Pause";
			this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
			//
			// stopButton
			//
			this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.stopButton.Name = "stopButton";
			this.stopButton.Text = "Stop";
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			//
			// saveFileDialog
			//
			this.saveFileDialog.Filter = "Comma delimeted files|*.csv";
			//
			// statusBar
			//
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
																					   this.timeStepLabel,
																					   this.rowsLabel,
																					   this.executionLabel});
			this.statusBar.Name = "statusBar";
			this.statusBar.TabIndex = 2;
			//
			// timeStepLabel
			//
			this.timeStepLabel.Name = "timeStepLabel";
			this.timeStepLabel.Text = "TimeStep";
			this.timeStepLabel.AutoSize = false;
			this.timeStepLabel.Width = 150;
			//
			// rowsLabel
			//
			this.rowsLabel.Name = "rowsLabel";
			this.rowsLabel.Text = "Rows";
			this.rowsLabel.AutoSize = false;
			this.rowsLabel.Width = 150;
			//
			// executionLabel
			//
			this.executionLabel.Name = "executionLabel";
			this.executionLabel.Text = "Stopped";
			this.executionLabel.AutoSize = false;
			this.executionLabel.Width = 150;
			//
			// label1
			//
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(584, 32);
			this.label1.TabIndex = 3;
			this.label1.Text = "Please wait whilst pausing and stopping execution, the effect will not take effec" +
				"t until the current time step has completed ";
			//
			// Outputs
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScrollMargin = new System.Drawing.Size(5, 5);
			this.ClientSize = new System.Drawing.Size(584, 662);
			this.Controls.Add(this.dataGrid);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toolBar);
			this.Controls.Add(this.statusBar);
			this.Name = "Outputs";
			this.Text = "Outputs";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.toolBar.ResumeLayout(false);
			this.statusBar.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion


		private void pauseButton_Click(object sender, System.EventArgs e)
		{
			if( sim.Status != null )
			{
				if( sim.Status.Status == Execution.State.PAUSED )
				{
					this.pauseButton.Text = "Pause";
					sim.Status.Run();
				}
				else if( sim.Status.Status == Execution.State.RUNNING )
				{
					this.pauseButton.Text = "Continue";
					sim.Status.Pause();
				}
			}
		}

		private void stopButton_Click(object sender, System.EventArgs e)
		{
			if( sim.Status != null )
			{
				if( sim.Status.Status == Execution.State.PAUSED )
				{
					sim.Status.ImmediateStop();
					pauseButton.Enabled = false;
					stopButton.Enabled = false;
				}
				else if( sim.Status.Status == Execution.State.RUNNING )
				{
					sim.Status.Stop();
					pauseButton.Enabled = false;
					stopButton.Enabled = false;
				}
			}
		}

		private void exportButton_Click(object sender, System.EventArgs e)
		{
			exportButton.Enabled = false;

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
				exportButton.Enabled = true;
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
			executionLabel.Text = state.ToString();

			if( state == Execution.State.STOPPED || state == Execution.State.PAUSED )
			{
				dataGrid.DataSource = data.Data;
				exportButton.Enabled	=  true;

				if( state == Execution.State.STOPPED )
				{
					stopButton.Enabled		= false;
					pauseButton.Enabled		= false;
				}
			}
			else
			{
				dataGrid.DataSource = null;
				exportButton.Enabled = false;
			}
		}

		private void dataChangeHandler(object sender, DataRowChangeEventArgs e)
		{
			BeginInvoke(new MethodInvoker(updateRows));
			BeginInvoke(new MethodInvoker(updateTime));
		}

		private void updateRows()
		{
			rowsLabel.Text = data.Rows + " rows";
		}

		private void updateTime()
		{
			if(data.Rows >0)
			{
				timeStepLabel.Text = "TimeStep " + data.Data.Rows[data.Rows -1]["Time"];
			}
		}
	}
}
