#region Imports

using System;

#endregion

/// <summary>
/// Object to represent a 2D rectangle with an origin of 0,0
/// </summary>
/// <remarks>
/// Float precision has been chosen
/// C# 2005 implements generics, these may be used to extend this class to use user-defined accuracy.
/// Should be imposible to create an invalid rectangle i.e. length or width less than or equil to 0
/// </remarks>
public struct Rectangle
{
	#region Constructor(s)

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="width">The width of the rectangle</param>
	/// <param name="length">The length of the rectangle</param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if a length or width less than 0 is entered
	/// </exception>
	public Rectangle( float width, float length )
	{
		if( length >=0 )
		{
			this.length = length;
		}
		else 
		{
			throw new ArgumentOutOfRangeException( "length", length, "Length must be greater than 0" );
		}
		if( width >=0.0 )
		{
			this.width = width;
		}
		else 
		{
			throw new ArgumentOutOfRangeException( "width", width, "Width must be greater than 0" );
		}
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="squareSize">The length of one side of a square</param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if a size less than 0 is entered
	/// </exception>
	public Rectangle( float squareSize )
	{
		if( squareSize >=0 )
		{
			this.length = squareSize;
			this.width = squareSize;
		}
		else 
		{
			throw new ArgumentOutOfRangeException( "squareSize", squareSize, "Size must be greater than 0" );
		}
	}

	#endregion

	#region Accessors and mutators

	/// <summary>
	/// A variable to store the length of the rectangle
	/// </summary>
	private float length;
	/// <summary>
	/// A property to access and mutate the length of the rectangle
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if a length less than 0 is entered
	/// </exception>
	public float Length 
	{
		get{ return length; }
		set
		{ 
			if( value >0 )
			{
				this.length = value;
			}
			else 
			{
				throw new ArgumentOutOfRangeException( "length", length, "Length must be greater than 0" );
			}
		}
	}

	/// <summary>
	/// A variable to store the width of the rectangle
	/// </summary>
	private float width;
	/// <summary>
	/// A property to access and mutate the width of the rectangle
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if a width less than 0 is entered
	/// </exception>
	public float Width 
	{
		get{ return width; }
		set
		{ 
			if( value >0 )
			{
				this.width = value;
			}
			else 
			{
				throw new ArgumentOutOfRangeException( "width", width, "Width must be greater than 0" ); 
			}
		}
	}

	#endregion
}
