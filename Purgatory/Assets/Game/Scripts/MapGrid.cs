using UnityEngine;
using System.Collections;

public class MapGrid : MonoBehaviour {

    public uint row;
    public uint column;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public GameObject backgroundAngelTile;
    public GameObject backgroundDemonTile;
    public GameObject[] obstacleTile;
    public GameObject angelBaseTile;
    public GameObject demonBaseTile;
    public GameObject wandererSpawnerTile;
    public GameObject pickUpSpawnerTile;

    private uint[][] mapGrid;
    private float cellWidth;
    private float cellHeight;

    // Use this for initialization
    void Start () {

        gridSetup();
        setObstacles();
        setPlayerBasesTiles(10, 2, 2, 14);
        setWandererSpawners();
        setPickUpSpawners();    

        //GameObject.FindObjectOfType<Seeker>().seek(new Vector3(-5.0f, -5.0f));

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

    public void addValueToCell(Vector2 pos, uint value)
    {
        Vector2 cellCoord = getCellCoord(pos);

        mapGrid[(uint)cellCoord.x][(uint)cellCoord.y] += value;
    }

    public void substractValueToCell(Vector2 pos, uint value)
    {
        Vector2 cellCoord = getCellCoord(pos);

        mapGrid[(uint)cellCoord.x][(uint)cellCoord.y] -= value;
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

    public void gridSetup()
    {
        GameObject backgroundTile;

        cellWidth = (Mathf.Abs(xMin) + Mathf.Abs(xMax)) / column;
        cellHeight = (Mathf.Abs(yMin) + Mathf.Abs(yMax)) / row;

        mapGrid = new uint[row][];

        for (uint i = 0; i < row; i++)
        {

            mapGrid[i] = new uint[column];

            for (uint j = 0; j < column; j++)
            {
                if (j < 8)
                {
                    backgroundTile = backgroundAngelTile;
                }
                else if (j == 8)
                {
                    if (i % 2 == 0)
                        backgroundTile = backgroundAngelTile;
                    else
                        backgroundTile = backgroundDemonTile;
                }
                else
                {
                    backgroundTile = backgroundDemonTile;
                }

                mapGrid[i][j] = Constants.EMPTY;

                backgroundTile.transform.localScale = new Vector3(cellWidth / backgroundTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x * 1.01f,
                                                                  cellHeight / backgroundTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 1.01f,
                                                                  0);
                Instantiate(backgroundTile, new Vector3(xMin + j * cellWidth, yMax - i * cellHeight, 0), Quaternion.identity);

            }
        }
    }

    public void setObstacleTile(uint row, uint column)
    {
        GameObject currentObstacleTile = obstacleTile[Random.Range(0, obstacleTile.Length)];

        currentObstacleTile.transform.localScale = new Vector3(cellWidth / currentObstacleTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                                                               cellHeight / currentObstacleTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y,
                                                               0);
        Instantiate(currentObstacleTile, new Vector3(xMin + column * cellWidth, yMax - row * cellHeight, 0), Quaternion.identity);
        mapGrid[row][column] = Constants.OBSTACLE;
    }

    public void setObstacles()
    {
        setObstacleTile(0, 4);
        setObstacleTile(1, 12);
        setObstacleTile(3, 10);
        setObstacleTile(4, 2);
        setObstacleTile(4, 9);
        setObstacleTile(4, 13);
        setObstacleTile(5, 2);
        setObstacleTile(5, 3);
        setObstacleTile(7, 13);
        setObstacleTile(7, 14);
        setObstacleTile(8, 3);
        setObstacleTile(8, 7);
        setObstacleTile(8, 14);
        setObstacleTile(9, 6);
        setObstacleTile(11, 4);
        setObstacleTile(12, 12);
    }

    public void setPlayerBasesTiles(uint angelBaseRow, uint angelBaseColumn, uint demonBaseRow, uint demonBaseColumn)
    {
        angelBaseTile.transform.localScale = new Vector3(cellWidth / angelBaseTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                                                         cellHeight / angelBaseTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y,
                                                          0);
        Instantiate(angelBaseTile, 
                    new Vector3(xMin + angelBaseColumn * cellWidth + cellWidth/2, yMax - angelBaseRow * cellHeight - cellHeight/2, 0), 
                    Quaternion.identity);
        mapGrid[angelBaseRow][angelBaseColumn] = Constants.CRYSTAL_ANGEL;


        demonBaseTile.transform.localScale = new Vector3(cellWidth / demonBaseTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                                                         cellHeight / demonBaseTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y,
                                                          0);
        Instantiate(demonBaseTile,
                    new Vector3(xMin + demonBaseColumn * cellWidth + cellWidth / 2, yMax - demonBaseRow * cellHeight - cellHeight / 2, 0),
                    Quaternion.identity);
        mapGrid[demonBaseRow][demonBaseColumn] = Constants.CRYSTAL_DEMON;
    }

    public void setWandererSpawnerTile(uint row, uint column)
    {
        wandererSpawnerTile.transform.localScale = new Vector3(cellWidth / wandererSpawnerTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                                                         cellHeight / wandererSpawnerTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y,
                                                          0);
        Instantiate(wandererSpawnerTile,
                    new Vector3(xMin + column * cellWidth, yMax - row * cellHeight, 0),
                    Quaternion.identity);
        mapGrid[row][column] = Constants.EMPTY;
    }

    public void setWandererSpawners()
    {
        setWandererSpawnerTile(1, 1);
        setWandererSpawnerTile(1, 5);
        setWandererSpawnerTile(1, 10);
        setWandererSpawnerTile(2, 3);
        setWandererSpawnerTile(5, 6);
        setWandererSpawnerTile(7, 10);
        setWandererSpawnerTile(10, 13);
        setWandererSpawnerTile(11, 6);
        setWandererSpawnerTile(11, 11);
        setWandererSpawnerTile(11, 15);
    }
    
    public void setPickUpSpawnerTile(uint row, uint column)
    {
        pickUpSpawnerTile.transform.localScale = new Vector3(cellWidth / pickUpSpawnerTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                                                             cellHeight / pickUpSpawnerTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y,
                                                             0);
        Instantiate(pickUpSpawnerTile,
                    new Vector3(xMin + column * cellWidth, yMax - row * cellHeight, 0),
                    Quaternion.identity);
        mapGrid[row][column] = Constants.EMPTY;
    }

    public void setPickUpSpawners()
    {
        setPickUpSpawnerTile(1, 15);
        setPickUpSpawnerTile(2, 7);
        setPickUpSpawnerTile(4, 3);
        setPickUpSpawnerTile(8, 13);
        setPickUpSpawnerTile(10, 9);
        setPickUpSpawnerTile(11, 1);

    }
}
