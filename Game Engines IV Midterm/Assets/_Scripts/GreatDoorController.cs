using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatDoorController : MonoBehaviour
{
    public Light redLight;
    public Light orangeLight;
    public Light yellowLight;
    public Light greenLight;
    public Light blueLight;
    public Light indigoLight;
    public Light violetLight;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        redLight.enabled = false;
        orangeLight.enabled = false;
        yellowLight.enabled = false;
        greenLight.enabled = false;
        blueLight.enabled = false;
        indigoLight.enabled = false;
        violetLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        redLight.enabled = GameManager.Instance.redCollected;
        orangeLight.enabled = GameManager.Instance.orangeCollected;
        yellowLight.enabled = GameManager.Instance.yellowCollected;
        greenLight.enabled = GameManager.Instance.greenCollected;
        blueLight.enabled = GameManager.Instance.blueCollected;
        indigoLight.enabled = GameManager.Instance.indigoCollected;
        violetLight.enabled = GameManager.Instance.violetCollected;

        if (redLight.enabled && orangeLight.enabled && yellowLight.enabled && greenLight.enabled && blueLight.enabled)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if (door.transform.rotation.x > 0)
        {
            door.transform.Rotate(new Vector3(10.0f, 0.0f, 0.0f) * Time.deltaTime);
        }
    }
}
