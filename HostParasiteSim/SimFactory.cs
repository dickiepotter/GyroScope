#region Imports

using System;

#endregion

/// <summary>
/// Immutable object which creates Simulation instances.
/// </summary>
/// <remarks>
/// Immutable because all simulations for one model must be exactly the same.
/// Provides future abstraction capabilities if used with the abstract factory pattern.
/// Currently helps decouple the simulation from the host and parasite objects.
/// </remarks>
public class SimFactory
{

	#region Constructor(s)

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="initialParasiteQty">The initial number of parasites that will infect each host</param>
	/// <param name="parasiteTravelDist">The distance a parasite can cover in one timestep / move</param>
	/// <param name="parasiteFactory">An object used to create new parasites</param>
	/// <param name="hostLength">The length (y dimension) of the host</param>
	/// <param name="hostWidth">The width (x dimension) of the host</param>
	/// <param name="hostFactory">An object used to create new hosts</param>
	public SimFactory
		(
		int initialParasiteQty,
		float parasiteTravelDist,
		ParasiteFactory parasiteFactory,
		float hostLength, 
		float hostWidth,
		HostFactory hostFactory
		)
	{
		this.initialParasiteQty = initialParasiteQty;
		this.parasiteTravelDist = parasiteTravelDist;
		this.parasiteFactory	= parasiteFactory;
		this.hostLength			= hostLength;
		this.hostWidth			= hostWidth;
		this.hostFactory		= hostFactory;
	}

	#endregion

	#region Values for simulations

	/// <summary>
	/// A variable to store the initial number of parasites that will infect each host.
	/// </summary>
	private int initialParasiteQty;
	/// <summary>
	/// A property to access the initial number of parasites that will infect each host.
	/// </summary>
	public int InitialParasiteQty
	{
		get{ return initialParasiteQty; }
	}

	/// <summary>
	/// A variable to store the distance a parasite can cover in one timestep.
	/// </summary>
	private float parasiteTravelDist;
	/// <summary>
	/// A property to access the distance a parasite can cover in one timestep.
	/// </summary>
	public float ParasiteTravelDist
	{
		get{ return parasiteTravelDist; }
	}

	/// <summary>
	/// A variable to store an object used to create new parasites.
	/// </summary>
	private ParasiteFactory parasiteFactory;
	/// <summary>
	/// A property to access the object used to create new parasites.
	/// </summary>
	public ParasiteFactory ParasiteBuilder
	{
		get{ return parasiteFactory; }
	}

	/// <summary>
	/// A variable to store the length (y dimension) of the host.
	/// </summary>
	private	float hostLength;
	/// <summary>
	/// A property to access the length (y dimension) of the host.
	/// </summary>
	public float HostLength
	{
		get{ return hostLength; }
	}

	/// <summary>
	/// A variable to store the width (x dimension) of the host.
	/// </summary>
	private float hostWidth;
	/// <summary>
	/// A property to access the width (x dimension) of the host.
	/// </summary>
	public float HostWidth
	{
		get{ return hostWidth; }
	}

	/// <summary>
	/// A variable to store an object used to create new hosts.
	/// </summary>
	private HostFactory hostFactory;
	/// <summary>
	/// A property to access the object used to create new hosts.
	/// </summary>
	public HostFactory HostBuilder
	{
		get{ return hostFactory; }
	}

	#endregion

	/// <summary>
	/// Create a Simulation instance using initial values stored.
	/// </summary>
	/// <param name="seed">The random number seed that should control all random numbers generated in the simulation. This allows the reproduction of results.</param>
	/// <param name="caller">The object which called this method.</param>
	/// <returns>New Simulation instance</returns>
	/// <remarks>
	/// The need for the caller parameter should be removed during refactoring. It should not be necessary.
	/// </remarks>
	public Simulation CreateSim(int seed, Model caller)
	{
		//The number of unique locations on host is equal to (rows x columns) where rows and columns = hostWidth/parasiteTravelDist
	//	int hostLocationQty = (int)((hostWidth/parasiteTravelDist)*(hostWidth/parasiteTravelDist));

		Host host = hostFactory.CreateHost(hostWidth, hostLength);

		return new Simulation(host, infect(host, seed), caller);
	}


	/// <summary>
	/// Infect a host with a number of parasites.
	/// Give each parasite a position within the bounds of a host.
	/// </summary>
	/// <param name="host">The host to infect with parasites.</param>
	/// <param name="seed">The random number seed that should control all random numbers generated in the simulation.</param>
	/// <returns>An array of parasites positioned on the host.</returns>
	private Parasite[] infect(Host host, int seed)
	{
		Parasite[] parasites = new Parasite[initialParasiteQty];

		Random rnd;
		if( seed >= 0 ){ rnd = new Random(seed); }
		else{ rnd = new Random(); }

		for( int i=0; i< parasites.Length; i++ )
		{
			Position position = new Position( host.GridSize, parasiteTravelDist, rnd );
			parasites[i] = parasiteFactory.CreateParasite( position );
		}

		return parasites;
	}
}

