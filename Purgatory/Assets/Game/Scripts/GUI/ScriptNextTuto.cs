using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScriptNextTuto : MonoBehaviour {

    public void onClick()
    {
        SceneManager.LoadScene("Help2");
    }
}

