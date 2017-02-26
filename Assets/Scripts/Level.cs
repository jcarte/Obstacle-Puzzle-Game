using System;

[Serializable]
public class Level
{
    /// <summary>
    /// Unique Id for this level
    /// </summary>
    public int LevelID;

    /// <summary>
    /// Descriptive name for the level
    /// </summary>
    public string Name;

    /// <summary>
    /// Number of rows of tiles in a level
    /// </summary>
    public int RowCount;

    /// <summary>
    /// Number of columns of tiles in a level
    /// </summary>
    public int ColumnCount;

    /// <summary>
    /// Array of row object of tile information
    /// </summary>
    public CellRow[] Array;

    /// <summary>
    /// Number of moves to achieve bronze medal
    /// </summary>
    public int BronzeTarget;

    /// <summary>
    /// Number of moves to achieve Silver medal
    /// </summary>
    public int SilverTarget;

    /// <summary>
    /// Number of moves to achieve Gold medal
    /// </summary>
    public int GoldTarget;

    /// <summary>
    /// Creates a new level
    /// </summary>
    /// <param name="rows">Number of cells high for the whole board</param>
    /// <param name="cols">Number of cells across for the whole board</param>
    /// <returns></returns>
    public static Level Create(int rows, int cols)
    {
        return Create(rows, cols, 0, 0, 0, 0);
    }

    /// <summary>
    /// Creates a new level
    /// </summary>
    /// <param name="rows">Number of cells high for the whole board</param>
    /// <param name="cols">Number of cells across for the whole board</param>
    /// <param name="levelID">Unique id for the level</param>
    /// <returns></returns>
    public static Level Create(int rows, int cols, int levelID)
    {
        return Create(rows, cols, levelID, 0, 0, 0);
    }


    /// <summary>
    /// Creates a new level
    /// </summary>
    /// <param name="rows">Number of cells high for the whole board</param>
    /// <param name="cols">Number of cells across for the whole board</param>
    /// <param name="levelID">Unique id for the level</param>
    /// <param name="bronzeTarget">Number of moves to receive a bronze medal</param>
    /// <param name="silverTarget">Number of moves to receive a silver medal</param>
    /// <param name="goldTarget">Number of moves to receive a gold medal</param>
    /// <returns></returns>
    public static Level Create(int rows, int cols, int levelID, int bronzeTarget, int silverTarget, int goldTarget)
    {
        Level lvl = new Level();
        lvl.LevelID = levelID;
        lvl.Name = "Level " + levelID;// string.Format("00", levelID);
        lvl.BronzeTarget = bronzeTarget;
        lvl.SilverTarget = silverTarget;
        lvl.GoldTarget = goldTarget;
        lvl.RowCount = rows;
        lvl.ColumnCount = cols;
        lvl.Array = new CellRow[rows];

        for (int r = 0; r < rows; r++)
        {
            //Create a new row with col number of cells
            lvl.Array[r] = new CellRow() { Cells = new Cell[cols] };

            //Fill every cell in every row with new blank tiles to start off
            for (int c = 0; c < cols; c++)
            {
                lvl.Array[r].Cells[c] = new Cell()
                {
                    Tile = new Tile()
                    {
                        Type = TileType.Empty
                    }
                };
            }
        }
        return lvl;
    }



    /// <summary>
    /// Alter a tile at a given location
    /// </summary>
    /// <param name="row">Row number (0 based)</param>
    /// <param name="col">Column number (0 based)</param>
    /// <param name="type">Tile type to change cell to</param>
    public void ChangeTile(int row, int col, TileType type)
    {
        ChangeTile(row, col, type, null, null);
    }

    /// <summary>
    /// Alter a tile at a given location and gives the cell a colour, for destination tiles
    /// </summary>
    /// <param name="row">Row number (0 based)</param>
    /// <param name="col">Column number (0 based)</param>
    /// <param name="type">Tile type to change cell to</param>    
    /// <param name="colour">The colour of the destination tile, ignored if other tile type</param>
    public void ChangeTile(int row, int col, TileType type, ColourType colour)
    {
        ChangeTile(row, col, type, colour, null);
    }

    /// <summary>
    /// Alter a tile at a given location and gives the cell a colour, for destination tiles
    /// </summary>
    /// <param name="row">Row number (0 based)</param>
    /// <param name="col">Column number (0 based)</param>
    /// <param name="type">Tile type to change cell to</param>    
    /// <param name="rowRedirectOffset">Number of rows (-+) to move a piece from this cell</param>
    /// <param name="colRedirectOffset">Number of columns (-+) to move a piece from this cell</param>
    public void ChangeTile(int row, int col, TileType type, int rowRedirectOffset, int colRedirectOffset)
    {
        ChangeTile(row, col, type, null, new RedirectVector() { RowOffset = rowRedirectOffset, ColumnOffset = colRedirectOffset });
    }

    private void ChangeTile(int row, int col, TileType type, ColourType? colour, RedirectVector rv)
    {
        if (row < 0 || col < 0 || row >= RowCount || col >= ColumnCount)
            throw new Exception("Row/Column Reference Out Of Range");

        Tile t = new Tile()
        {
            Type = type,
            Colour = colour,
            Redirect = rv
        };
        Array[row].Cells[col].Tile = t;
    }

    /// <summary>
    /// Add a movable piece in given location
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="ct"></param>
    public void AddMovable(int row, int col, ColourType ct)
    {
        if (row < 0 || col < 0 || row >= RowCount || col >= ColumnCount)
            throw new Exception("Row/Column Reference Out Of Range");

        Array[row].Cells[col].Movable = new Movable() { Colour = ct };
    }

    /// <summary>
    /// Removes movable piece at given location
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public void RemoveMovable(int row, int col)
    {
        if (row < 0 || col < 0 || row >= RowCount || col >= ColumnCount)
            throw new Exception("Row/Column Reference Out Of Range");

        Array[row].Cells[col].Movable = null;
    }


    public class CellRow
    {
        public Cell[] Cells;
    }

    public class Cell
    {
        public Tile Tile;
        public Movable Movable = null;
    }

    public class Tile
    {
        public TileType Type;
        public ColourType? Colour = null;
        public RedirectVector Redirect = null;
    }

    public class RedirectVector
    {
        public int RowOffset = 0;
        public int ColumnOffset = 0;
    }

    public class Movable
    {
        public ColourType Colour { get; set; }
    }


    public enum ColourType
    {
        //None = 0,
        Red = 1,
        Yellow = 2,
        Blue = 3,
        Green = 4
    }

    public enum TileType
    {
        Empty = 0,
        Landing = 1,
        NonLanding = 2,
        Obstacle = 3,
        Redirect = 4,
        Enemy = 5,
        Disappearing = 6,
        FakeDisappearing = 7,
        Destination = 8
    }



    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
