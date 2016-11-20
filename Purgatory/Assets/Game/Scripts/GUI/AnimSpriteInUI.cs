using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimSpriteInUI : MonoBehaviour {

    public Sprite[] sprites;
    public float framesPerSecond;

    public void Update()
    {
        int index = (int)(Time.time * framesPerSecond) % sprites.Length;
        GetComponent<Image>().sprite = sprites[index];
    }
}
