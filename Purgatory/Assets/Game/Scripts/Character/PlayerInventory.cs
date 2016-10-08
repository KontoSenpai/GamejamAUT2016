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

    public void UseItem()
    {
        GameObject objectPlaced = (GameObject)Instantiate(itemInInventory, transform.position, transform.rotation);
        if( objectPlaced.tag == "Mine")
        {
            if (getIsDark())
                objectPlaced.GetComponent<MineBehavior>().SetOwner(false);
            else if (getIsBright())
                objectPlaced.GetComponent<MineBehavior>().SetOwner(true);
            itemInInventory = null;
        }
    }
}
