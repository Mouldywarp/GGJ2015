using System;
using SFML.Window;
using SFML.Graphics;

/*! \brief Static functions for commonly used functions for finding tile indices and coordinates
 * 
 * Converts between indices (the number of the tile, 0 is top-left then count horizontally first),
 * coordinates (the number of the column and row of the tile), points (a point within a tilemap) and
 * rects (a rectangle representing a full tile withing a tilemap).
 * */
class TileMath
{
    //! Converts a tile's index to its coordinates
    public static Vector2i IndexToCoordinates(int index, int tilesInRow)
    {
        return new Vector2i(index % tilesInRow, (int)(Math.Floor((float)index / tilesInRow)));
    }

    //! Coverts a tile's coordinates to its index
    public static int CoordinatesToIndex(int col, int row, int tilesInRow) { return CoordinatesToIndex(new Vector2i(col, row), tilesInRow); }
    public static int CoordinatesToIndex(Vector2i coords, int tilesInRow)
    {
        return (coords.Y * tilesInRow) + coords.X;
    }

    //! Convert a tile's index to a rect representing that tile in the tilemap
    public static IntRect IndexToRect(int index, int tilesInRow, int tileSize) { return IndexToRect(index, tilesInRow, new Vector2i(tileSize, tileSize)); }
    public static IntRect IndexToRect(int index, int tilesInRow, Vector2i tileSize)
    {
        IntRect rect = new IntRect();
        rect.Width = tileSize.X;
        rect.Height = tileSize.Y;
        rect.Left = (index % tilesInRow) * tileSize.X;
        rect.Top = (int)(Math.Floor((float)index / tilesInRow)) * tileSize.Y;
        return rect;
    }

    //! Converts a point in the tilemap to the tile's coordinates
    public static Vector2i PointToCoordinates(Vector2i point, int tileSize)
    {
        return new Vector2i((int)Math.Floor((float)point.X / tileSize), (int)Math.Floor((float)point.Y / tileSize));
    }
    public static Vector2i PointToCoordinates(Vector2i point, Vector2i tileSize)
    {
        return new Vector2i((int)Math.Floor((float)point.X / tileSize.X), (int)Math.Floor((float)point.Y / tileSize.Y));
    }

    // Convert a point in the tilemap to the index of the tile it falls on
    public static int PointToIndex(Vector2i point, int tilesInRow, int tileSize)
    {
        return CoordinatesToIndex(PointToCoordinates(point, tileSize), tilesInRow);
    }


    //!< Converts an index to the point in tilemap corresponding to top-left of the tile (its origin)
    public static Vector2f IndexToTileOriginf(int index, int tilesInRow, int tileSize)
    {
        return new Vector2f((float)(index % tilesInRow) * tileSize, (float)(Math.Floor(index / (float)tilesInRow)) * tileSize);
    }


    //!< Returns the number of tiles between 2 tiles defined by their coords
    public static int DistanceInTiles(Vector2i tileACoords, Vector2i tileBCoords)
    {
        Vector2i distance = tileBCoords - tileACoords;
        return Math.Abs(distance.X) + Math.Abs(distance.Y);
    }
}

