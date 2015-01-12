#region Imports 

using System;

#endregion

/// <summary>
/// Representation of a host (such as a fish) which has an immune response to parasites.
/// The host is the bounds and immuno-response grid on which parasites can move, thus it extends Grid.
/// </summary>
public class Host :Grid
{
	#region Hidden variables

	/// <summary>
	/// An array of variables storing the responsiveness of each location on the host to a parasite infection.
	/// When the values hit a pre-defined immuno-compitence threshold parasites at that location will take damage
	/// </summary>
	private float[] responseWeighting = null;

	/// <summary>
	/// An array of variables storing the latest timestep in which each location on the host was visited.
	/// </summary>
	private int[]	latestVisit = null;

	#endregion

	#region Constructor(s)

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="grid">The grid on which the host is based, uses the grid base's copy constructor</param>
	/// <param name="immunityDamage">The damage that will be inflicted on visitors to an immuno-responsive site</param>
	/// <param name="minimalResourceGain">The smallest resource gain that a visitor will take (if damage is inflicted)</param>
	/// <param name="normalResourceGain">The largest / normal resource gain that a visitor will take</param>
	/// <param name="immunoDecay">The percent of the response weighting that will be removed each timestep</param>
	/// <param name="immunoCompitence">The threshold that must be reached by the response weighting for each site before the site does damage to visitors (i.e. is immuno-responsive)</param>
	/// <param name="id">An identifier for each host instance</param>
	public Host
	(
		Grid  grid, 
		float immunityDamage, 
		float minimalResourceGain, 
		float normalResourceGain, 
		float immunoDecay,
		float immunoCompitence,
		int	  id
	) : base(grid)
	{
		this.minimalResourceGain	= minimalResourceGain;
		this.normalResourceGain		= normalResourceGain;
		this.immunityDamage			= immunityDamage;
		this.immunoCompitence		= immunoCompitence;
		this.immunoDecay			= immunoDecay;
		this.id						= id;

		responseWeighting			= new float[CellCount];
		latestVisit					= new int[CellCount];
	}

	#endregion

	#region Immunity processing

	/// <summary>
	/// The effect of a particular site on a visitor
	/// Response weightings and visiting time is recorded
	/// </summary>
	/// <param name="visitor">A parasite infecting the host</param>
	/// <param name="timestep">The current timestep being processed</param>
	public void LocationEffect(Parasite visitor, int timestep) 
	{
		int onResponseCell		= this.Cell(visitor.Location);
		Immunity immunityState	= IsLocationImune( onResponseCell, timestep );

		//register a visit at current location
		responseWeighting[onResponseCell] +=1;
	
		// if location is immune
		if( immunityState == Immunity.IMMUNE )
		{
			// take damage
			visitor.Constitution		-= immunityDamage;
			// gain minimal resources
			visitor.Resources			+= minimalResourceGain;
			// move from immuneo-compitent location
			visitor.Location.MustMove	= true ;
		}
		// if immune response is just coming into effect
		else if ( immunityState == Immunity.DEVELOPING )
		{
			visitor.Resources			+= normalResourceGain;
			visitor.Location.MustMove	= true ;
		}
		// if location is not immune
		//if (immunityState == Immunity.NOT_IMMUNE )
		else 
		{
			visitor.Resources			+= normalResourceGain;
		}

		// Log the timestep in which the location was visited
		latestVisit[onResponseCell] = timestep;
	}

	/// <summary>
	/// Identifies if a location is immuno-responsive / causes damage and applies response decay
	/// </summary>
	/// <param name="onResponceCell">The grid cell too check</param>
	/// <param name="currentTimeStep">The current timestep being processed</param>
	/// <returns>True if the location is immuno-responsive</returns>
	public Immunity IsLocationImune(int onResponceCell, int currentTimeStep) 
	{
		// if the location has been visited before
		if( latestVisit[onResponceCell] > 0 )
		{ 
			// decay the response weighting by imunoDecay \ per time step since last visit
			int timeSinceLastVisit = currentTimeStep - latestVisit[onResponceCell];
			float decayPerStep = responseWeighting[onResponceCell] * (immunoDecay / 100);
			float decay = decayPerStep * timeSinceLastVisit;

			responseWeighting[onResponceCell] -= decay;

			if(responseWeighting[onResponceCell] < 0 )
			{
				responseWeighting[onResponceCell] = 0;
			}
		}

		if( responseWeighting[onResponceCell] > immunoCompitence )
		{
			return Immunity.IMMUNE;			// yes
		}
		else if( responseWeighting[onResponceCell] == immunoCompitence )
		{
			return Immunity.DEVELOPING;		// just coming into effect
		}
		//else if( responseWeighting[onResponceCell] < responseWeighting[location.PositionIndex] )
		
		return Immunity.NOT_IMMUNE; 		// no
	}

	
	#region Immunity state enumeration

