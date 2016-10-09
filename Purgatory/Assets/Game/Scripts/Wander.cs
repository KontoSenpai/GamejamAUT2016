using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wander : MonoBehaviour {

    MapGrid m_map;

    List<Vector2> possibleCases;

    Vector2 destination;
    Vector2 previousPosition;

    bool isWandering;

	// Use this for initialization
	void Start () {
	
        m_map = GameObject.FindObjectOfType<MapGrid>();
        previousPosition = new Vector2();

        possibleCases = new List<Vector2>();
        isWandering = false;

    }
	
	// Update is called once per frame
	void Update () {



        if (isWandering && Vector2.Distance(transform.position, destination) < 0.1f)
            StopWandering();

	}


    public void Wandering()
    {
        Vector2 objectCellCoord = m_map.getCellCoord(transform.position);

        possibleCases.Clear();

        //       #
        //      #X#     Definition of the 4 possible cases
        //       #
        List<Vector2> listAdjacent = new List<Vector2>();

        listAdjacent.Add(new Vector2(objectCellCoord.x + 1, objectCellCoord.y));
        listAdjacent.Add(new Vector2(objectCellCoord.x - 1, objectCellCoord.y));
        listAdjacent.Add(new Vector2(objectCellCoord.x, objectCellCoord.y - 1));
        listAdjacent.Add(new Vector2(objectCellCoord.x, objectCellCoord.y + 1));

        foreach(Vector2 adjacent in listAdjacent)
        {

            if ((adjacent.x >= 0 && adjacent.x < m_map.getRow()) &&
               (adjacent.y >= 0 && adjacent.y < m_map.getColumn()))
                if (m_map.getCellValue((uint)adjacent.x, (uint)adjacent.y) / 1000 == 0)
                    possibleCases.Add(adjacent);
                else if (m_map.getCellValue((uint)adjacent.x, (uint)adjacent.y) / 1000 == 2 &&
                        GetComponent<Soul>().getIsBright())
                    possibleCases.Add(adjacent);
                else if (m_map.getCellValue((uint)adjacent.x, (uint)adjacent.y) / 1000 == 3 &&
                        GetComponent<Soul>().getIsDark())
                    possibleCases.Add(adjacent);

        }

        ////       #
        ////      #X?
        ////       #
        //if (objectCellCoord.x + 1 < m_map.getColumn() && m_map.getCellValue(transform.position) == Constants.EMPTY)
        //{
        //    possibleCases.Add(rightAdjacentCellCoord);
        //    print(rightAdjacentCellCoord);
        //}

        ////       #
        ////      ?X#
        ////       #
        //if (objectCellCoord.x - 1 >= 0 && m_map.getCellValue(transform.position) == Constants.EMPTY)
        //{ 
        //    possibleCases.Add(leftAdjacentCellCoord);
        //    print(leftAdjacentCellCoord);
        //}

        ////       ?
        ////      #X#
        ////       #
        //if (objectCellCoord.y - 1 >= 0 && m_map.getCellValue(transform.position) == Constants.EMPTY)
        //{
        //    possibleCases.Add(upAdjacentCellCoord);
        //    print(upAdjacentCellCoord);
        //}

        ////       #
        ////      #X#
        ////       ?
        //if (objectCellCoord.y + 1 < m_map.getRow() && m_map.getCellValue(transform.position) == Constants.EMPTY)
        //{
        //    possibleCases.Add(downAdjacentCellCoord);
        //    print(downAdjacentCellCoord);
        //}

        //Choose a random cell in the list of possible cells
        do
        {
            int rand = Random.Range(0, possibleCases.Count);

            Vector2 chosenNextCase = possibleCases[rand];

            destination = m_map.getCenterOfCell((uint)chosenNextCase.x, (uint)chosenNextCase.y);

        } while (destination == previousPosition);

        previousPosition = transform.position;

        GetComponent<Soul>().moveTo(destination);


        isWandering = true;
    }

    public void StopWandering()
    {

        if (isWandering)
        {
            GetComponent<Soul>().stopMovement();

            isWandering = false;
        }

    }

}
