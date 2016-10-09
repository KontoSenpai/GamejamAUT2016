using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    public bool isBright;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Wanderer") && coll.GetComponent<Soul>().getIsBright() && isBright)
        {
            Destroy(coll.gameObject);
            Spawn.decreaseNumberOfWanderer();
            GameObject.FindObjectOfType<GameController>().gainScoreAngel(Constants.WANDERER_SCORE);
        }
        else if (coll.CompareTag("Wanderer") && coll.GetComponent<Soul>().getIsDark() && !isBright)
        {
            Destroy(coll.gameObject);
            Spawn.decreaseNumberOfWanderer();
            GameObject.FindObjectOfType<GameController>().gainScoreDemon(Constants.WANDERER_SCORE);
        }
    }
}
