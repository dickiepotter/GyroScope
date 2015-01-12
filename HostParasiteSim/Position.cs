#region Imports

using System;

#endregion

/// <summary>
/// Object to represents a 2D position / co-ordinate
/// </summary>
/// <remarks>
/// C# 2005 implements generics, these may be used to extend this class to use userdefined accuracy.
/// </remarks>
public class Position 
{
	#region Hidden variables

	/// <summary>
	/// A variable to reference the movement bounds
	/// </summary>
	private Rectangle border;

	/// <summary>
	/// A variable to hold a random number generator
	/// </summary>
	/// <remarks>
	/// Improved performance gained by using a single random number generator for all random numbers.
	/// </remarks>
	private Random randomGenerator = null;

	#endregion

	#region Constructor(s)

	/// <summary>
	/// Constructor which does not require the input of a random number generator as a default is created
	/// </summary>
	/// <param name="border">A Rectangle object representing the movement bounds</param>
	/// <param name="moveDistance">The distance a move can cover in one timestep</param>
	/// <param name="x">The initial position on the x axis</param>
	/// <param name="y">The initial position on the y axis</param>
	public Position( Rectangle border, float moveDistance, float x, float y) 
	{
		this.randomGenerator	= new Random();
		this.moveDistance		= moveDistance;
		this.border				= border;
		this.x					= x;
		this.y					= y;
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="border">A Rectangle object representing the movement bounds</param>
	/// <param name="moveDistance">The distance a move can cover in one timestep</param>
	/// <param name="x">The initial position on the x axis</param>
	/// <param name="y">The initial position on the y axis</param>
	/// <param name="randomNumberGenerator">A random number generator for use when moving location</param>
	public Position( Rectangle border, float moveDistance, float x, float y, Random randomNumberGenerator) 
	{
		randomGenerator		= randomNumberGenerator;
		this.moveDistance	= moveDistance;
		this.border			= border;
		this.x				= x;
		this.y				= y;
	}

	/// <summary>
	/// Constructor which does not require the input of a random number generator as a default is created and randomly assigns the initial position
	/// </summary>
	/// <param name="border">A Rectangle object representing the movement bounds</param>
	/// <param name="moveDistance">The distance a move can cover in one timestep</param>
	public Position( Rectangle border, float moveDistance )
	{
		randomGenerator		= new Random();
		this.moveDistance	= moveDistance;
		this.border			= border;

		randomPosition();
	}

	/// <summary>
	/// Constructor which randomly assigns the initial position
	/// </summary>
	/// <param name="border">A Rectangle object representing the movement bounds</param>
	/// <param name="moveDistance">The distance a move can cover in one timestep</param>
	/// <param name="randomNumberGenerator">A random number generator for use when moving location</param>
	public Position( Rectangle border, float moveDistance, Random randomNumberGenerator )
	{
		this.randomGenerator	= randomNumberGenerator;
		this.moveDistance		= moveDistance;
		this.border				= border;

		randomPosition();
	}

	#endregion

	#region Accessors and Mutataors
 
	/// <summary>
	/// A variable to flag wether the a movement must be made
	/// </summary>
	private bool mustMove = false;
	/// <summary>
	/// A property to access and mutate the flag wether the a movement must be made
	/// </summary>
	public bool MustMove 
	{
		get{ return mustMove; }
		set{ mustMove = value; }
	}

	/// <summary>
	/// A variable to store the x co-ordinate
	/// </summary>
	private float x;
	/// <summary>
	/// A property to access and mutate the x co-ordinate
	/// </summary>
	public float X 
	{
		get{ return x; }
		set{ x = value; } 
	}

	/// <summary>
	/// A variable to store the y co-ordinate
	/// </summary>
	private float y;
	/// <summary>
	/// A property to access and mutate the y co-ordinate
	/// </summary>
	public float Y 
	{
		get{ return y; }
		set{ y = value; } 
	}

	/// <summary>
	/// A variable to store the distance that may be covered in one movement
	/// </summary>
	private float moveDistance;
	/// <summary>
	/// A property to access and mutate the distance that may be covered in one movement
	/// </summary>
	public float MoveDistance
	{
		get{ return moveDistance; }
		set{ moveDistance = value; } 
	}

	#endregion
	
	#region Movement

	/// <summary>
	/// Move to a random valid position within the border and reset mustMove flag
	/// </summary>
	/// <remarks>
	/// Movement only along an axis and always move the full distance.
	/// If the move results in exceeding the length (Y) bounds, the position will wrap around
	/// On the width bounds (X) the movement will simply not be allowed.
	/// </remarks>
	public void Move() 
	{
		// random integer between 0 and 1 to decide if parasite moves along the x axis or the y axis
		int direction	= randomGenerator.Next(5);
		float distance	= (float)randomGenerator.NextDouble() * this.MoveDistance;
		switch( direction )
		{
			// North +X
			case 1:
				if( (X + distance) <= border.Width )
				{
					X += distance;
				}
				break;

			// East +Y
			case 2:
				Y += distance;
				// Wrap arround
				while( Y > border.Length )
				{
					Y -= border.Length;
				}
				break;

			// South -X
			case 3:
				if( (X - distance) >= 0 )
				{
					X -= distance;
				}
				break;

			// West -Y
			case 4:
				Y -= distance;
				// Wrap arround
				while( Y < 0 )
				{
					Y += border.Length;
				}
				break;
		}

		mustMove = false;
	}

	private void randomPosition()
	{
		// Assume a random position within the border
		X = (float)randomGenerator.NextDouble()* border.Width;
		Y = (float)randomGenerator.NextDouble()* border.Length;
	}

	#endregion

	#region Standard Methods

	/// <summary>
	/// Describe the object in English.
	/// </summary>
	/// <returns></returns>
	public override string ToString()
	{
		return string.Format("(X:{0},Y:{1})", X, Y);
	}

	#endregion
}
