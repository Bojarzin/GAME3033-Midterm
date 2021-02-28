using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public int gemsCollected = 0;

    public bool redCollected = false;
    public bool orangeCollected = false;
    public bool yellowCollected = false;
    public bool greenCollected = false;
    public bool blueCollected = false;
    public bool indigoCollected = false;
    public bool violetCollected = false;

    public bool redPortal = false;
    public bool orangePortal = false;
    public bool yellowPortal = false;
    public bool greenPortal = false;
    public bool bluePortal = false;
    public bool indigoPortal = false;
    public bool violetPortal = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadScene(string _sceneToLoad)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(_sceneToLoad);
    }
}
