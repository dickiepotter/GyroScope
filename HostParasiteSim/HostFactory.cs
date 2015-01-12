#region Imports

using System;
using System.Collections;

#endregion

/// <summary>
/// Object which creates Host instances
/// </summary>
/// <remarks>
/// Provides future abstraction capabilities if used with the abstract factory pattern.
/// Currently helps decouple the simulation and the host objects.
/// </remarks>
public class HostFactory
{
	#region constructor(s)

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="minimalResourceGain">The initial smallest resource gain that a visitor will take (if damage is inflicted)</param>
	/// <param name="normalResourceGain">The initial largest / normal resource gain that a visitor will take</param>
	/// <param name="immunityDamage">The initial damage that will be inflicted on visitors to an immuno-responsive site</param>
	/// <param name="immunoDecay">The initial percent of the response weighting that will be removed each timestep</param>
	/// <param name="immunoCompitence">The initial threshold that must be reached by the response weighting for each site before the site does damage to visitors (i.e. is immuno-responsive)</param>
	/// <param name="rows">The number of rows that the host will be divided into, to create immuno-areas</param>
	/// <param name="columns">The number of columns that the host will be divided into, to create immuno-areas</param>

	public HostFactory
	( 
		float minimalResourceGain, 
		float normalResourceGain, 
		float immunityDamage, 
		float immunoDecay,
		float immunoCompitence,
		int	  rows,
		int	  columns
	)
	{
		this.MinimalResourceGain	= minimalResourceGain;
		this.NormalResourceGain		= normalResourceGain;
		this.ImmunityDamage			= immunityDamage;
		this.ImmunoDecay			= immunoDecay;
		this.ImmunoCompitence		= immunoCompitence;
		this.Rows					= rows;
		this.Columns				= columns;
	}

	#endregion

	#region Initial values for host

	/// <summary>
	/// A variable holding the initial damage that will be inflicted on visitors to an immuno-responsive site
	/// </summary>
	private float immunityDamage;
	/// <summary>
	/// A property to access and mutate the initial damage that will be inflicted on visitors to an immuno-responsive site
	/// </summary>
	public float ImmunityDamage
	{
		get{ return immunityDamage; }
		set{ immunityDamage = value; }
	}

	/// <summary>
	/// A variable holding the initial smallest resource gain that a visitor will take (if damage is inflicted)
	/// </summary>
	private float minimalResourceGain;
	/// <summary>
	/// A property to access and mutate the initial smallest resource gain that a visitor will take
	/// </summary>
	public float MinimalResourceGain
	{
		get{ return minimalResourceGain; }
		set{ minimalResourceGain = value; }
	}

	/// <summary>
	/// A variable holding the initial largest / normal resource gain that a visitor will take
	/// </summary>
	private float normalResourceGain;
	/// <summary>
	/// A property to access and mutate the initial largest / normal resource gain that a visitor will take
	/// </summary>
	public float NormalResourceGain
	{
		get{ return normalResourceGain; }
		set{ normalResourceGain = value; }
	}

	/// <summary>
	/// A variable holding the initial percent of the response weighting that will be removed each timestep
	/// </summary>
	private float immunoDecay;
	/// <summary>
	/// A property to access and mutate the initial percent of the response weighting that will be removed each timestep
	/// </summary>
	public float ImmunoDecay
	{
		get{ return immunoDecay; }
		set{ immunoDecay = value; }
	}

	/// <summary>
	/// A variable holding the initial threshold that must be reached by the response weighting for each site before the site does damage to visitors
	/// </summary>
	private float immunoCompitence;
	/// <summary>
	/// A property to access and mutate the initial threshold that must be reached by the response weighting for each site before the site does damage to visitors
	/// </summary>
	public float ImmunoCompitence
	{
		get{ return immunoCompitence; }
		set{ immunoCompitence = value; }
	}

	/// <summary>
	/// A variable holding the initial initial number of unique immuno-response locations.
	/// </summary>
	private int immunoEffectLocations;
	/// <summary>
	///	A property to access and mutate the initial number of unique immuno-response locations.
	/// </summary>
	public int ImmunoEffectLocations
	{
		get{ return immunoEffectLocations; }
		set{ immunoEffectLocations = value; }
	}

	/// <summary>
	/// A variable incremented to give a unique identifier to each host instance created.
	/// </summary>
	/// <remarks>
	/// This value should not need to de defined explicitly. 
	/// The array in which the simulation instances are stored (Model class) already records this information implicitly as the array index
	/// </remarks>
	private int id;


	/// <summary>
	/// A variable holding the number of rows the host will be divided into.
	/// </summary>
	private int rows;
	/// <summary>
	/// A property to access and mutate the number of rows the host will be divided into.
	/// </summary>
	public int Rows
	{
		get{ return rows; }
		set{ rows = value; }
	}

	/// <summary>
	/// A variable holding the number of columns the host will be divided into.
	/// </summary>
	private int columns;
	/// <summary>
	/// A property to access and mutate the number of columns the host will be divided into.
	/// </summary>
	public int Columns
	{
		get{ return columns; }
		set{ columns = value; }
	}

	#endregion

	/// <summary>
	/// Create Host instance using initial values stored with a unique id
	/// </summary>
	/// <param name="length">The length of the host</param>
	/// <param name="width">The width of the host</param>
	/// <returns>A new Host instance with a unique id</returns>
	public Host CreateHost(float width, float length)
	{
		id ++;
		Grid grid = new Grid( new Rectangle( width, length), columns, rows);
		return new Host(grid, immunityDamage, minimalResourceGain, normalResourceGain, immunoDecay, immunoCompitence, id);
	} 
}
