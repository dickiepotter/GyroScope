#region Imports

using System;
using System.Collections;

#endregion

/// <summary>
/// Object which creates Parasite instances
/// </summary>
/// <remarks>
/// Provided future abstraction capabilities if used with the abstract factory pattern.
/// Currently helps decouple the simulation and the Parasite objects.
/// </remarks>
public class ParasiteFactory
{
	#region constructor(s)

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="constitution">The initial constitution of the parasite</param>
	/// <param name="resources">The initial resources the parasite has</param>
	/// <param name="reproductionReq">The initial resource requirements for reproduction</param>
	public ParasiteFactory(float constitution, float resources, float reproductionReq)
	{
		this.constitution		= constitution;
		this.resources			= resources;
		this.reproductionReq	= reproductionReq;
	}

	#endregion

	#region Initial values for parasites

	/// <summary>
	/// A variable to hold the constitution of the parasite.
	/// </summary>
	private float constitution;
	/// <summary>
	/// The property to access and mutate the constitution of the parasite.
	/// </summary>
	public float Constitution
	{
		get{ return constitution; }
		set{ constitution = value; }
	}

	/// <summary>
	/// A variable to hold the resources the parasite has.
	/// </summary>
	private float resources =0;
	/// <summary>
	/// The property to access and mutate the resources the parasite has.
	/// </summary>
	public float Resources
	{
		get{ return resources; }
		set{ resources = value; }
	}

	/// <summary>
	/// A variable to hold the resource requirements for reproduction.
	/// </summary>
	private float reproductionReq;
	/// <summary>
	/// The property to access and mutate the resource requirements for reproduction.
	/// </summary>
	public float ReproductionReq
	{
		get{ return reproductionReq; }
		set{ reproductionReq = value; }
	}
	
	/// <summary>
	/// A variable incremented to give a unique identifier to each parasite instance created.
	/// </summary>
	/// <remarks>
	/// This value should not need to de defined explicitly. 
	/// The array in which the parasite instances are stored (Simulation class) already records this information implicitly as the array index
	/// </remarks>
	private int id =0;

	#endregion

	/// <summary>
	/// Create Parasite instance using initial values stored with a unique id
	/// </summary>
	/// <param name="position">An object representing the initial position of the parasite on the host.</param>
	/// <returns>New parasite instance with unique id</returns>
	public Parasite CreateParasite(Position position)
	{
		id ++;
		return new Parasite(position, constitution, resources, reproductionReq, this, id);
	}
}

