#region Imports

using System;

#endregion

/// <summary>
/// This object defines a two dimensional grid.
/// </summary>
public class Grid
{
	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <remarks>
	/// Cell dimensions are defined by deviding the grid dimensions by the columns and rows.
	/// </remarks>
	/// <param name="gridSize">The dimensions of the grid.</param>
	/// <param name="columnCount">The number of columns in the grid.</param>
	/// <param name="rowCount">The number of rows in the grid.</param>
	public Grid(Rectangle gridSize, int columnCount, int rowCount)
	{
		this.GridSize		= gridSize;
		this.ColumnCount	= columnCount;
		this.RowCount		= rowCount;
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <remarks>
	/// Grid dimensions are defined by multipling the cell dimensions by the columns and rows.
	/// </remarks>
	/// <param name="columnCount">The number of columns in the grid.</param>
	/// <param name="rowCount">The number of rows in the grid.</param>
	/// <param name="cellSize">The dimensions of each cell in the grid.</param>
	public Grid(int columnCount, int rowCount, Rectangle cellSize)
	{
		this.ColumnCount	= columnCount;
		this.RowCount		= rowCount;
		this.CellSize		= cellSize;
	}

	/// <summary>
	/// Copy constructor
	/// </summary>
	/// <param name="toCopy">The grid to copy.</param>
	public Grid(Grid toCopy)
	{
		this.GridSize		= toCopy.GridSize;
		this.ColumnCount	= toCopy.ColumnCount;
		this.RowCount		= toCopy.RowCount;
	}

	#endregion
    
	#region Accessors and Mutators

	/// <summary>
	/// A variable to record the number of columns in the grid.
	/// </summary>
	private int columnCount;
	/// <summary>
	/// The property to access and mutate the number of columns in the grid.
	/// </summary>
	public int ColumnCount
	{
		get{ return columnCount; }
		set
		{
			if( value < 1 )
			{
				throw new ArgumentOutOfRangeException("value", value, "There must be one or more columns");
			}
			columnCount = value; 
		}
	}

	/// <summary>
	/// A variable to record the number of rows in the grid.
	/// </summary>
	private int rowCount;
	/// <summary>
	/// The property to access and mutate the number of rows in the grid.
	/// </summary>
	public int RowCount
	{
		get{ return rowCount; }
		set
		{ 
			if( value < 1 )
			{
				throw new ArgumentOutOfRangeException("value", value, "There must be one or more rows");
			}
			rowCount = value; 
		}
	}

	/// <summary>
	/// A variable to record the dimensions of the grid.
	/// </summary>
	private Rectangle gridSize;
	/// <summary>
	/// A property to access and mutate the dimensions of the grid.
	/// </summary>
	public Rectangle GridSize
	{
		get{ return gridSize; }
		set{ gridSize = value; }
	}

	/// <summary>
	/// A property to access and mutate the dimensions of the cells within the grid.
	/// </summary>
	/// <remarks>
	/// These values are calculated, changing the cell dimensions actualy alters the dimensions of the grid.
	/// </remarks>
	public Rectangle CellSize
	{
		get
		{
			float cellWidth	= GridSize.Width / columnCount ;
			float cellLength	= GridSize.Length / rowCount ;
			return new Rectangle( cellWidth, cellLength ); 
		}
		set
		{
			if( value.Width > GridSize.Width || value.Length > GridSize.Length )
			{
				throw new ArgumentOutOfRangeException("value", value, "A cell cannot be larger than the grid");
			}
			GridSize = new Rectangle(value.Width * columnCount, value.Length * rowCount);
		}
	}

	/// <summary>
	/// A property to access the total number of cells in the grid.
	/// </summary>
	public int CellCount
	{
		get
		{
			return columnCount * rowCount;
		}
	}

	#endregion

	#region Position to Grid Index Convertors

	/// <summary>
	/// Returns the given position, converted to an cell number, using the algorithm index = x + (columns * Y).
	/// </summary>
	/// <remarks>
	/// The cell numbering starts from 0 for compatability with array indexing.
	/// </remarks>
	public int Cell(Position position)
	{
		int column	= Column(position);
		int row		= Row(position);

		return column + ( columnCount * row);
	}

	/// <summary>
	/// Identifies the row the given position occupies.
	/// </summary>
	public int Row(Position position)
	{
		int row;
		if(position.Y == GridSize.Length)
		{
			// Special case where a position exactly on the boarder produces an invalid row number
			// This unbalences the probability by a tiny fraction
			row = RowCount -1;
		}
		else
		{
			//value from 0 - n-1
			row = (int)Math.Floor(position.Y / CellSize.Length);
			//validation
			if(row > rowCount-1 || row < 0)
			{
				throw new ArgumentOutOfRangeException("position", position, "Y co-ordinate was outside of the grid");
			}
		}
		
		return row;
	}

	/// <summary>
	/// Identifies the column the given position occupies.
	/// </summary>
	public int Column(Position position)
	{
		int column;
		if(position.X == GridSize.Width)
		{
			// Special case where a position is exactly on the boarder produces an invalid column number
			// This unbalences the probability by a tiny fraction
			column = ColumnCount -1;
		}
		else
		{
			//value from 0 - n-1
			column = (int)Math.Floor(position.X / CellSize.Width);

			//validation
			if(column > columnCount-1 || column < 0)
			{
				throw new ArgumentOutOfRangeException("position", position, "X co-ordinate was outside of the grid");
			}
		}

		return column;
	}

#endregion
}