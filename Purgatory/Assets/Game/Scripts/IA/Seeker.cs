using UnityEngine;
using System.Collections.Generic;

public struct Tile
{
    public int value;
    public Vector2 pos;

    public Tile(int _value, Vector2 _pos)
    {
        this.value = _value;
        this.pos = _pos;
    }

    public void setValue(int _value)
    {
        this.value = _value;
    }
}

public class Seeker : MonoBehaviour {

    public Vector2 m_destination;

    Stack<Vector2> m_pathToDestination;

    bool isActive;

    private List<Vector2> m_visited;

    Stack<Vector2> m_best;

    MapGrid m_map;

    // Use this for initialization
    void Start()
    {
        isActive = false;
        m_visited = new List<Vector2>();
        m_pathToDestination = new Stack<Vector2>();
        m_best = new Stack<Vector2>();

        m_map = GameObject.FindObjectOfType<MapGrid>();
    }

    void FixedUpdate()
    {

        if(isActive)
        {

            if(m_best.Count == 0)
            {

                isActive = false;

            }
            else
            {
                uint value = m_map.getCellValue(new Vector2((uint)m_best.Peek().x, (uint)m_best.Peek().y));
                if (value / 1000 == 1)
                    AStar(m_map.getCellCoord(transform.position), m_destination);
                else if (value / 1000 == 2 && !GetComponent<Soul>().getIsBright())
                    AStar(m_map.getCellCoord(transform.position), m_destination);
                else if (value / 1000 == 3 && !GetComponent<Soul>().getIsDark())
                    AStar(m_map.getCellCoord(transform.position), m_destination);


                Vector2 destination = m_map.getCenterOfCell((uint)m_best.Peek().x, (uint)m_best.Peek().y);

                GetComponent<Soul>().moveTo(destination);

                if (Vector2.Distance(destination, transform.position) < 0.1f)
                    m_best.Pop();

                if (m_best.Count == 0)
                    GetComponent<Soul>().stopMovement();

            }

        }

    }
        
    public void seek(Vector2 position)
    {
        Clear();

        m_destination = m_map.getCellCoord(position);

        isActive = true;

        AStar(
            m_map.getCellCoord(transform.position),
            m_map.getCellCoord(position));

    }

    bool AStar(Vector2 origin, Vector2 destination)
    {

        if (!m_map)
            return false;

        int test2 = buildPath(origin, destination, 0);

        m_visited.Clear();

        if (test2 != -1)
            return true;
        else
            return false;
    }

    int buildPath(Vector2 currentTile, Vector2 destination, int iter)
    {

        uint[][] matrix = m_map.getMatrix();

        if (currentTile == destination)
        {
            if (iter < m_best.Count || m_best.Count == 0)
            {
                m_best.Clear();

                m_best.Push(currentTile);

                Vector2[] copyPath = new Vector2[50];
                m_pathToDestination.CopyTo(copyPath, 0);

                for (int i = 0; i < m_pathToDestination.Count; i++)
                    m_best.Push(copyPath[i]);

            }

            return iter;
        }

        if (m_visited.Contains(currentTile))
            return -1;

        if (currentTile.x < 0 || currentTile.x >= matrix.Length || currentTile.y < 0 || currentTile.y >= matrix[0].Length)
            return -1;

        if (matrix[(int)currentTile.x][(int)currentTile.y] / 1000 == 1)
            return -1;
        else if (matrix[(int)currentTile.x][(int)currentTile.y] / 1000 == 2 &&
                 !GetComponent<Soul>().getIsBright())
            return -1;
        else if (matrix[(int)currentTile.x][(int)currentTile.y] / 1000 == 3 &&
                 !GetComponent<Soul>().getIsDark())
            return -1;

        m_pathToDestination.Push(currentTile);
        m_visited.Add(currentTile);

        List<Tile> bestAdjacents = new List<Tile>();

        bestAdjacents.Add(new Tile(posValue(new Vector2(currentTile.x - 1, currentTile.y), iter + 1, destination), new Vector2(currentTile.x - 1, currentTile.y)));
        bestAdjacents.Add(new Tile(posValue(new Vector2(currentTile.x + 1, currentTile.y), iter + 1, destination), new Vector2(currentTile.x + 1, currentTile.y)));
        bestAdjacents.Add(new Tile(posValue(new Vector2(currentTile.x, currentTile.y + 1), iter + 1, destination), new Vector2(currentTile.x, currentTile.y + 1)));
        bestAdjacents.Add(new Tile(posValue(new Vector2(currentTile.x, currentTile.y - 1), iter + 1, destination), new Vector2(currentTile.x, currentTile.y - 1)));

        bestAdjacents.Sort(compareTiles);

        int bestPath = 9999;
        int pathValue = 0;

        foreach (Tile adjacent in bestAdjacents)
        {
            if (bestPath != 9999 && adjacent.value > bestPath)
                break;
            if ((pathValue = buildPath(adjacent.pos, destination, iter + 1)) != -1)
                if (pathValue <= bestPath)
                {
                    bestPath = pathValue;
                    break;
                }
        }

        if (bestPath != 9999)
        {
            m_visited.Remove(currentTile);
            m_pathToDestination.Pop();
            return bestPath;
        }

        m_pathToDestination.Pop();
        return -1;

    }

    int compareTiles(Tile first, Tile second)
    {
        if (first.value > second.value)
            return 1;
        else if (first.value < second.value)
            return -1;

        return 0;
    }
    
    int ManhattanDistance(Vector2 origin, Vector2 destination)
    {
        return (int)(Mathf.Abs(destination.x - origin.x) + Mathf.Abs(destination.y - origin.y));
    }

    int posValue(Vector2 pos, int iter, Vector2 destination)
    {
        return iter + ManhattanDistance(pos, destination);
    }

    public void Clear()
    {
        isActive = false;
        m_visited.Clear();
        m_pathToDestination.Clear();
        m_best.Clear();
    }

}
