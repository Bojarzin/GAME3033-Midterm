using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    public bool isUsed = false;

    public enum Colour
    {
        RED,
        ORANGE,
        YELLOW,
        GREEN,
        BLUE,
        INDIGO,
        VIOLET
    };

    public Colour colour;

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Start()
    {

    }

    private void Update()
    {
        switch (colour)
        {
            case Colour.RED:
                isUsed = GameManager.Instance.redPortal;
                break;
            case Colour.ORANGE:
                isUsed = GameManager.Instance.orangePortal;
                break;
            case Colour.YELLOW:
                isUsed = GameManager.Instance.yellowPortal;
                break;
            case Colour.GREEN:
                isUsed = GameManager.Instance.greenPortal;
                break;
            case Colour.BLUE:
                isUsed = GameManager.Instance.bluePortal;
                break;
            case Colour.INDIGO:
                isUsed = GameManager.Instance.indigoPortal;
                break;
            case Colour.VIOLET:
                isUsed = GameManager.Instance.violetPortal;
                break;
        }
    }
}
