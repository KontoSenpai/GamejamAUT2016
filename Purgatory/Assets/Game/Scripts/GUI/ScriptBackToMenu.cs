using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScriptBackToMenu : MonoBehaviour
{

    public void onClick()
    {
        SceneManager.LoadScene("Main");
    }
}
