using UnityEngine;
using System.Collections;

public class MapGrid : MonoBehaviour {

    public uint row;
    public uint column;
    public int xMin;
    public int xMax;
    public int yMin;
    public int yMax;

    public GameObject backgroundTile;
    public Sprite backgroundSprite;

    private uint[][] mapGrid;
    private uint cellWidth;
    private uint cellHeight;

    // Use this for initialization
    void Start () {
        
        cellWidth = ((uint)(Mathf.Abs(xMin) + Mathf.Abs(xMax))) / column;
        cellHeight = ((uint)(Mathf.Abs(yMin) + Mathf.Abs(yMax))) / row;

        mapGrid = new uint[row][];

        for (uint i = 0; i < row; i++)
            mapGrid[i] = new uint[column];

        for (uint i = 0; i < row; i++)
        {
            for (uint j = 0; j < column; j++)
            {
                mapGrid[i][j] = Constants.EMPTY;

                backgroundTile.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
                Instantiate(backgroundTile, new Vector3(i,j,0), Quaternion.identity);
            }
        }
                

        print(getCellCoord(new Vector2(-2, 3)));
        print(getCellCoord(new Vector2(-3, -4)));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector2 getCellCoord(Vector2 pos)
    {
        uint xCol = (uint)((pos.x + (-xMin)) / cellWidth);
        uint yRow = (uint)((pos.y + (-yMin)) / cellHeight);

        return new Vector2(yRow, xCol);
    }

    public uint getCellValue(Vector2 pos)
    {
        Vector2 coord = getCellCoord(pos);

        return mapGrid[(uint)coord.x][(uint)coord.y];
    }
}
