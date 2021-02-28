using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas instructionsCanvas;

    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        mainCanvas.enabled = true;
        instructionsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void SwitchCanvas()
    {
        mainCanvas.enabled = !mainCanvas.enabled;
        instructionsCanvas.enabled = !instructionsCanvas.enabled;
    }
}
