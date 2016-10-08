using UnityEngine;
using System.Collections;

public class PlayerInventory : Player
{
    GameObject itemInInventory;

	// Update is called once per frame
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if( itemInInventory != null)
            {
                print("USING ITEM");
                UseItem();
            }
        }
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
        GameObject objectPlaced = (GameObject)Instantiate(itemInInventory, transform.position, transform.rotation);
        if( objectPlaced.tag == "Mine")
            DeployMine(objectPlaced);
        if( objectPlaced.tag == "Tower")
            DeployTower(objectPlaced);
    }

    void DeployMine( GameObject mine)
    {
        if (getIsDark())
            mine.GetComponent<MineBehavior>().SetOwner(false);
        else if (getIsBright())
            mine.GetComponent<MineBehavior>().SetOwner(true);
        itemInInventory = null;
    }

    void DeployTower( GameObject tower)
    {
        if (getIsDark())
            tower.GetComponent<TowerBehavior>().SetOwner(false);
        else if (getIsBright())
            tower.GetComponent<TowerBehavior>().SetOwner(true);
        itemInInventory = null;
    }
}