	/// <summary>
	/// An enumeration of the possible immunity states
	/// </summary>
	public enum Immunity
	{
		/// <summary>
		/// The area is immune.
		/// </summary>
		IMMUNE =1,
		/// <summary>
		/// The area will become immune shortly.
		/// </summary>
		DEVELOPING =0,
		/// <summary>
		/// The area is not immune and there is no immediate threat of it becoming immune.
		/// </summary>
		NOT_IMMUNE =-1
	}

	#endregion

	#endregion

	#region Properties to access object parameters

	/// <summary>
	/// A variable to store the damage that will be inflicted on visitors to an immuno-responsive site.
	/// </summary>
	private float immunityDamage;
	/// <summary>
	/// A property to access and mutate the damage that will be inflicted on visitors to an immuno-responsive site.
	/// </summary>
	public float ImmunityDamage
	{
		get{ return immunityDamage; }
		set{ immunityDamage = value; }
	}

	/// <summary>
	/// A variable to store the smallest resource gain that a visitor will take.
	/// </summary>
	private float minimalResourceGain;
	/// <summary>
	/// A property to access and mutate the smallest resource gain that a visitor will take.
	/// </summary>
	public float MinimalResourceGain
	{
		get{ return minimalResourceGain; }
		set{ minimalResourceGain = value; }
	}

	/// <summary>
	/// A variable to store the largest / normal resource gain that a visitor will take.
	/// </summary>
	private float normalResourceGain;
	/// <summary>
	/// A property to access and mutate the largest / normal resource gain that a visitor will take.
	/// </summary>
	public float NormalResourceGain
	{
		get{ return normalResourceGain; }
		set{ normalResourceGain = value; }
	}

	/// <summary>
	/// A variable to store the percent of the response weighting that will be removed each timestep.
	/// </summary>
	private float immunoDecay;
	/// <summary>
	/// A property to access and mutate the percent of the response weighting that will be removed each timestep.
	/// </summary>
	public float ImmunoDecay
	{
		get{ return immunoDecay; }
		set{ immunoDecay = value; }
	}

	/// <summary>
	/// A variable to store the threshold that must be reached by the response weighting for each site before the site does damage to visitors.
	/// </summary>
	private float immunoCompitence;
	/// <summary>
	/// A property to access and mutate the threshold that must be reached by the response weighting for each site before the site does damage to visitors.
	/// </summary>
	public float ImmunoCompitence
	{
		get{ return immunoCompitence; }
	}

	/// <summary>
	/// A variable to store an identifier for each host instance
	/// </summary>
	/// <remarks>
	/// This value should not need to de defined explicitly. 
	/// The array in which the simulation instances are stored (Model class) already records this information implicitly as the array index
	/// </remarks>
	private int id;
	/// <summary>
	/// A property to access an identifier for each host instance
	/// </summary>
	public int ID
	{
		get{ return id; } 
	}

	/// <summary>
	/// Gets the response weighting for a particular location on the host.
	/// </summary>
	/// <param name="positionIndex">The position for which to get the weighting.</param>
	/// <returns>The response weighting</returns>
	public float Response(int positionIndex)
	{
		return responseWeighting[positionIndex];
	}

	/// <summary>
	/// Gets themost recently recorded visit to a location
	/// </summary>
	/// <param name="positionIndex">The location for which to get the latest visit</param>
	/// <returns>The timestep of most recent visit</returns>
	public float LastVisit(int positionIndex)
	{
		return latestVisit[positionIndex];
	}
	#endregion
}
