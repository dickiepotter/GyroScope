#region Imports 

using System;
using System.Collections;
using System.Threading;

#endregion

/// <summary>
/// An immutable object representing a host-parasite dynamics model for simulation.
/// Encapsulates an execution thread used to allow parameter changes during execution of the simulation loop
/// </summary>
public class Model 
{
	#region Constructor(s)

	/// <summary>
	/// Constructor taking a seed for random events
	/// </summary>
	/// <param name="repetitions">The number of identical simulations to run to average results</param>
	/// <param name="factory">An object which creates valid simulations on request</param>
	/// <param name="seed">A seed to be used on all random events</param>
	public Model( int repetitions, SimFactory factory, int seed )
	{
		simulations = new Simulation[repetitions];

		for( int i=0; i< simulations.Length; i++)
		{
			simulations[i] = factory.CreateSim(seed, this);
		}

		executionStatus.ExecutionEvent += new Execution.ExecutionEventHandler(executionEventHandler);
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="repetitions">The number of identical simulations to run to average results</param>
	/// <param name="factory">An object which creates valid simulations on request</param>
	public Model( int repetitions, SimFactory factory )
	{
		simulations = new Simulation[repetitions];

		for( int i=0; i< simulations.Length; i++)
		{
			simulations[i] = factory.CreateSim(-1, this);
		}

		executionStatus.ExecutionEvent += new Execution.ExecutionEventHandler(executionEventHandler);
	}

	#endregion

	#region Hidden variables

	/// <summary>
	/// A thread on which to execute the simulations.
	/// </summary>
	private Thread executionThread = null;

	/// <summary>
	/// An execution status recorder and notification object
	/// </summary>
	private Execution executionStatus = new Execution();

	/// <summary>
	/// An array of references to simulation objects, each should be near identical except for the random elements
	/// </summary>
	private Simulation[] simulations;

	#endregion

	#region Simulate

	private bool throwSimulationEvents = true;
	private ISimulationOutput outputTo;

	/// <summary>
	/// A method to run the model simulations on a separate thread and identify execution state changes.
	/// Only one thread per model instance may be permitted to prevent synchronization issues.
	/// </summary>
	/// <param name="duration">The number of timesteps for which the simulations are executed</param>
	private void run(int duration)
	{
			//Prevent running thread from being overwritten
			if( (executionThread != null && executionThread.ThreadState == ThreadState.Running) ) 
			{ return;}
		
			// Create the thread for the simulation
			executionThread = new Thread(new ThreadStart(simulate));
			executionThread.Name = "Simulation thread";
			//executionThread.IsBackground = true;
			// Set execution state to running
			executionStatus.Status = Execution.State.RUNNING;

			this.duration = duration;

			executionThread.Start();
	}

	public void Run(int duration, ISimulationOutput outputTo)
	{
		throwSimulationEvents = false;
		this.outputTo = outputTo;
		run(duration);
	}

	public void Run(int duration)
	{
		throwSimulationEvents = true;
		run(duration);
	}

	#region Simulation variables cache for paused execution

	/// <summary>
	/// The current timestep being processed by the simulate method
	/// </summary>
	private int timeStep =1;

	/// <summary>
	/// The duration for which the simulation will run
	/// </summary>
	private int duration = 0;

	#endregion
	
	/// <summary>
	/// Run the model simulations and with allowances for paused execution.
	/// </summary>
	/// <remarks>
	/// The checking of a paused execution state at the end of a time step has a negligible effect on execution speed.
	/// It does has a large impact on the user, who has to wait several seconds for their action to be reacted to.
	/// </remarks>
	/// TODO Find a better polling method for paused state reaction, so's not to inconvenience the user.
	private void simulate()
	{
		// For the duration
		while( timeStep < duration +1 )
		{
			bool allDead = false; 

			foreach( Simulation simulation in simulations )
			{
				simulation.RunOnce(timeStep);

				// Check if this simulation contains any parasites
				if( simulation.ParasiteCount <= 0 )
				{ allDead = true; }
				else
				{ allDead = false; }
			}

			timeStep ++;

			// Only check for execution status changes at the end of a single timestep
			// End the simulation if there are no further parasites or a stop command has been issued
			// Running the simulation will cause the simulation to begin again from the start
			if( allDead )
			{
				//Exit the loop activating the reset code
				break;
			}
			else if( executionStatus.Status == Execution.State.STOPPING )
			{
				//Exit the loop activating the reset code
				break;
			}
			else if( executionStatus.Status == Execution.State.PAUSING )
			{ 
				executionStatus.Status = Execution.State.PAUSED;
				// Avoid reset code so that the current state of execution remains available
				return; 
			}
			// Otherwise, contine to run (do nothing, already in the loop)
		}

		// Preform a reset on exiting the loop
		resetSim();
		executionStatus.ImmediateStop();
		// Thread terminates
	}

	/// <summary>
	/// Resets the simulation variables when execution stops.
	/// </summary>
	private void resetSim()
	{
		timeStep =1;
		duration =0;
	}

	/// <summary>
	/// Cleanly aborts running simulations.
	/// </summary>
	public void ImmediateHalt()
	{
		executionThread.Abort();
		executionStatus = new Execution();
		resetSim();
	}
	
	#region Execution State

	/// <summary>
	/// The event handler that is notified of ExecutionEvents.
	/// </summary>
	/// <param name="sender">The object which sent the event.</param>
	/// <param name="e">A description of the changes made to the execution state.</param>
	private void executionEventHandler( object sender, Execution.ExecutionEventArgs e )
	{
		// If state changed from paused to running then ...
		if( executionThread != null )
		{
			if( e.PreviousState == Execution.State.PAUSED )
			{
				if( e.CurrentState == Execution.State.RUNNING )
				{
					// Recover and continue from paused position
					this.Run(duration);
				}
				else if( e.CurrentState == Execution.State.STOPPED )
				{
					resetSim();
				}
			}
		}
	}

	/// <summary>
	/// Accessor for the current execution status
	/// </summary>
	public Execution Status
	{
		get{ return executionStatus; }
	}

	#endregion

	#region Parasite changed event

	/// <summary>
	/// An event to notify interested parties that a parasite has been changed / updated
	/// </summary>
	public event ParasiteEventHandler ParasiteEvent;   
	
	/// <summary>
	/// An delegate onto which methods can be attached to register their interest in parasite changes
	/// </summary>
	public delegate void ParasiteEventHandler(object sender, ParasiteEventArgs e);
	
	/// <summary>
	/// A class to hold a description of the changes that have occurred to a parasite.
	/// An instance of this class will be passed by the ParasiteEvent.
	/// </summary>
	/// <remarks>
	/// This class is immutable.
	/// This class is a subclass of Model, it seems appropriate to keep this information with the parent.
	/// </remarks>
	public class ParasiteEventArgs : EventArgs 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="parasite">The parasite that has been changed</param>
		/// <param name="timestep">The timestep in which the parasite was changed</param>
		/// <param name="host">The host on which the parasite was residing</param>
		public ParasiteEventArgs( Parasite parasite, int timestep, Host host )
		{
			this.parasite = parasite;
			this.timestep = timestep;
			this.host	  = host;
		}

		/// <summary>
		/// A variable to hold the parasite that has been changed.
		/// </summary>
		private Parasite parasite;
		/// <summary>
		/// A property to access he parasite that has been changed.
		/// </summary>
		public Parasite ParasiteChanged
		{
			get{ return parasite; }
		}
		
		/// <summary>
		/// A variable to hold the timestep in which the parasite was changed.
		/// </summary>
		private int timestep;
		/// <summary>
		/// A property to access the timestep in which the parasite was changed.
		/// </summary>
		public int Time
		{
			get{ return timestep; }
		}

		/// <summary>
		/// A variable to hold the host on which the parasite was residing.
		/// </summary>
		private Host host;
		/// <summary>
		/// A property to access the host on which the parasite was residing.
		/// </summary>
		public Host HostAttachedTo
		{
			get{ return host; }
		}
	}

