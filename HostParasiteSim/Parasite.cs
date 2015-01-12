#region Imports 

using System;

#endregion

/// <summary>
/// Representation of a parasite
/// </summary>
public class Parasite 
{

	#region Constructor(s)

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="position">An object representing the position of the parasite on the host</param>
	/// <param name="constitution">The initial constitution of the parasite </param>
	/// <param name="resources">The initial resources the parasite has</param>
	/// <param name="reproductionReq">The initial resource requirements for reproduction</param>
	/// <param name="factory">An object used to reproduce / create new parasites</param>
	/// <param name="id">An identifier for each parasite instance</param>
	public Parasite
		( 
		Position position, 
		float constitution, 
		float resources, 
		float reproductionReq,
		ParasiteFactory factory,
		int id
		)
	{
		this.location			= position;
		this.constitution		= constitution;
		this.resources			= resources;
		this.reproductionReq	= reproductionReq;
		this.factory			= factory;
		this.id					= id;
	}

	#endregion

	#region Reproduce

	/// <summary>
	/// Create a new parasite at the parents location / reproduce
	/// </summary>
	/// <returns>New parasite</returns>
	public Parasite Reproduce() 
	{
		// Resources are expended on reproduction
		Resources = 0;
		
		// Create child
		return factory.CreateParasite( location );	
	}

	/// <summary>
	/// Calculates whether the parasite has sufficient resources to reproduce
	/// </summary>
	public bool CanReproduce 
	{
		get 
		{
			if( Resources >= ReproductionRequirements )
			{
				return true;
			}
			//else
			return false;
		}
	}

	#endregion

	#region Death

	/// <summary>
	/// Calculates if a parasites constitution is so low that the parasite has died
	/// </summary>
	public bool IsDead 
	{
		get 
		{ 
			if( Constitution <= 0 )
			{
				return true;
			}
			//else
			return false;
		}  
	}

	#endregion

	#region Properties to access parasite parameters

	/// <summary>
	/// A variable to hold the resource requirements for reproduction.
	/// </summary>
	private float reproductionReq;
	/// <summary>
	/// A property to access and mutate the resource requirements for reproduction.
	/// </summary>
	public float ReproductionRequirements
	{
		get{ return reproductionReq; }
		set{ reproductionReq = value; }
	}

	/// <summary>
	/// A variable to hold the resources the parasite has.
	/// </summary>
	private float resources;
	/// <summary>
	/// A property to access and mutate the resources the parasite has.
	/// </summary>
	public float Resources 
	{
		get{ return resources; }
		set{ resources = value; }
	}

	/// <summary>
	/// A variable to hold the constitution of the parasite.
	/// </summary>
	private float constitution;
	/// <summary>
	/// A property to access and mutate the constitution of the parasite.
	/// </summary>
	public float Constitution 
	{
		get{ return constitution; }
		set{ constitution = value; }
	}

	/// <summary>
	/// A variable to hold the object representing the position of the parasite on the host.
	/// </summary>
	private Position location;
	/// <summary>
	/// A property to access and mutate the position of the parasite on the host.
	/// </summary>
	public Position Location 
	{
		get{ return location; }
		set{ location = value; }
	}

	/// <summary>
	/// A variable to hold the object used to reproduce / create new parasites.
	/// </summary>
	private ParasiteFactory factory;
	/// <summary>
	/// A property to access and mutate the object used to reproduce / create new parasites.
	/// </summary>
	public ParasiteFactory Factory 
	{
		get{ return factory; }
		set{ factory = value; }
	}

	/// <summary>
	/// A variable to hold an identifier for each parasite instance.
	/// </summary>
	private int id;
	/// <summary>
	/// A property to access an identifier for each parasite instance.
	/// </summary>
	public int ID
	{
		get{ return id; }
	}
	#endregion

}
