#region Imports

using System;

#endregion

/// <summary>
/// A class to describe and record a state of execution.
/// </summary>
public class Execution
{
	#region Constructor(s)

	/// <summary>
	/// Default constructor, the initial state will be recorded as stopped
	/// </summary>
	public Execution() {}
	
	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="status">The initial state of execution recorded</param>
	/// <remarks>
	/// The initial state does not trigger the state changed event 
	/// as this is deemed to be the default state for this instance
	/// </remarks>
	public Execution( State status )
	{
		this.status = status;
	}

	#endregion

	#region State enumeration

	/// <summary>
	/// An enumeration of recordable states
	/// </summary>
	public enum State
	{
		/// <summary>
		/// Execution in progress
		/// </summary>
		RUNNING,

		/// <summary>
		/// Execution temporarily halted
		/// </summary>
		PAUSED,

		/// <summary>
		/// Execution terminated or has not yet begun
		/// </summary>
		STOPPED,

		/// <summary>
		/// Execution is about to treminate
		/// </summary>
		STOPPING,

		/// <summary>
		/// Execution is about to temporarily halt
		/// </summary>
		PAUSING

	}

	#endregion

	#region Status accessors and mutators

	/// <summary>
	/// A variable to record the execution state that a related model should be in.
	/// </summary>
	private State status = State.STOPPED;
	/// <summary>
	/// A property to access and mutate the execution state that a related model should be in.
	/// </summary>
	/// <remarks>
	/// The mutator for this property should implement a valid state model
	/// e.g. It should not be possible to change from a stopped state of execution to a paused state.
	/// </remarks>
	/// <event cref="ExecutionEvent">
	/// If the new value is not the same as the previous an event is thrown to notify any interested parties that the recorded execution state has changed.
	/// </event>
	public State Status
	{
		get{ return status; }
		set
		{
			switch(value)
			{
				case State.STOPPED:
					if( this.Status != State.STOPPING ) 
						return;
					break;
				case State.STOPPING:
					if( this.Status != State.RUNNING && this.Status != State.PAUSED ) 
						return;
					break;
				case State.RUNNING:
					if( this.Status != State.PAUSED && this.Status != State.STOPPED ) 
						return;
					break;
				case State.PAUSED:
					if( this.Status != State.PAUSING) 
						return;
					break;
				case State.PAUSING:
					if( this.Status != State.RUNNING) 
						return;
					break;
			}
			
			ExecutionEventArgs args = new ExecutionEventArgs( status, value );
			// Change execution status
			// Change the private variable, don't use the set method, will cause infinate loop
			status = value;
			
			// Fire event to notify a state change occurence
			ExecutionEvent( this, args );
		}
	}

	/// <summary>
	/// A method to change the recorded state to STOPPED
	/// </summary>
	/// <remarks>
	/// This uses the Status property mutator to improve code re-use thus the appropriate event is thrown
	/// </remarks>
	public void Stop()
	{
		Status = State.STOPPING;
	}

	/// <summary>
	/// A method to change the recorded state to RUNNING
	/// </summary>
	/// <remarks>
	/// This uses the Status property mutator to improve code re-use thus the appropriate event is thrown
	/// </remarks>
	public void Run()
	{
		Status = State.RUNNING;
	}

	/// <summary>
	/// A method to change the recorded state to PAUSED
	/// </summary>
	/// <remarks>
	/// This uses the Status property mutator to improve code re-use thus the appropriate event is thrown
	/// </remarks>
	public void Pause()
	{
		Status = State.PAUSING;
	}

	/// <summary>
	/// Under some circumstances it may be necessary to halt execution regardless of the current execution state
	/// </summary>
	public void ImmediateStop()
	{
		ExecutionEventArgs args = new ExecutionEventArgs( status, State.STOPPED );

		// Does not invoke the set method
		status = State.STOPPED;
			
		// Fire event to notify a state change occurence
		ExecutionEvent( this, args );
	}

	#endregion

	#region Execution status changed event

	/// <summary>
	/// An event to notify interested parties that the state of execution has changed
	/// </summary>
	public event ExecutionEventHandler ExecutionEvent;   

	/// <summary>
	/// An delegate onto which methods can be attached to register their interest in the state of execution
	/// </summary>
	public delegate void ExecutionEventHandler(object sender, ExecutionEventArgs e);
	
	/// <summary>
	/// A class to hold a description of the changes that have occurred to the execution state.
	/// An instance of this class will be passed by the ExecutionEvent.
	/// </summary>
	/// <remarks>
	/// This class is immutable.
	/// This class is a subclass of Execution, it seems appropriate to keep this information with the parent.
	/// </remarks>
	public class ExecutionEventArgs : EventArgs 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="previous">The state recorded before a change was made.</param>
		/// <param name="current">The newly recoded execution state.</param>
		public ExecutionEventArgs( State previous, State current )
		{
			this.previous = previous;
			this.current = current;
		}

		/// <summary>
		/// A variable to hold the state recorded before a change was made.
		/// </summary>
		private State previous;
		/// <summary>
		/// A property to access the state recorded before a change was made.
		/// </summary>
		public State PreviousState
		{
			get{ return previous; }
		}
		
		/// <summary>
		/// A variable to hold the state recorded after a change was made.
		/// </summary>
		private State current;
		/// <summary>
		/// A property to access the state recorded after a change was made.
		/// </summary>
		public State CurrentState
		{
			get{ return current; }
		}
	}

	#endregion

	#region Execution State Change Event Watcher

	/// <summary>
	/// A flag to notify if this class is watching ExecutionEvents
	/// </summary>
	/// <remarks>
	/// This class generates ExecutionEvents and loops could occur whilst watching events it generates
	/// </remarks>
	/// <remarks>
	/// By default incoming ExecutionChangedEvent's will not be watched
	/// </remarks>
	private bool watchingExecutionEvents = false;

	/// <summary>
	/// The object which is added to the Execution event delegate specifing a method to call when the event is thrown
	/// </summary>
	private ExecutionEventHandler ExecutionEventLinker;

	/// <summary>
	/// A property to access and mutate the flag to notify if this class is watching ExecutionEvents.
	/// The mutator for this property adds and removes event watchers from the ExecutionEvent.
	/// </summary>
	/// TODO The mutator has not been tested
	public bool IsWatchingExecutionEvents
	{
		get{ return watchingExecutionEvents; }
		set
		{
			// If the new value is not the same as the old value ...
			if( value != watchingExecutionEvents )
			{
				if( value )
				{
					ExecutionEventLinker = new ExecutionEventHandler(stateChangeHandler);
					ExecutionEvent += ExecutionEventLinker;
				}
				else
				{
					ExecutionEvent -= ExecutionEventLinker;
				}

				watchingExecutionEvents = value;
			}
		}
	}

	/// <summary>
	/// The event handler that is notified of ExecutionEvents if watched
	/// </summary>
	/// <param name="sender">The object which sent the event</param>
	/// <param name="e">A description of the changes made to the execution state</param>
	private void stateChangeHandler( object sender, ExecutionEventArgs e )
	{
		if( Status != e.CurrentState )
		{
			status = e.CurrentState;
		}
		// No need for event notifying change of execution state if change was in response to recieving notification
	}

	#endregion
}