	/// <summary>
	/// A method that can be used to notify instances of this class about parasite changes.
	/// Fires a parasiteEvent.
	/// </summary>
	/// <param name="parasite">The parasite that has been changed</param>
	/// <param name="timestep">The timestep in which the parasite was changed</param>
	/// <param name="host">The host on which the parasite was residing</param>
	/// <event cref='ParasiteEvent'>
	/// Fired with the parameters passed into the method
	/// </event>
	/// <remarks>
	/// This method should NOT be needed, it simply allows classes other than this call the ParasiteEvent.
	/// Refactoring should be able to remove this method entirely.
	/// The need for this method suggests that the simulation classes that this class holds references too should not be separated as they are.
	/// </remarks>
	public void NotifyUpdate( Parasite parasite, int timestep, Host host )
	{
		if( this.outputTo != null )
		{
			this.outputTo.Add
			( 
				host.ID,
                timestep, 
				parasite.ID, 
				parasite.Location.X, 
				parasite.Location.Y,
				parasite.Resources,
				parasite.Constitution
			);
		}

		if( this.throwSimulationEvents = true )
		{
			try
			{
				ParasiteEvent( this, new ParasiteEventArgs( parasite, timestep, host ) );
			}
			catch( NullReferenceException ){/* No one to pick up the event */ }
		}
	}

	#endregion

	#endregion 

}
