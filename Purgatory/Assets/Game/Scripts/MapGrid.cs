using UnityEngine;
using System.Collections;

public class MapGrid : MonoBehaviour {

    public uint row;
    public uint column;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public GameObject backgroundTile;
    public GameObject rockTile;

    private uint[][] mapGrid;
    private float cellWidth;
    private float cellHeight;

    // Use this for initialization
    void Start () {
        
        cellWidth = (Mathf.Abs(xMin) + Mathf.Abs(xMax)) / column;
        cellHeight = (Mathf.Abs(yMin) + Mathf.Abs(yMax)) / row;

        mapGrid = new uint[row][];

        for (uint i = 0; i < row; i++)
        {

            mapGrid[i] = new uint[column];

            for (uint j = 0; j < column; j++)
            {

                mapGrid[i][j] = Constants.EMPTY;

                backgroundTile.transform.localScale = new Vector3(cellWidth / backgroundTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x * 1.01f,
                                                                  cellHeight / backgroundTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 1.01f,
                                                                  0);
                Instantiate(backgroundTile, new Vector3(xMin + j * cellWidth, yMax - i * cellHeight, 0), Quaternion.identity);

            }
        }

        rockTile.transform.localScale = new Vector3(cellWidth / rockTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x * 1.01f,
                                                    cellHeight / rockTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 1.01f,
                                                    0);
        Instantiate(rockTile, new Vector3(xMin + 2 * cellWidth, yMax - 3 * cellHeight, 0), Quaternion.identity);
        mapGrid[3][2] = Constants.OBSTACLE;

        print(cellWidth);
        print(manhattanDistance(new Vector2(-5.3f, 5.8f), new Vector2(-8.6f, -2.5f)));
        print(getCenterOfCell(5, 0));

        GameObject.FindObjectOfType<Seeker>().seek(new Vector3(-5.0f, -5.0f));

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public uint getRow()
    {
        return row;
    }

    public uint getColumn()
    {
        return column;
    }

    public uint[][] getMatrix()
    {
        return mapGrid;
    }

    public Vector2 getCellCoord(Vector2 pos)
    {
        uint xCol = (uint)((pos.x + (-xMin)) / cellWidth);
        uint yRow = (row - 1) - (uint)((pos.y + (-yMin)) / cellHeight);

        return new Vector2(yRow, xCol);
    }

    public uint getCellValue(Vector2 pos)
    {
        Vector2 coord = getCellCoord(pos);

        return getCellValue((uint)coord.x, (uint)coord.y);
    }

    public uint getCellValue(uint x, uint y)
    {
        return mapGrid[x][y];
    }

    public Vector2 getCenterOfCell(uint x, uint y)
    {
        return new Vector2(xMin + (y * cellWidth + (float)cellWidth / 2),
                           yMax - (x * cellHeight + (float)cellHeight / 2));
    }

    public Vector2 manhattanDistance(Vector2 StartPoint, Vector2 EndPoint)
    {

        Vector2 matrixCellCoord_StartPoint = getCellCoord(StartPoint);
        Vector2 matrixCellCoord_EndPoint = getCellCoord(EndPoint);

        return manhattanDistance((int)matrixCellCoord_StartPoint.x, (int)matrixCellCoord_StartPoint.y,
                                 (int)matrixCellCoord_EndPoint.x, (int)matrixCellCoord_EndPoint.y);

    }


    public Vector2 manhattanDistance(int xStart, int yStart, int xEnd, int yEnd)
    {
        return new Vector2(xEnd - xStart, yEnd - yStart);
    }



}
