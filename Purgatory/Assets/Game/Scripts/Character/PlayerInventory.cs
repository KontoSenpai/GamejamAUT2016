using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    GameObject itemInInventory;
    private bool isBright;

    void Start()
    {
        isBright = GetComponent<Player>().getIsBright();
    }
	// Update is called once per frame
	void Update()
    {
	}

    public void AddInventory(GameObject trap)
    {
        itemInInventory = trap;
        print(itemInInventory.tag);
    }

    public GameObject GetItemInInventory()
    {
        return itemInInventory;
    }

    public void UseItem()
    {
        if( itemInInventory != null)
        {
            GameObject objectPlaced = (GameObject)Instantiate(itemInInventory, transform.position, transform.rotation);
            if (objectPlaced.tag == "Mine")
                DeployMine(objectPlaced);
            if (objectPlaced.tag == "Tower")
                DeployTower(objectPlaced);
            if (objectPlaced.tag == "Wall")
                DeployWall(objectPlaced);
        }
    }

    void DeployMine( GameObject mine)
    {
        if (!isBright)
            mine.GetComponent<MineBehavior>().SetOwner(false);
        else if (isBright)
            mine.GetComponent<MineBehavior>().SetOwner(true);
        itemInInventory = null;
    }

    void DeployTower( GameObject tower)
    {
        if (!isBright)
            tower.GetComponent<TowerBehavior>().SetOwner(false);
        else if (isBright)
            tower.GetComponent<TowerBehavior>().SetOwner(true);
        itemInInventory = null;
    }

    void DeployWall( GameObject wall)
    {
        if (!isBright)
            wall.GetComponent<WallBehavior>().SetOwner(false);
        else if (isBright)
            wall.GetComponent<WallBehavior>().SetOwner(true);
        itemInInventory = null;
    }
}
