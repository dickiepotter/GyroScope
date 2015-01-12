#region Imports

using System;
using System.Collections;

#endregion

/// <summary>
/// This object represents a single simulation storing the host and parasites and running the interactions between them.
/// </summary>
/// <remarks>
/// This class may be better named to as infection, and move out the RunOnce(...) method, the method does not seem to fit logically.
/// </remarks>
public class Simulation
{
	#region Constructor(s)

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="host">The Host object which the parasites will infect</param>
	/// <param name="parasites">The array of parasites which will infect the host</param>
	/// <param name="caller">The object which created this instance.</param>
	/// <remarks>
	/// The caller parameter should not be needed, refactoring should be able to remove the need for it
	/// </remarks>
	public Simulation(Host host, Parasite[] parasites, Model caller)
	{
		this.host		= host;
		this.parasites	= new ArrayList(parasites);
		this.caller		= caller;
	}

	#endregion

	#region Hidden Variables

	/// <summary>
	/// A variable to hold the Host object which the parasites will infect.
	/// </summary>
	private Host host;

	/// <summary>
	/// A variable to hold the array of parasites which will infect the host.
	/// </summary>
	private ArrayList parasites;

	/// <summary>
	/// A variable to hold the object which craeted this instance.
	/// </summary>
	private Model caller;

	#endregion

	#region Properties to access the simulation parameters

	/// <summary>
	/// Accessor for the number of parasites currently in the array.
	/// This will include those that are children of the initial parasites (i.e. when the parent reproduces).
	/// </summary>
	public int ParasiteCount
	{
		get{ return parasites.Count; }
	}

	#endregion

	/// <summary>
	/// Run simulation for a single time step resolving the interactions between host and parasites
	/// </summary>
	/// <param name="timeStep">The timestep for which this simulation is running.</param>
	/// <remarks>
	/// This entire method does not seem to fit, as the need for a parent reference earlier and the timestep parameter suggest.
	/// Refactoring this method to another class may be the answer.
	/// </remarks>
	public void RunOnce(int timeStep) 
	{
		// List of the dead parasites for garbage collection
		ArrayList deaths = new ArrayList();
		ArrayList births = new ArrayList();

		for(int i=0; i<parasites.Count; i++)
		{
			Parasite parasite = (Parasite) parasites[i];
			/*  Effects on parasite:
			*   Add effects from being on a certain position 
			*   If the parasite has died, remove parasite from simulation 
			*   Otherwise:
			*   Reproduce if necessary
			*	Move parasite if necessary
			*/ 
			host.LocationEffect( parasite, timeStep );

			if(! parasite.IsDead )
			{
				if( parasite.CanReproduce ) 
				{ 
					births.Add( parasite.Reproduce() );
				}
				if( parasite.Location.MustMove )
				{
					parasite.Location.Move();
				}	
			}
			else{ deaths.Add(i); }

			// Inform any interested parties that the parasite has been updated
			caller.NotifyUpdate( parasite, timeStep, host );
		}

		//Remove dead parasites for memory efficiency
		// Reverse loop to take parasites from the end of the array first
		for(int i= deaths.Count-1; i>=0; i-- )
		{
			parasites.RemoveAt( (int)deaths[i] );
		}

		//Add new born parasites
		foreach( Parasite born in births )
		{
			parasites.Add(born);
		}
	}

}
